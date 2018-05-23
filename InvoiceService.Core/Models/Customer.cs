using InvoiceService.Core.Events;
using InvoiceService.Core.EventSourcing;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Customer : AggregateBase<CustomerId>
	{
		private string Email { get; set; }

		private string Address { get; set; }

		private string PostalCode { get; set; }

		private string Residence { get; set; }

		public Customer(CustomerId customerId, string email, string address, string postalCode, string residence)
		{
			if (customerId == null) throw new ArgumentNullException(nameof(customerId));
			RaiseEvent(new CustomerCreatedEvent(customerId, email, address, postalCode, residence));
		}

		internal void Apply(CustomerCreatedEvent ev)
		{
			Id = ev.AggregateId;
			Email = ev.Email;
			Address = ev.Address;
			PostalCode = ev.PostalCode;
			Residence = ev.Residence;
		}
	}
}
