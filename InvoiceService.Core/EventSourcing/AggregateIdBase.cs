using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Core.EventSourcing
{
	public abstract class AggregateIdBase : IAggregateId
	{
		protected abstract string IdAsStringPrefix { get; }

		public Guid Id { get; private set; }

		private AggregateIdBase(Guid id)
		{
			Id = id;
		}

		protected AggregateIdBase(string id)
		{
			Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
		}

		protected AggregateIdBase() : this(Guid.NewGuid())
		{

		}

		public override string ToString()
		{
			return IdAsString();
		}

		public override bool Equals(object obj)
		{
			return Equals(Id, ((AggregateIdBase)obj).Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public string IdAsString()
		{
			return $"{IdAsStringPrefix}{Id.ToString()}";
		}
	}
}
