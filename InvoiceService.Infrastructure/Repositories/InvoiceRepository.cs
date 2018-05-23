using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using InvoiceService.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System;
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

		private async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var invoiceToAdd = (await dbContext.Invoices.AddAsync(invoice)).Entity;
			await dbContext.SaveChangesAsync();
			return invoiceToAdd;
		}

		private async Task<Invoice> CreateInvoiceLineAsync(string customerId, InvoiceLine invoiceLine)
		{
			throw new NotImplementedException();
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			/*var invoice = await GetInvoiceByEmail(customerId);

			invoice.Lines.Add(invoiceLine);

			dbContext.Invoices.Update(invoice);

			await dbContext.SaveChangesAsync();

			return invoice;*/
		}

		public async Task<Invoice> AddShipServiceLineAsync(Customer customer, Ship ship, ShipService shipService)
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
	}
}
