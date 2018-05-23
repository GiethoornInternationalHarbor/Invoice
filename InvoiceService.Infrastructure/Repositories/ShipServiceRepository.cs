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
		private readonly InvoiceDbContextFactory _invoiceDbFactory;

		public ShipServiceRepository(InvoiceDbContextFactory invoiceDbContextFactory)
		{
			_invoiceDbFactory = invoiceDbContextFactory;
		}

		public async Task<ShipService> CreateShipService(ShipService shipService)
		{
			throw new NotImplementedException();

			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var shipServiceToAdd = (await dbContext.ShipServices.AddAsync(shipService)).Entity;
			await dbContext.SaveChangesAsync();
			return shipServiceToAdd;*/
		}

		public async Task DeleteShipService(Guid id)
		{
			throw new NotImplementedException();
			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var shipServiceToDelete = new Ship() { Id = id };
			dbContext.Entry(shipServiceToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();*/
		}

		public Task<ShipService> GetShipService(Guid id)
		{
			throw new NotImplementedException();

			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return dbContext.ShipServices.LastOrDefaultAsync(x => x.Id == id);*/
		}

		public async Task<ShipService> UpdateShipService(ShipService shipService)
		{
			throw new NotImplementedException();

			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var updatedShipService = dbContext.ShipServices.Update(shipService);
			await dbContext.SaveChangesAsync();
			return updatedShipService.Entity;*/
		}
	}
}
