using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.App.Structs
{
	public struct ShipDockedMessageEvent
	{
		public string CustomerId { get; set; }
		public string ShipId { get; set; }
		public string ShipName { get; set; }
	}
}
