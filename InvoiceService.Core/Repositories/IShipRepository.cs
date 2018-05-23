using InvoiceService.Core.Models;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IShipRepository
	{
		/// <summary>
		/// Creates the ship.
		/// </summary>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="shipName">Name of the ship.</param>
		/// <returns></returns>
		Task CreateShip(string customerId, string shipName);

		/// <summary>
		/// Gets the ship.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<Ship> GetShip(Guid id);

		/// <summary>
		/// Deletes the ship.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteShip(Guid id);
	}
}
