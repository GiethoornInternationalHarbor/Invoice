using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using InvoiceService.Core.EventSourcing.Ids;
using System;

namespace InvoiceService.Core.Models
{
	public class Rental : AggregateBase<RentalId>
	{
		public CustomerId CustomerId { get; set; }
		public double Price { get; set; }

		public bool IsDeclined { get; set; }

		private Rental() { }

		public Rental(RentalId rentalId, CustomerId customerId, double price)
		{
			if (rentalId == null) throw new ArgumentNullException(nameof(rentalId));
			RaiseEvent(new RentalRequestedEvent(rentalId, customerId, price));
		}

		public void Accept(double price)
		{
			if (!IsDeclined)
			{
				RaiseEvent(new RentalAcceptedEvent(Id, Price, price));
			}
		}

		public void Decline()
		{
			if (!IsDeclined)
			{
				RaiseEvent(new RentalDeclinedEvent(Id));
			}
		}

		internal void Apply(RentalRequestedEvent ev)
		{
			Id = ev.AggregateId;
			Price = ev.Price;
		}

		internal void Apply(RentalAcceptedEvent ev)
		{
			Price = ev.NewPrice;
		}

		internal void Apply(RentalDeclinedEvent ev)
		{
			IsDeclined = true;
		}
	}
}
