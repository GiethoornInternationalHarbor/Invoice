using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class RentalDeclinedEvent : DomainEventBase<RentalId>
	{
		public RentalDeclinedEvent()
		{

		}

		internal RentalDeclinedEvent(RentalId aggregateId) : base(aggregateId)
		{
		}

		private RentalDeclinedEvent(RentalId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
		{ }

		public override IDomainEvent<RentalId> WithAggregate(RentalId aggregateId, long aggregateVersion)
		{
			return new RentalDeclinedEvent(aggregateId, aggregateVersion);
		}
	}
}
