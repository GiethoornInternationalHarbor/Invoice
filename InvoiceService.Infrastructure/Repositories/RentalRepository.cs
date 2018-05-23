using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using InvoiceService.Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class RentalRepository : IRentalRepository
	{
		private readonly IEventSourcingRepository<Rental, RentalId> _eventRepository;

		public RentalRepository(IEventSourcingRepository<Rental, RentalId> eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public async Task CreateRental(string rentalId, double price)
		{
			Rental rental = new Rental(new RentalId(rentalId), price);
			await _eventRepository.SaveAsync(rental);
		}

		public async Task DeleteRental(string rentalId)
		{
			var rental = await _eventRepository.GetByIdAsync(new RentalId(rentalId));
			rental.Decline();
			await _eventRepository.SaveAsync(rental);
		}

		public Task<Rental> GetRental(string rentalId)
		{
			return _eventRepository.GetByIdAsync(new RentalId(rentalId));
		}
	}
}
