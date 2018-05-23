using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class CustomerUpdatedEvent : DomainEventBase<CustomerId>
	{
		public string OldEmail { get; set; }
		public string OldAddress { get; set; }
		public string OldPostalCode { get; set; }
		public string OldResidence { get; set; }
		public string NewEmail { get; set; }
		public string NewAddress { get; set; }
		public string NewPostalCode { get; set; }
		public string NewResidence { get; set; }

		public CustomerUpdatedEvent()
		{

		}

		internal CustomerUpdatedEvent(CustomerId aggregateId, string oldEmail, string oldAddress, string oldPostalCode, string oldResidence, string newEmail, string newAddress, string newPostalCode, string newResidence) : base(aggregateId)
		{
			OldEmail = oldEmail;
			OldAddress = oldAddress;
			OldPostalCode = oldPostalCode;
			OldResidence = oldResidence;
			NewEmail = newEmail;
			NewAddress = newAddress;
			NewPostalCode = newPostalCode;
			NewResidence = newResidence;
		}

		private CustomerUpdatedEvent(CustomerId aggregateId, long aggregateVersion, string oldEmail, string oldAddress, string oldPostalCode, string oldResidence, string newEmail, string newAddress, string newPostalCode, string newResidence) : base(aggregateId, aggregateVersion)
		{
			OldEmail = oldEmail;
			OldAddress = oldAddress;
			OldPostalCode = oldPostalCode;
			OldResidence = oldResidence;
			NewEmail = newEmail;
			NewAddress = newAddress;
			NewPostalCode = newPostalCode;
			NewResidence = newResidence;
		}

		public override IDomainEvent<CustomerId> WithAggregate(CustomerId aggregateId, long aggregateVersion)
		{
			return new CustomerUpdatedEvent(aggregateId, aggregateVersion, OldEmail, OldAddress, OldPostalCode, OldResidence, NewEmail, NewAddress, NewPostalCode, NewResidence);
		}
	}
}