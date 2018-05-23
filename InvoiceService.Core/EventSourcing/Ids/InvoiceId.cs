using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.EventSourcing.Ids
{
	public class InvoiceId : AggregateIdBase
	{
		protected override string IdAsStringPrefix => "Invoice-";

		/// <summary>
		/// Initializes a new instance of the <see cref="InvoiceId"/> class.
		/// Initialized with Guid.NewGuid()
		/// </summary>
		public InvoiceId() : base() { }

		public InvoiceId(string id) : base(id)
		{
		}
	}
}
