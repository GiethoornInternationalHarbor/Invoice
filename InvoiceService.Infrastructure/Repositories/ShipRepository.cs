using System;
using System.Threading.Tasks;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure.Repositories
{
	public class ShipRepository : IShipRepository
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly InvoiceDbContextFactory _invoiceDbFactory;

		public ShipRepository(IInvoiceRepository invoiceRepository, InvoiceDbContextFactory invoiceDbContextFactory)
		{
			_invoiceDbFactory = invoiceDbContextFactory;
			_invoiceRepository = invoiceRepository;
		}

		public async Task<Ship> CreateShip(Ship ship)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var shipToAdd = (await dbContext.Ships.AddAsync(ship)).Entity;
			await dbContext.SaveChangesAsync();
			return shipToAdd;
		}

		public async Task DeleteShip(Guid id)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var shipToDelete = new Ship() { Id = id };
			dbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public Task<Ship> GetShip(Guid id)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return dbContext.Ships.LastOrDefaultAsync(x => x.Id == id);
		}
	}
}
