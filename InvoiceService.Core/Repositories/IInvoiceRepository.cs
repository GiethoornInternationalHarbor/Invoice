using InvoiceService.Core.Models;
using System;
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
		Task<Invoice> GetInvoice(string email);

		/// <summary>
		/// Updates the invoice asynchronous.
		/// </summary>
		/// <param name="shipService">The ship service.</param>
		/// <returns></returns>
		Task<Invoice> UpdateInvoiceAsync(Ship ship, ShipService shipService);

		/// <summary>
		/// Updates the invoice asynchronous.
		/// </summary>
		/// <param name="rental">The rental.</param>
		/// <returns></returns>
		Task<Invoice> UpdateInvoiceAsync(Rental rental);
	}
}
