﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing
{
    public interface IEventSourcingAggregate<TAggregateId>
    {
		long Version { get; }
		void ApplyEvent(IDomainEvent<TAggregateId> @event, long version);
		IEnumerable<IDomainEvent<TAggregateId>> GetUncommittedEvents();
		void ClearUncommittedEvents();
	}
}
