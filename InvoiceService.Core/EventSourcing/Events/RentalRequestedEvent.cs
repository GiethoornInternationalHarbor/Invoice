using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	public class RentalRequestedEvent : DomainEventBase<RentalId>
	{
		public double Price { get; set; }

		public RentalRequestedEvent()
		{

		}

		internal RentalRequestedEvent(RentalId aggregateId, double price) : base(aggregateId)
		{
			Price = price;
		}

		private RentalRequestedEvent(RentalId aggregateId, long aggregateVersion, double price) : base(aggregateId, aggregateVersion)
		{
			Price = price;
		}

		public override IDomainEvent<RentalId> WithAggregate(RentalId aggregateId, long aggregateVersion)
		{
			return new RentalRequestedEvent(aggregateId, aggregateVersion, Price);
		}
	}
}
