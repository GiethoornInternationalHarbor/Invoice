using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using dotenv.net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Utf8Json.Resolvers;
using InvoiceService.Core.Messaging;
using InvoiceService.Infrastructure.DI;
using InvoiceService.App.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace InvoiceService.App
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			string filePath = ".env";

#if DEBUG
			filePath = Path.Combine(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("bin")), filePath);
#endif

			DotEnv.Config(throwOnError: false, filePath: filePath);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			IConfiguration config = new ConfigurationBuilder()
			.AddEnvironmentVariables()
			.Build();

			DIHelper.Setup(services, config);

			// Setup the app services
			services.AddTransient<IMessageHandlerCallback, InvoiceMessageHandler>();

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			CompositeResolver.RegisterAndSetAsDefault(new[]
			{
			   EnumResolver.UnderlyingValue,
			   StandardResolver.ExcludeNullCamelCase
			});

			// Get the message handler
			IMessageHandler messageHandler = app.ApplicationServices.GetService<IMessageHandler>();
			IMessageHandlerCallback messageHandlerCallback = app.ApplicationServices.GetService<IMessageHandlerCallback>();

			//DIHelper.OnServicesSetup(app.ApplicationServices);

			app.UseMvc();

			Task.Run(() =>
			{

				try
				{
					Console.WriteLine("Starting handler");
					messageHandler.Start(messageHandlerCallback);
					Console.WriteLine("Handler started");
				}
				catch (Exception ex)
				{
					// Error during staring
					Console.Error.WriteLine($"Error during starting message handler, message: {ex.Message}");
					Environment.Exit(1);
				}

				while (true)
				{
					Thread.Sleep(10000);
				}
			});

			Console.WriteLine("Invoice service started.");
		}
	}
}
