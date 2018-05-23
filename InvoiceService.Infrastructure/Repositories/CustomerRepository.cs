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
	public class CustomerRepository : ICustomerRepository
	{
		private readonly IEventSourcingRepository<Customer, CustomerId> _eventRepository;

		public CustomerRepository(IEventSourcingRepository<Customer, CustomerId> eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public async Task CreateCustomerAsync(string customerId, string email, string address, string postalCode, string residence)
		{
			Customer customer = new Customer(new CustomerId(customerId), email, address, postalCode, residence);
			await _eventRepository.SaveAsync(customer);
		}

		public Task<Customer> GetCustomerAsync(string customerId)
		{
			return _eventRepository.GetByIdAsync(new CustomerId(customerId));
		}

		public async Task UpdateCustomerAsync(string customerId, string email, string address, string postalCode, string residence)
		{
			var customer = await _eventRepository.GetByIdAsync(new CustomerId(customerId));
			customer.UpdateCustomer(email, address, postalCode, residence);
			await _eventRepository.SaveAsync(customer);
		}

		public async Task DeleteCustomerAsync(string customerId)
		{
			var customer = await _eventRepository.GetByIdAsync(new CustomerId(customerId));
			customer.Delete();
			await _eventRepository.SaveAsync(customer);
		}

		public async Task AddInvoice(string customerId, string invoiceId)
		{
			Customer customer = await _eventRepository.GetByIdAsync(new CustomerId(customerId));
			customer.AddInvoice(invoiceId);

			await _eventRepository.SaveAsync(customer);
		}
	}

}
