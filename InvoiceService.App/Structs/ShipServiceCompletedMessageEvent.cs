using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.App.Structs
{
	public struct ShipServiceCompletedMessageEvent
	{
		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public string CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the ship identifier.
		/// </summary>
		public string ShipId { get; set; }

		/// <summary>
		/// Gets or sets the service identifier.
		/// </summary>
		public string ServiceId { get; set; }
	}
}
