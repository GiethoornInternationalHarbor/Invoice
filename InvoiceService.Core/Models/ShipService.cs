using InvoiceService.Core.EventSourcing;
using InvoiceService.Core.EventSourcing.Events;
using InvoiceService.Core.EventSourcing.Ids;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class ShipService : AggregateBase<ShipServiceId>
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		protected string Name { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		protected double Price { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		protected bool IsDeleted { get; set; }

		private ShipService() { }

		public ShipService(ShipServiceId serviceId, string name, double price)
		{
			if (serviceId == null) throw new ArgumentNullException(nameof(serviceId));
			RaiseEvent(new ShipServiceCreatedEvent(serviceId, name, price));
		}

		public void Update(string name, double price)
		{
			if (!IsDeleted)
			{
				RaiseEvent(new ShipServiceUpdatedEvent(Id, Name, Price, name, price));
			}
		}

		public void Delete()
		{
			if (!IsDeleted)
			{
				RaiseEvent(new ShipServiceDeletedEvent(Id));
			}
		}

		internal void Apply(ShipServiceCreatedEvent ev)
		{
			Id = ev.AggregateId;
			Name = ev.Name;
			Price = ev.Price;
		}

		internal void Apply(ShipServiceUpdatedEvent ev)
		{
			Name = ev.NewName;
			Price = ev.NewPrice;
		}

		internal void Apply(ShipServiceDeletedEvent ev)
		{
			IsDeleted = true;
		}
	}
}
