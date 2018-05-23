namespace InvoiceService.Core.EventSourcing
{
	public interface IAggregateId
	{
		string IdAsString();
	}
}
