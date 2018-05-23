using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Ids;
using InvoiceService.Core.Models;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class InvoiceRentalSetEvent : DomainEventBase<InvoiceId>
	{
		public RentalId RentalId { get; set; }

		public InvoiceRentalSetEvent()
		{

		}

		internal InvoiceRentalSetEvent(InvoiceId aggregateId, RentalId rentalId) : base(aggregateId)
		{
			RentalId = rentalId;
		}

		private InvoiceRentalSetEvent(InvoiceId aggregateId, long aggregateVersion, RentalId rentalId) : base(aggregateId, aggregateVersion)
		{
			RentalId = rentalId;
		}

		public override IDomainEvent<InvoiceId> WithAggregate(InvoiceId aggregateId, long aggregateVersion)
		{
			return new InvoiceRentalSetEvent(aggregateId, aggregateVersion, RentalId);
		}
	}
}