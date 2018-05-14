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

		public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
		{
			var invoiceToAdd = (await _invoiceDbContext.Invoices.AddAsync(invoice)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return invoiceToAdd;
		}

		public async Task<InvoiceLine> CreateInvoiceLineAsync(Guid id, InvoiceLine invoiceLine)
		{
			var invoice = await GetInvoice(id);
			invoice.Lines.Add(invoiceLine);
			return invoiceLine;
		}

		public Task<Invoice> GetInvoice(Guid id)
		{
			return _invoiceDbContext.Invoices.LastOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Invoice> UpdateInvoiceAsync(Ship ship)
		{
			var invoice = await GetInvoice(ship.Email);

			if (ship.ShipServices != null && ship.ShipServices.Count > 0)
			{
				ship.ShipServices.ForEach(x =>
				{
					invoice.Price = invoice.Price += x.Price;
				});
			}

			return invoice;
		}

		public async Task<Invoice> UpdateInvoiceAsync(Rental rental)
		{
			var invoice = await GetInvoice(rental.Email);

			if (rental.Accepted)
			{
				invoice.Price = invoice.Price += rental.Price;
			}

			return invoice;
		}

		private async Task<Invoice> GetInvoice(string email)
		{
			return await _invoiceDbContext.Invoices.LastOrDefaultAsync(x => x.Customer.Email == email);
		}
	}
}
