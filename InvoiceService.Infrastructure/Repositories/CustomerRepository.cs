using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly InvoiceDbContextFactory _invoiceDbFactory;
		private readonly IEventSourcingRepository<Customer> _eventRepository;

		public CustomerRepository(InvoiceDbContextFactory invoiceDbContextFactory)
		{
			_invoiceDbFactory = invoiceDbContextFactory;
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var customerToAdd = (await dbContext.Customers.AddAsync(customer)).Entity;
			await dbContext.SaveChangesAsync();
			return customerToAdd;
		}

		public Task<Customer> GetCustomerAsync(string email)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			return dbContext.Customers.LastOrDefaultAsync(x => x.Email == email);
		}

		public async Task<Customer> UpdateCustomerAsync(Customer customer)
		{
			InvoiceDbContext dbContext = _invoiceDbFactory.CreateDbContext();
			var updatedCustomer = dbContext.Customers.Update(customer);
			await dbContext.SaveChangesAsync();
			return updatedCustomer.Entity;
		}
	}
}
