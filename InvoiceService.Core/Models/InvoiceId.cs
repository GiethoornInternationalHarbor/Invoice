using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.Models
{
	public class InvoiceId : IAggregateId
	{
		private const string IdAsStringPrefix = "Invoice-";

		public Guid Id { get; private set; }

		private InvoiceId(Guid id)
		{
			Id = id;
		}

		public InvoiceId(string id)
		{
			Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
		}

		public override string ToString()
		{
			return IdAsString();
		}

		public override bool Equals(object obj)
		{
			return obj is InvoiceId && Equals(Id, ((InvoiceId)obj).Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static InvoiceId NewInvoiceId()
		{
			return new InvoiceId(Guid.NewGuid());
		}

		public string IdAsString()
		{
			return $"{IdAsStringPrefix}{Id.ToString()}";
		}
	}
}
