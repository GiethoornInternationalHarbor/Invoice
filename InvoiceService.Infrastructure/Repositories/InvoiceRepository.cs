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

		public InvoiceRepository(InvoiceDbContext invoiceDbContext)
		{
			_invoiceDbContext = invoiceDbContext;
		}

		private async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
		{
			var invoiceToAdd = (await _invoiceDbContext.Invoices.AddAsync(invoice)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return invoiceToAdd;
		}

		private async Task<Invoice> CreateInvoiceLineAsync(string email, InvoiceLine invoiceLine)
		{
			var invoice = await GetInvoice(email);

			invoice.Lines.Add(invoiceLine);

			_invoiceDbContext.Invoices.Update(invoice);

			return invoice;
		}

		public async Task<Invoice> UpdateInvoiceAsync(Ship ship, ShipService shipService)
		{
			var invoiceLine = new InvoiceLine()
			{
				InvoiceType = InvoiceTypes.ShipService,
				Price = shipService.Price
			};

			return await CreateInvoiceLineAsync(ship.Email, invoiceLine);
		}

		public async Task<Invoice> UpdateInvoiceAsync(Rental rental)
		{
			var invoiceLine = new InvoiceLine()
			{
				InvoiceType = InvoiceTypes.Rental,
				Price = rental.Price
			};

			return await CreateInvoiceLineAsync(rental.Email, invoiceLine);
		}

		public async Task<Invoice> GetInvoice(string email)
		{
			return await _invoiceDbContext.Invoices.LastOrDefaultAsync(x => x.Customer.Email == email);
		}
	}
}
