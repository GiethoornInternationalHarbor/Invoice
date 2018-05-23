using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using InvoiceService.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly IEventSourcingRepository<Invoice, InvoiceId> _eventRepository;
		private readonly ICustomerRepository _customerRepository;

		public InvoiceRepository(IEventSourcingRepository<Invoice, InvoiceId> eventRepo, ICustomerRepository customerRepository)
		{
			_eventRepository = eventRepo;
			_customerRepository = customerRepository;
		}

		public async Task<Invoice> AddShipServiceLineAsync(string invoiceId, string shipId, string serviceId)
		{
			throw new NotImplementedException();

			/*var invoiceLine = new InvoiceLine()
			{
				Description = $"Service: {shipService.Name} applied for ship: {ship.Name}",
				InvoiceType = InvoiceTypes.ShipService,
				Price = shipService.Price
			};

			return await CreateInvoiceLineAsync(customer.Email, invoiceLine);*/
		}

		public async Task<Invoice> UpdateInvoiceAsync(Customer customer, Rental rental)
		{
			throw new NotImplementedException();

			/*var invoiceLine = new InvoiceLine()
			{
				InvoiceType = InvoiceTypes.Rental,
				Price = rental.Price
			};

			var invoice = await CreateInvoiceLineAsync(customer.Email, invoiceLine);

			await _rentalRepository.DeleteRental(rental.Id);

			return invoice;*/
		}

		public async Task<Invoice> GetInvoiceByEmail(string email)
		{
			throw new NotImplementedException();

			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return await dbContext.Invoices.LastOrDefaultAsync(x => x.Customer.Email == email);*/
		}

		public async Task CreateInvoice(string customerId, string rentalId)
		{
			Invoice invoice = new Invoice(new InvoiceId(), new CustomerId());
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
	}
}
