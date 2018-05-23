namespace InvoiceService.Core.EventSourcing
{
	public interface IAggregate<TId>
	{
		TId Id { get; }
	}
}
