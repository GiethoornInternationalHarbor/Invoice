using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Repositories;
using InvoiceService.Core.Messaging;
using InvoiceService.Infrastructure.Messaging;
using Polly;
using System.Threading.Tasks;

namespace InvoiceService.Infrastructure.DI
{
	public static class DIHelper
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<InvoiceDbContext>(opt => opt.UseSqlServer(configuration.GetSection("DB_CONNECTION_STRING").Value));

			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<IInvoiceRepository, InvoiceRepository>();
			services.AddTransient<IShipRepository, ShipRepository>();
			services.AddTransient<IShipServiceRepository, ShipServiceRepository>();
			services.AddTransient<IRentalRepository, RentalRepository>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static Task OnServicesSetup(IServiceProvider serviceProvider)
		{
			return Task.Run(() =>
			{
				Console.WriteLine("Connecting to database and migrating if required");
				Policy
				 .Handle<Exception>()
				 .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) =>
				 {
					 Console.Error.WriteLine("Error connecting to database. Retrying in 5 sec.");
				 })
				 .Execute(() =>
				 {
					 var dbContext = serviceProvider.GetService<InvoiceDbContext>();
					 dbContext.Database.Migrate();
					 Console.WriteLine("Completed connecting to database");
				 });
			});
		}
	}
}
