using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using InvoiceService.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class ShipServiceRepository : IShipServiceRepository
	{
		private readonly IEventSourcingRepository<ShipService, ShipServiceId> _eventRepository;

		public ShipServiceRepository(IEventSourcingRepository<ShipService, ShipServiceId> eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public async Task CreateShipService(string serviceId, string name, double price)
		{
			ShipService shipService = new ShipService(new ShipServiceId(serviceId), name, price);
			await _eventRepository.SaveAsync(shipService);
		}

		public async Task DeleteShipService(string id)
		{
			ShipService service = await _eventRepository.GetByIdAsync(new ShipServiceId(id));
			service.Delete();
			await _eventRepository.SaveAsync(service);
		}

		public Task<ShipService> GetShipService(string id)
		{
			return _eventRepository.GetByIdAsync(new ShipServiceId(id));
		}

		public async Task UpdateShipService(string serviceId, string name, double price)
		{
			var service = await _eventRepository.GetByIdAsync(new ShipServiceId(serviceId));
			service.Update(name, price);
			await _eventRepository.SaveAsync(service);
		}
	}
}
