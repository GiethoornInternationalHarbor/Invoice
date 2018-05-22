using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceService.Infrastructure.Database
{
	public class InvoiceDbContextFactory
	{
		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		protected string ConnectionString { get; set; }

		public InvoiceDbContextFactory(string connectionString)
		{

		}

		/// <summary>
		/// Creates the database context.
		/// </summary>
		/// <returns></returns>
		public InvoiceDbContext CreateDbContext()
		{
			var optBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
			optBuilder.UseSqlServer(ConnectionString);

			return new InvoiceDbContext(optBuilder.Options);
		}
	}
}
