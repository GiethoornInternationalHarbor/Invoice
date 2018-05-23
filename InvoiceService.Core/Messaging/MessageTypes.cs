namespace InvoiceService.Core.Messaging
{
	public enum MessageTypes
	{
		Unknown,
		InvoiceCreated,
		CustomerCreated,
		CustomerUpdated,
		CustomerDeleted,
		RentalRequested,
		RentalAccepted,
		RentalDeclined,
		ServiceCreated,
		ServiceUpdated,
		ServiceDeleted,
		ServiceCompleted,
		ShipDocked,
		ShipUndocked
	}
}
