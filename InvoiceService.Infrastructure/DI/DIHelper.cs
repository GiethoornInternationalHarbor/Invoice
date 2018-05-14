using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using InvoiceService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Repositories;
using InvoiceService.Core.Messaging;

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

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Connecting to database and migrating if required");
			var dbContext = serviceProvider.GetService<InvoiceDbContext>();
			dbContext.Database.Migrate();
			Console.WriteLine("Completed connecting to database");
		}
	}
}
