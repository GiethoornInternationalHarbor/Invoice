using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class RentalRepository : IRentalRepository
	{
		private readonly InvoiceDbContext _invoiceDbContext;

		public RentalRepository(InvoiceDbContext invoiceDbContext)
		{
			_invoiceDbContext = invoiceDbContext;
		}

		public async Task<Rental> CreateRental(Rental rental)
		{
			var rentalToAdd = (await _invoiceDbContext.Rentals.AddAsync(rental)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return rentalToAdd;
		}

		public async Task DeleteRental(Guid id)
		{
			var rentalToDelete = new Rental() { Id = id };
			_invoiceDbContext.Entry(rentalToDelete).State = EntityState.Deleted;
			await _invoiceDbContext.SaveChangesAsync();
		}

		public async Task<Rental> GetRental(Guid id)
		{
			return await _invoiceDbContext.Rentals.LastOrDefaultAsync(x => x.Id == id);
		}
	}
}
