using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class Customer
	{
		[Required]
		[Key]
		public string Email { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string PostalCode { get; set; }

		[Required]
		public string Residence { get; set; }
	}
}
