using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	public class RentalAcceptedEvent : DomainEventBase<RentalId>
	{
		public double OldPrice { get; set; }

		public double NewPrice { get; set; }

		public RentalAcceptedEvent()
		{

		}

		internal RentalAcceptedEvent(RentalId aggregateId, double oldPrice, double newPrice) : base(aggregateId)
		{
			OldPrice = oldPrice;
			NewPrice = newPrice;
		}

		private RentalAcceptedEvent(RentalId aggregateId, long aggregateVersion, double oldPrice, double newPrice) : base(aggregateId, aggregateVersion)
		{
			OldPrice = oldPrice;
			NewPrice = newPrice;
		}

		public override IDomainEvent<RentalId> WithAggregate(RentalId aggregateId, long aggregateVersion)
		{
			return new RentalAcceptedEvent(aggregateId, aggregateVersion, OldPrice, NewPrice);
		}
	}
}
