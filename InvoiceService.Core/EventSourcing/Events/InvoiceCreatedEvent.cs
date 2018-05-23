using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class InvoiceCreatedEvent : DomainEventBase<InvoiceId>
	{
		public CustomerId CustomerId { get; private set; }

		public InvoiceCreatedEvent() { }

		internal InvoiceCreatedEvent(InvoiceId aggregateId, CustomerId customerId) : base(aggregateId)
		{
			CustomerId = customerId;
		}

		private InvoiceCreatedEvent(InvoiceId aggregateId, long aggregateVersion, CustomerId customerId) : base(aggregateId, aggregateVersion)
		{
			CustomerId = customerId;
		}

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new InvoiceCreatedEvent(aggregateId, aggregateVersion, CustomerId);
		}
	}
}
