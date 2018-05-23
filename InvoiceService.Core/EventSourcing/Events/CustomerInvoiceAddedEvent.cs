using InvoiceService.Core.EventSourcing.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing.Events
{
	internal class CustomerInvoiceAddedEvent : DomainEventBase<CustomerId>
	{
		public string InvoiceId { get; set; }

		public CustomerInvoiceAddedEvent()
		{

		}

		internal CustomerInvoiceAddedEvent(CustomerId aggregateId, string invoiceId) : base(aggregateId)
		{
			InvoiceId = invoiceId;
		}

		private CustomerInvoiceAddedEvent(CustomerId aggregateId, long aggregateVersion, string invoiceId) : base(aggregateId, aggregateVersion)
		{
			InvoiceId = invoiceId;
		}

		public override IDomainEvent<CustomerId> WithAggregate(CustomerId aggregateId, long aggregateVersion)
		{
			return new CustomerInvoiceAddedEvent(aggregateId, aggregateVersion, InvoiceId);
		}
	}

}
