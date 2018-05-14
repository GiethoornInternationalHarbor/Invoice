using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Invoice
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		[Required]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the ship.
		/// </summary>
		[Required]
		public Ship Ship { get; set; }

		/// <summary>
		/// Gets or sets the customer.
		/// </summary>
		[Required]
		public Customer Customer { get; set; }

		/// <summary>
		/// Gets or sets the invoice lines.
		/// </summary>
		[Required]
		public List<InvoiceLine> Lines { get; set; }

		/// <summary>
		/// Gets or sets the invoice price.
		/// </summary>
		[Required]
		public double Price { get; set; }
	}
}
