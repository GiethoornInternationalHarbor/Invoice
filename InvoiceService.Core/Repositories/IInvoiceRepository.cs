using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IInvoiceRepository
	{
		/// <summary>
		/// Gets the invoice asynchronous.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		Task<Invoice> GetInvoice(Guid id);

		/// <summary>
		/// Creates the invoice asynchronous.
		/// </summary>
		/// <param name="invoice">The invoice.</param>
		/// <returns></returns>
		Task<Invoice> CreateInvoiceAsync(Invoice invoice);

		/// <summary>
		/// Creates the invoice line asynchronous.
		/// </summary>
		/// <param name="invoiceLine">The invoice line.</param>
		/// <returns></returns>
		Task<InvoiceLine> CreateInvoiceLineAsync(Guid id, InvoiceLine invoiceLine);

		/// <summary>
		/// Updates the invoice asynchronous.
		/// </summary>
		/// <param name="ship">The ship.</param>
		/// <returns></returns>
		Task<Invoice> UpdateInvoiceAsync(Ship ship);

		/// <summary>
		/// Updates the invoice asynchronous.
		/// </summary>
		/// <param name="rental">The rental.</param>
		/// <returns></returns>
		Task<Invoice> UpdateInvoiceAsync(Rental rental);
	}
}
