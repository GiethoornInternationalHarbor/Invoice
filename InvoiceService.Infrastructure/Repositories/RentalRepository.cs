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
		private readonly InvoiceDbContextFactory _invoiceDbFactory;

		public RentalRepository(InvoiceDbContextFactory invoiceDbContextFactory)
		{
			_invoiceDbFactory = invoiceDbContextFactory;
		}

		public async Task<Rental> CreateRental(Rental rental)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var rentalToAdd = (await dbContext.Rentals.AddAsync(rental)).Entity;
			await dbContext.SaveChangesAsync();
			return rentalToAdd;
		}

		public async Task DeleteRental(Guid id)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var rentalToDelete = new Rental() { Id = id };
			dbContext.Entry(rentalToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public async Task<Rental> GetRental(Guid id)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return await dbContext.Rentals.LastOrDefaultAsync(x => x.Id == id);
		}
	}
}
