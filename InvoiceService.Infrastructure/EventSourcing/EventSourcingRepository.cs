using InvoiceService.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public class EventSourcingRepository<TAggregate, TAggregateId> : IEventSourcingRepository<TAggregate, TAggregateId>
	  where TAggregate : AggregateBase<TAggregateId>, IAggregate<TAggregateId>
	  where TAggregateId : IAggregateId
	{
		private readonly IEventStore eventStore;

		public EventSourcingRepository(IEventStore eventStore)
		{
			this.eventStore = eventStore;
		}

		public async Task<TAggregate> GetByIdAsync(TAggregateId id)
		{
			var aggregate = CreateEmptyAggregate();
			IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

			foreach (var @event in await eventStore.ReadEventsAsync(id))
			{
				aggregatePersistence.ApplyEvent(@event.DomainEvent, @event.EventNumber);
			}
			return aggregate;
		}

		public async Task SaveAsync(TAggregate aggregate)
		{
			IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

			foreach (var @event in aggregatePersistence.GetUncommittedEvents())
			{
				await eventStore.AppendEventAsync(@event);
			}
			aggregatePersistence.ClearUncommittedEvents();
		}

		private TAggregate CreateEmptyAggregate()
		{
			return (TAggregate)typeof(TAggregate)
			  .GetConstructor(
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
				null, new Type[0], new ParameterModifier[0])
			  .Invoke(new object[0]);
		}
	}
}
