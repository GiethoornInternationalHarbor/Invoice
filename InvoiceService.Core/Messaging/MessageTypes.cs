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
		ServiceCreated,
		ServiceUpdated,
		ServiceDeleted,
		ServiceCompleted,
		ShipDocked,
		ShipUndocked
	}
}
