using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly InvoiceDbContext _invoiceDbContext;

		public CustomerRepository(InvoiceDbContext invoiceDbContext)
		{
			_invoiceDbContext = invoiceDbContext;
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			var customerToAdd = (await _invoiceDbContext.Customers.AddAsync(customer)).Entity;
			await _invoiceDbContext.SaveChangesAsync();
			return customerToAdd;
		}

		public Task<Customer> GetCustomerAsync(string email)
		{
			return _invoiceDbContext.Customers.LastOrDefaultAsync(x => x.Email == email);
		}

		public async Task<Customer> UpdateCustomerAsync(Customer customer)
		{
			var updatedCustomer = _invoiceDbContext.Customers.Update(customer);
			await _invoiceDbContext.SaveChangesAsync();
			return updatedCustomer.Entity;
		}
	}
}
