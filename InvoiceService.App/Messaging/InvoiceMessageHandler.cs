using System;
using System.Threading.Tasks;
using InvoiceService.Core.Messaging;
using InvoiceService.Core.Models;
using InvoiceService.Core.Repositories;
using Utf8Json;

namespace InvoiceService.App.Messaging
{
	public class InvoiceMessageHandler : IMessageHandlerCallback
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IShipRepository _shipRepository;
		private readonly IShipServiceRepository _shipServiceRepository;
		private readonly IMessagePublisher _messagePublisher;

		public InvoiceMessageHandler(IInvoiceRepository invoiceRepository,
									 ICustomerRepository customerRepository,
									 IShipRepository shipRepository,
									 IShipServiceRepository shipServiceRepository,
									 IMessagePublisher messagePublisher)
		{
			_invoiceRepository = invoiceRepository;
			_customerRepository = customerRepository;
			_shipRepository = shipRepository;
			_shipServiceRepository = shipServiceRepository;
			_messagePublisher = messagePublisher;
		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			switch (messageType)
			{
				case MessageTypes.CustomedUpdated:
					{
						return await HandleCustomerUpdated(message);
					}
				case MessageTypes.CustomerCreated:
					{
						return await HandleCustomerCreated(message);
					}
				case MessageTypes.RentalAccepted:
					{
						return await HandleRentalAccepted(message);
					}
				case MessageTypes.RentalRequested:
					{
						return await HandleRentalRequested(message);
					}
				case MessageTypes.ServiceCompleted:
					{
						return await HandleServiceCompleted(message);
					}
				case MessageTypes.ServiceCreated:
					{
						return await HandleServiceCreated(message);
					}
				case MessageTypes.ServiceDeleted:
					{
						return await HandleServiceDeleted(message);
					}
				case MessageTypes.ServiceRequested:
					{
						return await HandleServiceRequested(message);
					}
				case MessageTypes.ServiceUpdated:
					{
						return await HandleServiceUpdated(message);
					}
				case MessageTypes.ShipDocked:
					{
						return await HandleShipDocked(message);
					}
				case MessageTypes.ShipUndocked:
					{
						return await HandleShipUndocked(message);
					}
			}

			return true;
		}

		private async Task<bool> HandleCustomerUpdated(string message)
		{
			var receivedCustomer = JsonSerializer.Deserialize<Customer>(message);

			await _customerRepository.UpdateCustomerAsync(receivedCustomer);

			return true;
		}

		private async Task<bool> HandleCustomerCreated(string message)
		{
			var receivedCustomer = JsonSerializer.Deserialize<Customer>(message);

			await _customerRepository.CreateCustomerAsync(receivedCustomer);

			return true;
		}

		private async Task<bool> HandleRentalAccepted(string message)
		{
			var receivedRental = JsonSerializer.Deserialize<Rental>(message);

			await _invoiceRepository.UpdateInvoiceAsync(receivedRental);

			return true;
		}

		private async Task<bool> HandleRentalRequested(string message)
		{
			var receivedRental = JsonSerializer.Deserialize<Rental>(message);

			await _invoiceRepository.UpdateInvoiceAsync(receivedRental);

			return true;
		}

		private async Task<bool> HandleServiceCompleted(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			await _shipRepository.UpdateShip(receivedShip);

			return true;
		}

		private async Task<bool> HandleServiceCreated(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipService>(message);

			await _shipServiceRepository.CreateShipService(receivedShipService);

			return true;
		}

		private async Task<bool> HandleServiceDeleted(string message)
		{
			var shipServiceId = Guid.Parse(message);

			await _shipServiceRepository.DeleteShipService(shipServiceId);

			return true;
		}

		private async Task<bool> HandleServiceRequested(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			await _shipRepository.UpdateShip(receivedShip);

			return true;
		}

		private async Task<bool> HandleServiceUpdated(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipService>(message);

			await _shipServiceRepository.UpdateShipService(receivedShipService);

			return true;
		}

		private async Task<bool> HandleShipDocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			await _shipRepository.CreateShip(receivedShip);

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			var invoice = await _invoiceRepository.GetInvoice(receivedShip.Id);

			await _messagePublisher.PublishMessageAsync(MessageTypes.InvoiceCreated, invoice);

			await _shipRepository.DeleteShip(receivedShip.Id);

			return true;
		}
	}
}
