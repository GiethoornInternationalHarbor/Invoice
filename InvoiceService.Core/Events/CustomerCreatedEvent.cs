using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.Events
{
	public class CustomerCreatedEvent : DomainEventBase<CustomerId>
	{
		public string Email { get; private set; }

		public string Address { get; private set; }

		public string PostalCode { get; private set; }

		public string Residence { get; private set; }

		internal CustomerCreatedEvent(CustomerId aggregateId, string email, string address, string postalCode, string residence) : base(aggregateId)
		{
			Email = email;
			Address = address;
			PostalCode = postalCode;
			Residence = residence;
		}

		private CustomerCreatedEvent(CustomerId aggregateId, long aggregateVersion, string email, string address, string postalCode, string residence) : base(aggregateId, aggregateVersion)
		{
			Email = email;
			Address = address;
			PostalCode = postalCode;
			Residence = residence;
		}

		public override IDomainEvent<CustomerId> WithAggregate(CustomerId aggregateId, long aggregateVersion)
		{
			return new CustomerCreatedEvent(aggregateId, aggregateVersion, Email, Address, PostalCode, Residence);
		}
	}
}
