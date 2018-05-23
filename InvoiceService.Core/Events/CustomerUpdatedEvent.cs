using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.Events
{
	public class CustomerUpdatedEvent : DomainEventBase<CustomerId>
	{
		internal CustomerUpdatedEvent(CustomerId aggregateId) : base(aggregateId)
		{
		}

		private CustomerUpdatedEvent(CustomerId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
		{
		}

		public override IDomainEvent<CustomerId> WithAggregate(CustomerId aggregateId, long aggregateVersion)
		{
			return new CustomerUpdatedEvent(aggregateId, aggregateVersion);
		}
	}
}
