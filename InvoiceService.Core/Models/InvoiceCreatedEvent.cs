using InvoiceService.Core.EventSourcing;

namespace InvoiceService.Core.Models
{
	public class InvoiceCreatedEvent : DomainEventBase<InvoiceId>
	{	 
		internal InvoiceCreatedEvent(InvoiceId aggregateId, CustomerId customerId) : base(aggregateId)
		{
			CustomerId = customerId;
		}

		private InvoiceCreatedEvent(InvoiceId aggregateId, long aggregateVersion, CustomerId customerId) : base(aggregateId, aggregateVersion)
		{
			CustomerId = customerId;
		}

		public CustomerId CustomerId { get; private set; }

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new InvoiceCreatedEvent(aggregateId, aggregateVersion, CustomerId);
		}
	}
}
