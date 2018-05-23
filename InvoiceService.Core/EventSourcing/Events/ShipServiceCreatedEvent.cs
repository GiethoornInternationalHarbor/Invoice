using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class ShipServiceCreatedEvent : DomainEventBase<ShipServiceId>
	{
		public string Name { get; set; }
		public double Price { get; set; }

		public ShipServiceCreatedEvent()
		{

		}

		internal ShipServiceCreatedEvent(ShipServiceId aggregateId, string name, double price) : base(aggregateId)
		{
			Name = name;
			Price = price;
		}

		private ShipServiceCreatedEvent(ShipServiceId aggregateId, long aggregateVersion, string name, double price) : base(aggregateId, aggregateVersion)
		{
			Name = name;
			Price = price;
		}

		public override IDomainEvent<ShipServiceId> WithAggregate(ShipServiceId aggregateId, long aggregateVersion)
		{
			return new ShipServiceCreatedEvent(aggregateId, aggregateVersion, Name, Price);
		}

	}
}
