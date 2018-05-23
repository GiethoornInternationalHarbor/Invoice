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
		/// Gets the last invoice for customer.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <returns></returns>
		Task<Invoice> GetLastInvoiceForCustomer(string customerId);

		/// <summary>
		/// Adds the ship service line asynchronous.
		/// </summary>
		/// <param name="invoiceId">The invoice identifier.</param>
		/// <param name="shipId">The ship identifier.</param>
		/// <param name="serviceId">The service identifier.</param>
		/// <returns></returns>
		Task<Invoice> AddShipServiceLineAsync(string invoiceId, string shipId, string serviceId);
	}
}
