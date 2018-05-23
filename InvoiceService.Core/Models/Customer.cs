using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using InvoiceService.Core.EventSourcing.Ids;
using System;
using System.Collections.Generic;

namespace InvoiceService.Core.Models
{
	public class Customer : AggregateBase<CustomerId>
	{
		public string Email { get; protected set; }

		public string Address { get; protected set; }

		public string PostalCode { get; protected set; }

		public string Residence { get; protected set; }

		public bool IsDeleted { get; protected set; }

		public List<InvoiceId> Invoices { get; protected set; }

		private Customer()
		{
			Invoices = new List<InvoiceId>();
		}

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

		public void AddInvoice(string invoiceId)
		{
			if (IsDeleted)
				return;

			RaiseEvent(new CustomerInvoiceAddedEvent(Id, invoiceId));
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

		internal void Apply(CustomerInvoiceAddedEvent ev)
		{
			Invoices.Add(new InvoiceId(ev.InvoiceId));
		}
	}
}
