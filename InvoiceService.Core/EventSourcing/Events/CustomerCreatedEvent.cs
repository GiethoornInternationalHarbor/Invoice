using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class CustomerCreatedEvent : DomainEventBase<CustomerId>
	{
		public string Email { get; set; }

		public string Address { get; set; }

		public string PostalCode { get; set; }

		public string Residence { get; set; }

		public CustomerCreatedEvent()
		{

		}

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
