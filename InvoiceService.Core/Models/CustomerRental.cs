using System;

namespace InvoiceService.Core.Models
{
	public class CustomerRental
	{
		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public string CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the rental identifier.
		/// </summary>
		public Guid RentalId { get; set; }
	}
}
