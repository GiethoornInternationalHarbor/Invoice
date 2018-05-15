using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class ShipServiceRepository : IShipServiceRepository
	{
		private readonly InvoiceDbContext _invoiceDbContext;

		public ShipServiceRepository(InvoiceDbContext invoiceDbContext)
		{
			_invoiceDbContext = invoiceDbContext;
		}

		public async Task<ShipService> CreateShipService(ShipService shipService)
		{
			var shipServiceToAdd = (await _invoiceDbContext.ShipServices.AddAsync(shipService)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return shipServiceToAdd;
		}

		public async Task DeleteShipService(Guid id)
		{
			var shipServiceToDelete = new Ship() { Id = id };
			_invoiceDbContext.Entry(shipServiceToDelete).State = EntityState.Deleted;
			await _invoiceDbContext.SaveChangesAsync();
		}

		public async Task<ShipService> UpdateShipService(ShipService shipService)
		{
			var updatedShipService = _invoiceDbContext.ShipServices.Update(shipService);
			await _invoiceDbContext.SaveChangesAsync();
			return updatedShipService.Entity;
		}
	}
}
