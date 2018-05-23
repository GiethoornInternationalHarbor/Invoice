using InvoiceService.Core.EventSourcing;

namespace InvoiceService.Core.Models
{
	public class RentalId : AggregateIdBase
	{
		protected override string IdAsStringPrefix => "Rental-";

		public RentalId() : base() { }

		public RentalId(string id) : base(id)
		{
		}
	}
}
