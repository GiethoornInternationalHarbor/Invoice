using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.Models
{
	public class ShipId : AggregateIdBase
	{
		protected override string IdAsStringPrefix => "Ship-";

		/// <summary>
		/// Initializes a new instance of the <see cref="ShipId"/> class.
		/// Initialized with Guid.NewGuid()
		/// </summary>
		public ShipId() : base() { }

		public ShipId(string id) : base(id)
		{
		}
	}
}