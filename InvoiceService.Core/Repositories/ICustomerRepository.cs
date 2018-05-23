using InvoiceService.Core.Models;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface ICustomerRepository
	{
		/// <summary>
		/// Gets the customer asynchronous.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		Task<Customer> GetCustomerAsync(string email);

		/// <summary>
		/// Creates the customer asynchronous.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="address">The address.</param>
		/// <param name="postalCode">The postal code.</param>
		/// <param name="residence">The residence.</param>
		/// <returns></returns>
		Task CreateCustomerAsync(string email, string address, string postalCode, string residence);

		/// <summary>
		/// Updates the customer asynchronous.
		/// </summary>
		/// <param name="customer">The customer.</param>
		/// <returns></returns>
		Task<Customer> UpdateCustomerAsync(Customer customer);
	}
}
