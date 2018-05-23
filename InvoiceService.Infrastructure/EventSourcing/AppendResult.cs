using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Infrastructure.EventSourcing
{
	public class AppendResult
	{
		public long NextExpectedVersion { get; }

		public AppendResult(long nextExpectedVersion)
		{
			NextExpectedVersion = nextExpectedVersion;
		}
	}
}
