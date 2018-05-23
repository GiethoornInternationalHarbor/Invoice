using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	public class RentalRequestedEvent : DomainEventBase<RentalId>
	{
		public double Price { get; set; }
		public CustomerId CustomerId { get; set; }

		public RentalRequestedEvent()
		{

		}

		internal RentalRequestedEvent(RentalId aggregateId, CustomerId customerId, double price) : base(aggregateId)
		{
			Price = price;
			CustomerId = customerId;
		}

		private RentalRequestedEvent(RentalId aggregateId, long aggregateVersion, CustomerId customerId, double price) : base(aggregateId, aggregateVersion)
		{
			Price = price;
			CustomerId = customerId;
		}

		public override IDomainEvent<RentalId> WithAggregate(RentalId aggregateId, long aggregateVersion)
		{
			return new RentalRequestedEvent(aggregateId, aggregateVersion, CustomerId, Price);
		}
	}
}
