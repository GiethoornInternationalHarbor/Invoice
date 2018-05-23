using InvoiceService.Core.EventSourcing;

namespace InvoiceService.Core.Models
{
	public class ShipServiceAddedEvent : DomainEventBase<InvoiceId>
	{
		public ShipServiceId ShipServiceId { get; private set; }

		internal ShipServiceAddedEvent(ShipServiceId shipServiceId) : base()
		{
			ShipServiceId = shipServiceId;
		}

		private ShipServiceAddedEvent(InvoiceId aggregateId, long aggregateVersion, ShipServiceId shipServiceId) : base(aggregateId, aggregateVersion)
		{
			ShipServiceId = shipServiceId;
		}

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new ShipServiceAddedEvent(aggregateId, aggregateVersion, ShipServiceId);
		}
	}
}
