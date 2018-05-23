﻿using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.Events
{
	public class CustomerDeletedEvent : DomainEventBase<CustomerId>
	{
		internal CustomerDeletedEvent(CustomerId aggregateId) : base(aggregateId)
		{
		}

		private CustomerDeletedEvent(CustomerId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
		{
		}

		public override IDomainEvent<CustomerId> WithAggregate(CustomerId aggregateId, long aggregateVersion)
		{
			return new CustomerDeletedEvent(aggregateId, aggregateVersion);
		}
	}
}
