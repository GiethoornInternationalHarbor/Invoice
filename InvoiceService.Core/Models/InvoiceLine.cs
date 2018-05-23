using InvoiceService.Core.EventSourcing.Ids;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class InvoiceLine
	{
		public ShipServiceId ServiceId { get; set; }

		public ShipId ShipId { get; set; }
	}
}
