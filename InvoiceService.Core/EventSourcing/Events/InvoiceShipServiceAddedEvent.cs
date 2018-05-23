using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class InvoiceShipServiceAddedEvent : DomainEventBase<InvoiceId>
	{
		public ShipServiceId ShipServiceId { get; private set; }

		public ShipId ShipId { get; private set; }

		internal InvoiceShipServiceAddedEvent(ShipServiceId shipServiceId, ShipId shipId) : base()
		{
			ShipServiceId = shipServiceId;
			ShipId = shipId;
		}

		private InvoiceShipServiceAddedEvent(InvoiceId aggregateId, long aggregateVersion, ShipServiceId shipServiceId, ShipId shipId) : base(aggregateId, aggregateVersion)
		{
			ShipServiceId = shipServiceId;
			ShipId = shipId;
		}

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new InvoiceShipServiceAddedEvent(aggregateId, aggregateVersion, ShipServiceId, ShipId);
		}
	}
}
