using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.Models
{
	public class ShipServiceId : IAggregateId
	{
		private const string IdAsStringPrefix = "ShipService-";

		public Guid Id { get; private set; }

		private ShipServiceId(Guid id)
		{
			Id = id;
		}

		public ShipServiceId(string id)
		{
			Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
		}

		public override string ToString()
		{
			return IdAsString();
		}

		public override bool Equals(object obj)
		{
			return obj is ShipServiceId && Equals(Id, ((ShipServiceId)obj).Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static ShipServiceId NewShipServiceId()
		{
			return new ShipServiceId(Guid.NewGuid());
		}

		public string IdAsString()
		{
			return $"{IdAsStringPrefix}{Id.ToString()}";
		}
	}
}
