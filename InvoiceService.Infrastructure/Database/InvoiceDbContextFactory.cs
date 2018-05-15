using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#if DEBUG
namespace InvoiceService.Infrastructure.Database
{
	public class InvoiceDbContextFactory : IDesignTimeDbContextFactory<InvoiceDbContext>
	{
		public InvoiceDbContext CreateDbContext(string[] args)
		{
			var optBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
			optBuilder.UseSqlServer("Server=.\\SQL_2017;Database=InvoiceService;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new InvoiceDbContext(optBuilder.Options);
		}
	}
}
#endif
