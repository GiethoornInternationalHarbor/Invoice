using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using System;

namespace InvoiceService.Core.Models
{
	public class Ship : AggregateBase<ShipId>
	{
		private CustomerId CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		private string Name { get; set; }

		public Ship(ShipId shipId, CustomerId customerId, string name)
		{
			if (shipId == null) throw new ArgumentNullException(nameof(shipId));
			if (customerId == null) throw new ArgumentNullException(nameof(customerId));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
			RaiseEvent(new ShipDockedEvent(shipId, customerId, name));
		}

		internal void Apply(ShipDockedEvent ev)
		{
			Id = ev.AggregateId;
			CustomerId = ev.CustomerId;
			Name = ev.Name;
		}
	}
}
