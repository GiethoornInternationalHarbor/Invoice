using InvoiceService.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public interface IEventSourcingRepository<TAggregate, TAggregateId> where TAggregate : IAggregate<TAggregateId>
	{
		/// <summary>
		/// Gets the aggregate by identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<TAggregate> GetByIdAsync(TAggregateId id);

		/// <summary>
		/// Saves the aggregate asynchronous.
		/// </summary>
		/// <param name="aggregate">The aggregate.</param>
		/// <returns></returns>
		Task SaveAsync(TAggregate aggregate);
	}
}
