using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.EventSourcing.Ids
{
	public class CustomerId : AggregateIdBase
	{
		protected override string IdAsStringPrefix => "Customer-";

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerId"/> class.
		/// Initialized with Guid.NewGuid()
		/// </summary>
		public CustomerId() : base() { }

		public CustomerId(string id) : base(id)
		{
		}
	}
}
