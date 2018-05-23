using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing
{
	public interface IAggregateId
	{
		string IdAsString();
	}
}
