﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InvoiceService.Core.EventSourcing;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public class EventStoreEventStore : IEventStore
	{
		private readonly IEventStoreConnection connection;

		public EventStoreEventStore(IEventStoreConnection connection)
		{
			this.connection = connection;
		}

		public async Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event) where TAggregateId : IAggregateId
		{
			var eventData = new EventData(
								@event.EventId,
								@event.GetType().AssemblyQualifiedName,
								true,
								Serialize(@event),
								Encoding.UTF8.GetBytes("{}"));

			var writeResult = await connection.AppendToStreamAsync(
				@event.AggregateId.IdAsString(),
				@event.AggregateVersion == AggregateBase<TAggregateId>.NewAggregateVersion ? ExpectedVersion.NoStream : @event.AggregateVersion,
				eventData);

			return new AppendResult(writeResult.NextExpectedVersion);
		}

		public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id) where TAggregateId : IAggregateId
		{
			var ret = new List<Event<TAggregateId>>();
			StreamEventsSlice currentSlice;
			long nextSliceStart = StreamPosition.Start;

			do
			{
				currentSlice = await connection.ReadStreamEventsForwardAsync(id.IdAsString(), nextSliceStart, 200, false);
				if (currentSlice.Status != SliceReadStatus.Success)
				{
					throw new KeyNotFoundException($"Aggregate {id.IdAsString()} not found");
				}
				nextSliceStart = currentSlice.NextEventNumber;
				foreach (var resolvedEvent in currentSlice.Events)
				{
					ret.Add(new Event<TAggregateId>(Deserialize<TAggregateId>(resolvedEvent.Event.EventType, resolvedEvent.Event.Data), resolvedEvent.Event.EventNumber));
				}
			} while (!currentSlice.IsEndOfStream);

			return ret;
		}

		private IDomainEvent<TAggregateId> Deserialize<TAggregateId>(string eventType, byte[] data)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			return (IDomainEvent<TAggregateId>)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType(eventType), settings);
		}

		private byte[] Serialize<TAggregateId>(IDomainEvent<TAggregateId> @event)
		{
			return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
		}
	}
}
