using InvoiceService.Core.Models;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IRentalRepository
	{
		/// <summary>
		/// Gets the rental.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<Rental> GetRental(Guid id);

		/// <summary>
		/// Creates the rental.
		/// </summary>
		/// <param name="rental">The rental.</param>
		/// <returns></returns>
		Task<Rental> CreateRental(Rental rental);

		/// <summary>
		/// Deletes the rental.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteRental(Guid id);
	}
}
