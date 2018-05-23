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
	public class RentalRepository : IRentalRepository
	{
		private readonly IEventSourcingRepository<Rental, RentalId> _eventRepository;
		private readonly IInvoiceRepository _invoiceRepository;

		public RentalRepository(IEventSourcingRepository<Rental, RentalId> eventRepository, IInvoiceRepository invoiceRepository)
		{
			_eventRepository = eventRepository;
			_invoiceRepository = invoiceRepository;
		}

		public async Task Accept(string rentalId, double price)
		{
			var rental = await _eventRepository.GetByIdAsync(new RentalId(rentalId));
			rental.Accept(price);
			await _eventRepository.SaveAsync(rental);
		}

		public async Task CreateRental(string customerId, string rentalId, double price)
		{
			Rental rental = new Rental(new RentalId(rentalId), new CustomerId(customerId), price);
			await _eventRepository.SaveAsync(rental);

			await _invoiceRepository.CreateInvoice(customerId, rentalId);
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
