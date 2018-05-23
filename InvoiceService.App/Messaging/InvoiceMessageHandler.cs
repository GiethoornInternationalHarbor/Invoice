using System;
using System.Threading.Tasks;
using InvoiceService.App.Structs;
using InvoiceService.Core.Events;
using InvoiceService.Core.Messaging;
using InvoiceService.Core.Messaging.Events;
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
		private readonly IRentalRepository _rentalRepository;
		private readonly IMessagePublisher _messagePublisher;

		public InvoiceMessageHandler(IInvoiceRepository invoiceRepository,
									 ICustomerRepository customerRepository,
									 IShipRepository shipRepository,
									 IShipServiceRepository shipServiceRepository,
									 IRentalRepository rentalRepository,
									 IMessagePublisher messagePublisher)
		{
			_invoiceRepository = invoiceRepository;
			_customerRepository = customerRepository;
			_shipRepository = shipRepository;
			_shipServiceRepository = shipServiceRepository;
			_rentalRepository = rentalRepository;
			_messagePublisher = messagePublisher;
		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			switch (messageType)
			{
				case MessageTypes.CustomerUpdated:
					{
						return await HandleCustomerUpdated(message);
					}
				case MessageTypes.CustomerCreated:
					{
						return await HandleCustomerCreated(message);
					}
				case MessageTypes.RentalRequested:
					{
						return await HandleRentalRequested(message);
					}
				case MessageTypes.RentalAccepted:
					{
						return await HandleRentalAccepted(message);
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
			var receivedCustomer = JsonSerializer.Deserialize<CustomerMessageEvent>(message);

			await _customerRepository.UpdateCustomerAsync(receivedCustomer.CustomerId, receivedCustomer.Email, receivedCustomer.Address, receivedCustomer.PostalCode, receivedCustomer.Residence);

			return true;
		}

		private async Task<bool> HandleCustomerCreated(string message)
		{
			var receivedCustomer = JsonSerializer.Deserialize<CustomerMessageEvent>(message);

			await _customerRepository.CreateCustomerAsync(receivedCustomer.Email, receivedCustomer.Address, receivedCustomer.PostalCode, receivedCustomer.Residence);

			return true;
		}

		private async Task<bool> HandleRentalRequested(string message)
		{
			var receivedRental = JsonSerializer.Deserialize<Rental>(message);

			await _rentalRepository.CreateRental(receivedRental);

			return true;
		}

		private async Task<bool> HandleRentalAccepted(string message)
		{
			var customerRental = JsonSerializer.Deserialize<CustomerRental>(message);

			var customer = await _customerRepository.GetCustomerAsync(customerRental.CustomerId);

			var rental = await _rentalRepository.GetRental(customerRental.RentalId);

			await _invoiceRepository.UpdateInvoiceAsync(customer, rental);

			return true;
		}

		private async Task<bool> HandleServiceCompleted(string message)
		{
			var receivedShipServiceObject = JsonSerializer.Deserialize<ShipServiceObject>(message);
			var ship = await _shipRepository.GetShip(receivedShipServiceObject.ShipId);
			var shipService = await _shipServiceRepository.GetShipService(receivedShipServiceObject.ServiceId);
			var customer = await _customerRepository.GetCustomerAsync(receivedShipServiceObject.CustomerId);

			await _invoiceRepository.AddShipServiceLineAsync(customer, ship, shipService);

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

		private async Task<bool> HandleServiceUpdated(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipService>(message);

			await _shipServiceRepository.UpdateShipService(receivedShipService);

			return true;
		}

		private async Task<bool> HandleShipDocked(string message)
		{
			var receivedShip = Newtonsoft.Json.JsonConvert.DeserializeObject<ShipDockedMessageEvent>(message);

			await _shipRepository.CreateShip(receivedShip.CustomerId, receivedShip.Name);

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<CustomerShip>(message);

			var invoice = await _invoiceRepository.GetInvoiceByEmail(receivedShip.CustomerId);

			await _messagePublisher.PublishMessageAsync(MessageTypes.InvoiceCreated, invoice);

			await _shipRepository.DeleteShip(receivedShip.ShipId);

			return true;
		}
	}
}
