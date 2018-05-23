using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class ShipDockedEvent : DomainEventBase<ShipId>
	{
		public CustomerId CustomerId { get; private set; }
		public string Name { get; private set; }

		public ShipDockedEvent() { }

		internal ShipDockedEvent(ShipId aggregateId, CustomerId customerId, string name) : base(aggregateId)
		{
			CustomerId = customerId;
			Name = name;
		}

		private ShipDockedEvent(ShipId aggregateId, long aggregateVersion, CustomerId customerId, string name) : base(aggregateId, aggregateVersion)
		{
			CustomerId = customerId;
			Name = name;
		}

		public override IDomainEvent<ShipId> WithAggregate(ShipId aggregateId, long aggregateVersion)
		{
			return new ShipDockedEvent(aggregateId, aggregateVersion, CustomerId, Name);
		}
	}
}
