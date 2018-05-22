using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InvoiceService.Infrastructure.Database
{
#if DEBUG

	public class InvoiceDbContextDesignTimeFactory : IDesignTimeDbContextFactory<InvoiceDbContext>
	{
		public InvoiceDbContext CreateDbContext(string[] args)
		{
			var optBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
			optBuilder.UseSqlServer("Server=.\\SQL_2017;Database=InvoiceService;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new InvoiceDbContext(optBuilder.Options);
		}
	}
#endif
}
