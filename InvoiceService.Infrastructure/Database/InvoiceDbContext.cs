using InvoiceService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure.Database
{
	public class InvoiceDbContext : DbContext
	{
		public InvoiceDbContext(DbContextOptions options)
			: base(options)
		{ }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Invoice> Invoices { get; set; }

		public DbSet<InvoiceLine> InvoiceLines { get; set; }

		public DbSet<Ship> Ships { get; set; }

		public DbSet<ShipService> ShipServices { get; set; }
	}
}
