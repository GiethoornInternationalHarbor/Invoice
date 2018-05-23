using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.App.Structs
{
	public struct RentalMessageEvent
	{
		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public string CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the rental identifier.
		/// </summary>
		public string RentalId { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>
		/// The price.
		/// </value>
		public double Price { get; set; }
	}
}
