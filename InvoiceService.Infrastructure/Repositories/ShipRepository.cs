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
		private readonly InvoiceDbContext _invoiceDbContext;
		private readonly IInvoiceRepository _invoiceRepository;

		public ShipRepository(IInvoiceRepository invoiceRepository, InvoiceDbContext invoiceDbContext)
		{
			_invoiceDbContext = invoiceDbContext;
			_invoiceRepository = invoiceRepository;
		}

		public async Task<Ship> CreateShip(Ship ship)
		{
			var shipToAdd = (await _invoiceDbContext.Ships.AddAsync(ship)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return shipToAdd;
		}

		public async Task DeleteShip(Guid id)
		{
			var shipToDelete = new Ship() { Id = id };
			_invoiceDbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await _invoiceDbContext.SaveChangesAsync();
		}

		public Task<Ship> GetShip(Guid id)
		{
			return _invoiceDbContext.Ships.LastOrDefaultAsync(x => x.Id == id);
		}
	}
}
