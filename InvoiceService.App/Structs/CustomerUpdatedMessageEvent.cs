using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceService.App.Structs
{
	public class CustomerUpdatedMessageEvent : BaseCustomerMessageEvent
	{
		public string CustomerId { get; set; }
	}
}
