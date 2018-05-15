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
		/// <param name="customer">The customer.</param>
		/// <returns></returns>
		Task<Customer> CreateCustomerAsync(Customer customer);

		/// <summary>
		/// Updates the customer asynchronous.
		/// </summary>
		/// <param name="customer">The customer.</param>
		/// <returns></returns>
		Task<Customer> UpdateCustomerAsync(Customer customer);
	}
}
