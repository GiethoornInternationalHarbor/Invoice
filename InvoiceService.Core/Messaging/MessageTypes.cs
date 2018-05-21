namespace InvoiceService.Core.Messaging
{
	public enum MessageTypes
	{
		Unknown,
		InvoiceCreated,
		CustomerCreated,
		CustomedUpdated,   
		RentalAccepted,
		ServiceCreated,
		ServiceUpdated,
		ServiceDeleted,
		ServiceCompleted,
		ShipDocked,
		ShipUndocked
	}
}
