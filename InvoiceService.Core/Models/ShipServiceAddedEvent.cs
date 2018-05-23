using InvoiceService.Core.EventSourcing;

namespace InvoiceService.Core.Models
{
	public class ShipServiceAddedEvent : DomainEventBase<InvoiceId>
	{
		public ShipServiceId ShipServiceId { get; private set; }

		public ShipId ShipId { get; private set; }

		internal ShipServiceAddedEvent(ShipServiceId shipServiceId, ShipId shipId) : base()
		{
			ShipServiceId = shipServiceId;
			ShipId = shipId;
		}

		private ShipServiceAddedEvent(InvoiceId aggregateId, long aggregateVersion, ShipServiceId shipServiceId, ShipId shipId) : base(aggregateId, aggregateVersion)
		{
			ShipServiceId = shipServiceId;
			ShipId = shipId;
		}

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new ShipServiceAddedEvent(aggregateId, aggregateVersion, ShipServiceId, ShipId);
		}
	}
}
