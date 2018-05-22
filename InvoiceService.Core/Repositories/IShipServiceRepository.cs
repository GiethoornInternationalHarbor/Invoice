using InvoiceService.Core.Models;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Core.Repositories
{
	public interface IShipServiceRepository
	{
		/// <summary>
		/// Creates the ship service.
		/// </summary>
		/// <param name="shipService">The ship service.</param>
		/// <returns></returns>
		Task<ShipService> CreateShipService(ShipService shipService);

		/// <summary>
		/// Gets the ship service.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<ShipService> GetShipService(Guid id);

		/// <summary>
		/// Updates the ship service.
		/// </summary>
		/// <param name="shipService">The ship service.</param>
		/// <returns></returns>
		Task<ShipService> UpdateShipService(ShipService shipService);

		/// <summary>
		/// Deletes the ship service.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteShipService(Guid id);
	}
}
