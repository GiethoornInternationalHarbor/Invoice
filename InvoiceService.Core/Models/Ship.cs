using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Ship
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		[Required]
		public string Email { get; set; }
	}
}
