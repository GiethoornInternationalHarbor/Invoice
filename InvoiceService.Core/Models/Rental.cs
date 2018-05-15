using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Rental
	{
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		[Required]
		public double Price { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Rental"/> is accepted.
		/// </summary>
		/// <value>
		///   <c>true</c> if accepted; otherwise, <c>false</c>.
		/// </value>
		[Required]
		public bool Accepted { get; set; }
	}
}
