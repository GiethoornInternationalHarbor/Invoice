using InvoiceService.Core.Models;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IInvoiceRepository
	{
		/// <summary>
		/// Creates the invoice.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="rentalId">The rental identifier.</param>
		/// <returns></returns>
		Task CreateInvoice(string customerId, string rentalId);

		/// <summary>
		/// Gets the invoice.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		Task<Invoice> GetInvoiceByEmail(string email);

		/// <summary>
		/// Adds the ship service line asynchronous.
		/// </summary>
		/// <param name="customer">The customer.</param>
		/// <param name="ship">The ship.</param>
		/// <param name="shipService">The ship service.</param>
		/// <returns></returns>
		Task<Invoice> AddShipServiceLineAsync(Customer customer, Ship ship, ShipService shipService);

		/// <summary>
		/// Updates the invoice asynchronous.
		/// </summary>
		/// <param name="customer">The customer.</param>
		/// <param name="rental">The rental.</param>
		/// <returns></returns>
		Task<Invoice> UpdateInvoiceAsync(Customer customer, Rental rental);
	}
}
