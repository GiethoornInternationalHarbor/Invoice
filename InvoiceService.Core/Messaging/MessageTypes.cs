namespace InvoiceService.Core.Messaging
{
	public enum MessageTypes
	{
		Unknown,
		InvoiceCreated,
		CustomerCreated,
		CustomedUpdated, 
		RentalRequested,
		RentalAccepted,
		ServiceCreated,
		ServiceUpdated,
		ServiceDeleted,
		ServiceRequested,
		ServiceCompleted,
		ShipDocked,
		ShipUndocked
	}
}
