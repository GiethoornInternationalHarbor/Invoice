using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class ShipServiceUpdatedEvent : DomainEventBase<ShipServiceId>
	{
		public string OldName { get; set; }
		public string NewName { get; set; }
		public double OldPrice { get; set; }
		public double NewPrice { get; set; }

		public ShipServiceUpdatedEvent()
		{

		}

		internal ShipServiceUpdatedEvent(ShipServiceId aggregateId, string oldName, double oldPrice, string newName, double newPrice) : base(aggregateId)
		{
			OldName = oldName;
			OldPrice = oldPrice;
			NewName = newName;
			NewPrice = newPrice;
		}

		private ShipServiceUpdatedEvent(ShipServiceId aggregateId, long aggregateVersion, string oldName, double oldPrice, string newName, double newPrice) : base(aggregateId, aggregateVersion)
		{
			OldName = oldName;
			OldPrice = oldPrice;
			NewName = newName;
			NewPrice = newPrice;
		}

		public override IDomainEvent<ShipServiceId> WithAggregate(ShipServiceId aggregateId, long aggregateVersion)
		{
			return new ShipServiceUpdatedEvent(aggregateId, aggregateVersion, OldName, OldPrice, NewName, NewPrice);
		}
	}
}