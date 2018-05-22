using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly InvoiceDbContext _invoiceDbContext;
		private readonly IRentalRepository _rentalRepository;

		public InvoiceRepository(InvoiceDbContext invoiceDbContext, IRentalRepository rentalRepository)
		{
			_invoiceDbContext = invoiceDbContext;
			_rentalRepository = rentalRepository;
		}

		private async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
		{
			var invoiceToAdd = (await _invoiceDbContext.Invoices.AddAsync(invoice)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return invoiceToAdd;
		}

		private async Task<Invoice> CreateInvoiceLineAsync(string customerId, InvoiceLine invoiceLine)
		{
			var invoice = await GetInvoice(customerId);

			invoice.Lines.Add(invoiceLine);

			_invoiceDbContext.Invoices.Update(invoice);

			return invoice;
		}

		public async Task<Invoice> AddShipServiceLineAsync(Customer customer, Ship ship, ShipService shipService)
		{
			var invoiceLine = new InvoiceLine()
			{
				Description = $"Service: {shipService.Name} applied for ship: {ship.Name}",
				InvoiceType = InvoiceTypes.ShipService,
				Price = shipService.Price
			};

			return await CreateInvoiceLineAsync(customer.Email, invoiceLine);
		}

		public async Task<Invoice> UpdateInvoiceAsync(Customer customer, Rental rental)
		{
			var invoiceLine = new InvoiceLine()
			{
				InvoiceType = InvoiceTypes.Rental,
				Price = rental.Price
			};

			var invoice = await CreateInvoiceLineAsync(customer.Email, invoiceLine);

			await _rentalRepository.DeleteRental(rental.Id);

			return invoice;
		}

		public async Task<Invoice> GetInvoice(string email)
		{
			return await _invoiceDbContext.Invoices.LastOrDefaultAsync(x => x.Customer.Email == email);
		}
	}
}
