using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.App.Structs
{
	public struct ShipServiceCudMessageEvent
	{
		/// <summary>
		/// Gets or sets the service identifier.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>
		/// The price.
		/// </value>
		public double Price { get; set; }
	}
}
