using System;
using System.Threading.Tasks;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using InvoiceService.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure.Repositories
{
	public class ShipRepository : IShipRepository
	{
		private readonly IEventSourcingRepository<Ship, ShipId> _eventRepository;

		public ShipRepository(IEventSourcingRepository<Ship, ShipId> repo)
		{
			_eventRepository = repo;
		}

		public async Task CreateShip(string customerId, string shipName)
		{
			Ship ship = new Ship(ShipId.NewShipServiceId(), new CustomerId(customerId), shipName);
			await _eventRepository.SaveAsync(ship);
		}

		public async Task DeleteShip(Guid id)
		{
			throw new NotImplementedException();

			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var shipToDelete = new Ship() { Id = id };
			dbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();*/
		}

		public Task<Ship> GetShip(Guid id)
		{
			/*InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return dbContext.Ships.LastOrDefaultAsync(x => x.Id == id);*/
			throw new NotImplementedException();
		}
	}
}
