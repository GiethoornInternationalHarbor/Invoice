﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceService.Core.Models
{
	public class ShipService
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		[Required]
		public double Price { get; set; }
	}
}
