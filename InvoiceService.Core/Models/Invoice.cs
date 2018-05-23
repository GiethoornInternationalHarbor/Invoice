using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using InvoiceService.Core.EventSourcing.Ids;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Invoice : AggregateBase<InvoiceId>
	{
		public CustomerId CustomerId { get; set; }
		public RentalId RentalId { get; set; }

		/// <summary>
		/// Gets or sets the invoice lines.
		/// </summary>
		public List<InvoiceLine> Lines { get; set; }

		private Invoice()
		{
			Lines = new List<InvoiceLine>();
		}

		public Invoice(InvoiceId invoiceId, CustomerId customerId) : this()
		{
			if (invoiceId == null) throw new ArgumentNullException(nameof(invoiceId));
			if (customerId == null) throw new ArgumentNullException(nameof(customerId));
			RaiseEvent(new InvoiceCreatedEvent(invoiceId, customerId));
		}

		public void AddShipService(ShipServiceId shipServiceId, ShipId shipId)
		{
			if (shipServiceId == null)
			{
				throw new ArgumentNullException(nameof(shipServiceId));
			}
			RaiseEvent(new InvoiceShipServiceAddedEvent(Id, shipServiceId, shipId));
		}

		public void SetRental(RentalId rentalId)
		{
			RaiseEvent(new InvoiceRentalSetEvent(Id, rentalId));
		}

		internal void Apply(InvoiceCreatedEvent ev)
		{
			Id = ev.AggregateId;
			CustomerId = ev.CustomerId;
		}

		internal void Apply(InvoiceShipServiceAddedEvent ev)
		{
			Lines.Add(new InvoiceLine()
			{
				ServiceId = ev.ShipServiceId,
				ShipId = ev.ShipId
			});
		}

		internal void Apply(InvoiceRentalSetEvent ev)
		{
			RentalId = ev.RentalId;
		}
	}
}
