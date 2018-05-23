using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.App.Structs
{
	public struct ShipDockedMessageEvent
	{
		public string CustomerId { get; set; }
		public string Name { get; set; }
	}
}
