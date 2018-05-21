using System;

namespace InvoiceService.Core.Models
{
	public class CustomerShip
	{
		/// <summary>
		/// Gets or sets the ship identifier.
		/// </summary>
		public Guid ShipId { get; set; }

		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public string CustomerId { get; set; }
	}
}
