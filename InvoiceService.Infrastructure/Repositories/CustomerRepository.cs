﻿using InvoiceService.Core.Models;
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

		public async Task CreateCustomerAsync(string email, string address, string postalCode, string residence)
		{
			Customer customer = new Customer(new CustomerId(), email, address, postalCode, residence);
			await _eventRepository.SaveAsync(customer);
		}

		public Task<Customer> GetCustomerAsync(string email)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateCustomerAsync(string customerId, string email, string address, string postalCode, string residence)
		{
			var customer = await _eventRepository.GetByIdAsync(new CustomerId(customerId));
			customer.UpdateCustomer(email, address, postalCode, residence);
			await _eventRepository.SaveAsync(customer);
		}
	}
}
