namespace InvoiceService.Core.Messaging
{
	public enum MessageTypes
	{
		Unknown,
		InvoiceCreated,
		CustomerCreated,
		CustomerUpdated,
		RentalRequested,
		RentalAccepted,
		ServiceCreated,
		ServiceUpdated,
		ServiceDeleted,
		ServiceCompleted,
		ShipDocked,
		ShipUndocked
	}
}
