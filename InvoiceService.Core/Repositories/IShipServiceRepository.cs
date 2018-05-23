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
		/// <param name="serviceId">The service identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		Task CreateShipService(string serviceId, string name, double price);

		/// <summary>
		/// Gets the ship service.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<ShipService> GetShipService(string id);

		/// <summary>
		/// Updates the ship service.
		/// </summary>
		/// <param name="serviceId">The service identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		Task UpdateShipService(string serviceId, string name, double price);

		/// <summary>
		/// Deletes the ship service.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteShipService(string id);
	}
}
