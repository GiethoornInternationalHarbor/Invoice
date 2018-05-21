using System;

namespace InvoiceService.Core.Models
{
	public class ShipServiceObject
	{
		/// <summary>
		/// Gets or sets the ship identifier.
		/// </summary>
		public Guid ShipId { get; set; }

		/// <summary>
		/// Gets or sets the service identifier.
		/// </summary>
		public Guid ServiceId { get; set; }
	}
}
