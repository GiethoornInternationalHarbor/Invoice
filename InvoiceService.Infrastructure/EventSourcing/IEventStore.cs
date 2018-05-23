using InvoiceService.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public interface IEventStore
	{
		Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
	where TAggregateId : IAggregateId;

		Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
			where TAggregateId : IAggregateId;
	}
}
