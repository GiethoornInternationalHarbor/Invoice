using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.ReadModel
{
	public struct InvoiceReadModel
	{
		public CustomerReadModel Customer { get; set; }
		public RentalReadModel Rental { get; set; }
		public IEnumerable<InvoiceLineReadModel> Lines { get; set; }
		public double TotalPrice { get; set; }
	}
}
