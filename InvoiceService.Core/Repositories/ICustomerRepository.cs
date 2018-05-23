using InvoiceService.Core.Models;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface ICustomerRepository
	{
		/// <summary>
		/// Gets the customer asynchronous.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <returns></returns>
		Task<Customer> GetCustomerAsync(string customerId);

		/// <summary>
		/// Creates the customer asynchronous.
		/// </summary>
		/// <param name="customerId">The customer.</param>
		/// <param name="email">The email.</param>
		/// <param name="address">The address.</param>
		/// <param name="postalCode">The postal code.</param>
		/// <param name="residence">The residence.</param>
		/// <returns></returns>
		Task CreateCustomerAsync(string customerId, string email, string address, string postalCode, string residence);

		/// <summary>
		/// Updates the customer asynchronous.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="email">The email.</param>
		/// <param name="address">The address.</param>
		/// <param name="postalCode">The postal code.</param>
		/// <param name="residence">The residence.</param>
		/// <returns></returns>
		Task UpdateCustomerAsync(string customerId, string email, string address, string postalCode, string residence);

		/// <summary>
		/// Deletes the customer asynchronous.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <returns></returns>
		Task DeleteCustomerAsync(string customerId);

		/// <summary>
		/// Adds the invoice.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="invoiceId">The invoice identifier.</param>
		/// <returns></returns>
		Task AddInvoice(string customerId, string invoiceId);
	}
}
