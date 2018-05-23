using InvoiceService.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public class Event<TAggregateId>
	{
		public long EventNumber { get; }

		public IDomainEvent<TAggregateId> DomainEvent { get; }

		public Event(IDomainEvent<TAggregateId> domainEvent, long eventNumber)
		{
			DomainEvent = domainEvent;
			EventNumber = eventNumber;
		}
	}
}
