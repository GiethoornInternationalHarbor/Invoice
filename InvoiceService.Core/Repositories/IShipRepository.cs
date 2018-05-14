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
		/// <param name="ship">The ship.</param>
		/// <returns></returns>
		Task<Ship> CreateShip(Ship ship); 

		/// <summary>
		/// Updates the ship.
		/// </summary>
		/// <param name="ship">The ship.</param>
		/// <returns></returns>
		Task<Ship> UpdateShip(Ship ship);

		/// <summary>
		/// Deletes the ship.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteShip(Guid id);
	}
}
