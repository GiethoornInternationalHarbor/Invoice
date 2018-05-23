using InvoiceService.Core.EventSourcing;
using System;

namespace InvoiceService.Core.Models
{
	public class ShipId : IAggregateId
	{
		private const string IdAsStringPrefix = "Ship-";

		public Guid Id { get; private set; }

		private ShipId(Guid id)
		{
			Id = id;
		}

		public ShipId(string id)
		{
			Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
		}

		public override string ToString()
		{
			return IdAsString();
		}

		public override bool Equals(object obj)
		{
			return obj is ShipId && Equals(Id, ((ShipId)obj).Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static ShipId NewShipServiceId()
		{
			return new ShipId(Guid.NewGuid());
		}

		public string IdAsString()
		{
			return $"{IdAsStringPrefix}{Id.ToString()}";
		}
	}
}