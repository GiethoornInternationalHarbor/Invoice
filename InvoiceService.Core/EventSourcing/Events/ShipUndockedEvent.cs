using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class ShipUndockedEvent : DomainEventBase<ShipId>
	{
		public ShipUndockedEvent() { }
		internal ShipUndockedEvent(ShipId aggregateId) : base(aggregateId)
		{
		}

		private ShipUndockedEvent(ShipId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
		{
		}

		public override IDomainEvent<ShipId> WithAggregate(ShipId aggregateId, long aggregateVersion)
		{
			return new ShipUndockedEvent(aggregateId, aggregateVersion);
		}
	}
}
