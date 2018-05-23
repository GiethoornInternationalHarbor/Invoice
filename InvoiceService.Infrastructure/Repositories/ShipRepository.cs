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
			Ship ship = new Ship(new ShipId(), new CustomerId(customerId), shipName);
			await _eventRepository.SaveAsync(ship);
		}

		public async Task DeleteShip(string id)
		{
			var ship = await _eventRepository.GetByIdAsync(new ShipId(id));
			ship.Undocked();
			await _eventRepository.SaveAsync(ship);
		}

		public Task<Ship> GetShip(string id)
		{
			return _eventRepository.GetByIdAsync(new ShipId(id));
		}
	}
}
