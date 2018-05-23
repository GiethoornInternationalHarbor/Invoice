using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class ShipServiceDeletedEvent : DomainEventBase<ShipServiceId>
	{
		public ShipServiceDeletedEvent()
		{

		}

		internal ShipServiceDeletedEvent(ShipServiceId aggregateId) : base(aggregateId)
		{
		}

		private ShipServiceDeletedEvent(ShipServiceId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
		{ }

		public override IDomainEvent<ShipServiceId> WithAggregate(ShipServiceId aggregateId, long aggregateVersion)
		{
			return new ShipServiceDeletedEvent(aggregateId, aggregateVersion);
		}
	}
}
