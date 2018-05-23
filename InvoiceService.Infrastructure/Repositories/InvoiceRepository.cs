using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using InvoiceService.Core.ReadModel;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.EventSourcing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly IEventSourcingRepository<Invoice, InvoiceId> _eventRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IShipServiceRepository _shipServiceRepository;
		private readonly IShipRepository _shipRepository;
		private readonly IRentalRepository _rentalRepository;

		public InvoiceRepository(IEventSourcingRepository<Invoice, InvoiceId> eventRepo, ICustomerRepository customerRepository, IShipServiceRepository shipServiceRepository, IShipRepository shipRepository, IRentalRepository rentalRepository)
		{
			_eventRepository = eventRepo;
			_customerRepository = customerRepository;
			_shipServiceRepository = shipServiceRepository;
			_shipRepository = shipRepository;
			_rentalRepository = rentalRepository;
		}

		public async Task CreateInvoice(string customerId, string rentalId)
		{
			Invoice invoice = new Invoice(new InvoiceId(), new CustomerId(customerId));
			invoice.SetRental(new RentalId(rentalId));

			await _eventRepository.SaveAsync(invoice);
			await _customerRepository.AddInvoice(customerId, invoice.Id.ToString());
		}

		public async Task<Invoice> GetLastInvoiceForCustomer(string customerId)
		{
			Customer customer = await _customerRepository.GetCustomerAsync(customerId);

			Invoice invoice = await _eventRepository.GetByIdAsync(customer.Invoices.Last());
			return invoice;
		}

		public async Task<InvoiceOverviewReadModel> GetInvoicesForCustomer(string customerId)
		{
			Customer customer = await _customerRepository.GetCustomerAsync(customerId);

			InvoiceOverviewReadModel overview = new InvoiceOverviewReadModel
			{
				CustomerId = customerId,
				Invoices = customer.Invoices.Select(x => x.IdAsString())
			};

			return overview;
		}

		public async Task<InvoiceReadModel> GetInvoice(string invoiceId)
		{
			Invoice invoice = await _eventRepository.GetByIdAsync(new InvoiceId(invoiceId));

			var taskInvoiceLines = Task.WhenAll(invoice.Lines.Select(async x =>
		   {
			   var getShipTask = _shipRepository.GetShip(x.ShipId.ToString());
			   var getServiceTask = _shipServiceRepository.GetShipService(x.ServiceId.ToString());
			   await Task.WhenAll(getShipTask, getServiceTask);

			   Ship ship = getShipTask.Result;
			   ShipService service = getServiceTask.Result;

			   return new InvoiceLineReadModel
			   {
				   Description = $"Service: {service.Name} applied for ship: {ship.Name}",
				   Price = service.Price
			   };
		   }));


			var taskCustomer = Task.Run(async () =>
			{
				Customer foundCustomer = await _customerRepository.GetCustomerAsync(invoice.CustomerId.ToString());
				CustomerReadModel readModel = new CustomerReadModel
				{
					Address = foundCustomer.Address,
					Email = foundCustomer.Email,
					PostalCode = foundCustomer.PostalCode,
					Residence = foundCustomer.Residence
				};

				return readModel;
			});

			var taskRental = Task.Run(async () =>
			{
				Rental foundRental = await _rentalRepository.GetRental(invoice.RentalId.ToString());
				RentalReadModel readModel = new RentalReadModel
				{
					Price = foundRental.Price
				};

				return readModel;
			});

			await Task.WhenAll(taskInvoiceLines, taskCustomer, taskRental);

			var customer = await taskCustomer;
			var lines = await taskInvoiceLines;
			var rental = await taskRental;

			return new InvoiceReadModel
			{
				Customer = customer,
				Lines = lines,
				Rental = rental,
				TotalPrice = rental.Price + lines.Sum(x => x.Price)
			};
		}

		public async Task AddShipServiceLineAsync(string invoiceId, string shipId, string serviceId)
		{
			Invoice invoice = await _eventRepository.GetByIdAsync(new InvoiceId(invoiceId));
			invoice.AddShipService(new ShipServiceId(serviceId), new ShipId(shipId));
			await _eventRepository.SaveAsync(invoice);

		}
	}
}
