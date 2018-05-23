using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IRentalRepository
	{
		/// <summary>
		/// Gets the rental.
		/// </summary>
		/// <param name="rentalId">The rental identifier.</param>
		/// <returns></returns>
		Task<Rental> GetRental(string rentalId);

		/// <summary>
		/// Accepts the specified rental identifier.
		/// </summary>
		/// <param name="rentalId">The rental identifier.</param>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		Task Accept(string rentalId, double price);

		/// <summary>
		/// Creates the rental.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="rentalId">The rental identifier.</param>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		Task<RentalId> CreateRental(string customerId, string rentalId, double price);

		/// <summary>
		/// Deletes the rental.
		/// </summary>
		/// <param name="rentalId">The rental identifier.</param>
		/// <returns></returns>
		Task DeleteRental(string rentalId);
	}
}
