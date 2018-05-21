using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceService.Core.Models
{
	public class InvoiceLine
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>											
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the type of the invoice.
		/// </summary>
		/// <value>
		/// The type of the invoice.
		/// </value>
		[Required]
		public InvoiceTypes InvoiceType { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		[Required]
		public double Price { get; set; }
	}
}
