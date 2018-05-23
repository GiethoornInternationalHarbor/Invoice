using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.EventSourcing.Ids
{
	public class ShipServiceId : AggregateIdBase
	{
		protected override string IdAsStringPrefix => "ShipService-";

		/// <summary>
		/// Initializes a new instance of the <see cref="ShipServiceId"/> class.
		/// Initialized with Guid.NewGuid()
		/// </summary>
		public ShipServiceId() : base() { }

		public ShipServiceId(string id) : base(id)
		{
		}
	}
}
