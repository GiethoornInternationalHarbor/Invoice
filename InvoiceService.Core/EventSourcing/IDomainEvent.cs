using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing
{
	public interface IDomainEvent<TAggregateId>
	{
		Guid EventId { get; }

		TAggregateId AggregateId { get; }

		long AggregateVersion { get; }
	}
}
