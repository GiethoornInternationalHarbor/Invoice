using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Rental
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		[Required]
		public double Price { get; set; }
	}
}
