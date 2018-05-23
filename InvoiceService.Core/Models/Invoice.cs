using InvoiceService.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Invoice : AggregateBase<InvoiceId>
	{
		private CustomerId CustomerId { get; set; }

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

		public void AddShipService(ShipServiceId shipServiceId)
		{
			if (shipServiceId == null)
			{
				throw new ArgumentNullException(nameof(shipServiceId));
			}
			RaiseEvent(new ShipServiceAddedEvent(shipServiceId));
		}

		internal void Apply(InvoiceCreatedEvent ev)
		{
			Id = ev.AggregateId;
			CustomerId = ev.CustomerId;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		//[Key]
		//[Required]
		//public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the ship.
		/// </summary>
		[Required]
		public Ship Ship { get; set; }

		/// <summary>
		/// Gets or sets the customer.
		/// </summary>
		[Required]
		public Customer Customer { get; set; }

		/// <summary>
		/// Gets or sets the invoice lines.
		/// </summary>
		[Required]
		public List<InvoiceLine> Lines { get; set; }

		/// <summary>
		/// Gets or sets the invoice price.
		/// </summary>
		[Required]
		public double Price { get; set; }
	}
}
