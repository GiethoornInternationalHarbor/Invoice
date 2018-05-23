using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.ReadModel
{
	public struct InvoiceLineReadModel
	{
		public double Price { get; set; }
		public string Description { get; set; }
	}
}
