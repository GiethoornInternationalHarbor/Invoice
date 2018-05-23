using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using System;

namespace InvoiceService.Core.Models
{
	public class Customer : AggregateBase<CustomerId>
	{
		private string Email { get; set; }

		private string Address { get; set; }

		private string PostalCode { get; set; }

		private string Residence { get; set; }

		private bool IsDeleted { get; set; }

		private Customer() { }

		public Customer(CustomerId customerId, string email, string address, string postalCode, string residence)
		{
			if (customerId == null) throw new ArgumentNullException(nameof(customerId));
			RaiseEvent(new CustomerCreatedEvent(customerId, email, address, postalCode, residence));
		}

		public void UpdateCustomer(string email, string address, string postalCode, string residence)
		{
			if (!IsDeleted)
			{
				RaiseEvent(new CustomerUpdatedEvent(Id, Email, Address, PostalCode, Residence, email, address, postalCode, residence));
			}
		}

		public void Delete()
		{
			if (!IsDeleted)
			{
				RaiseEvent(new CustomerDeletedEvent(Id));
			}
		}

		internal void Apply(CustomerCreatedEvent ev)
		{
			Id = ev.AggregateId;
			Email = ev.Email;
			Address = ev.Address;
			PostalCode = ev.PostalCode;
			Residence = ev.Residence;
		}

		internal void Apply(CustomerUpdatedEvent ev)
		{
			Email = ev.NewEmail;
			Address = ev.NewAddress;
			PostalCode = ev.NewPostalCode;
			Residence = ev.NewResidence;
		}

		internal void Apply(CustomerDeletedEvent ev)
		{
			IsDeleted = true;
		}
	}
}
