using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.ReadModel
{
	public struct InvoiceOverviewReadModel
	{
		public string CustomerId { get; set; }
		public IEnumerable<string> Invoices { get; set; }
	}
}
