using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.Messaging.Events
{
	public struct ShipDockedMessageEvent
	{
		public string CustomerId { get; set; }
		public string Name { get; set; }
	}
}
