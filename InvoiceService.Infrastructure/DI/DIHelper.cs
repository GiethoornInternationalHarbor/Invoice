using Microsoft.Extensions.DependencyInjection;
using System;
using InvoiceService.Core.Repositories;
using InvoiceService.Infrastructure.Repositories;
using InvoiceService.Core.Messaging;
using InvoiceService.Infrastructure.Messaging;
using Polly;
using EventStore.ClientAPI;
using InvoiceService.Infrastructure.EventSourcing;
using InvoiceService.Core.Models;
using InvoiceService.Core.EventSourcing.Ids;
using Microsoft.Extensions.Configuration;

namespace InvoiceService.Infrastructure.DI
{
	public static class DIHelper
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<IInvoiceRepository, InvoiceRepository>();
			services.AddTransient<IShipRepository, ShipRepository>();
			services.AddTransient<IShipServiceRepository, ShipServiceRepository>();
			services.AddTransient<IRentalRepository, RentalRepository>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));

			services.AddSingleton(x => EventStoreConnection.Create(new Uri(configuration.GetSection("EVENT_STORE_URL").Value)));
			services.AddTransient<IEventSourcingRepository<Customer, CustomerId>, EventSourcingRepository<Customer, CustomerId>>();
			services.AddTransient<IEventSourcingRepository<Ship, ShipId>, EventSourcingRepository<Ship, ShipId>>();
			services.AddTransient<IEventSourcingRepository<ShipService, ShipServiceId>, EventSourcingRepository<ShipService, ShipServiceId>>();
			services.AddTransient<IEventSourcingRepository<Rental, RentalId>, EventSourcingRepository<Rental, RentalId>>();
			services.AddTransient<IEventSourcingRepository<Invoice, InvoiceId>, EventSourcingRepository<Invoice, InvoiceId>>();
			services.AddSingleton<IEventStore, EventStoreEventStore>();
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Connecting to EventStore");
			Policy
			 .Handle<Exception>()
			 .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) =>
			 {
				 Console.Error.WriteLine("Error connecting to EventStore. Retrying in 5 sec.");
			 })
			 .Execute(() =>
			 {
				 IEventStoreConnection eventStoreConnection = serviceProvider.GetService<IEventStoreConnection>();
				 eventStoreConnection.ConnectAsync().Wait();
				 Console.WriteLine("Completed connecting to EventStore");
			 });
		}
	}
}
