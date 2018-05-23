using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.ReadModel
{
	public struct CustomerReadModel
	{
		public string Email { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
		public string Residence { get; set; }
	}
}
