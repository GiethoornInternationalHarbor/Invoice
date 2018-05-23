using System;
using System.Threading.Tasks;
using InvoiceService.App.Structs;
using InvoiceService.Core.EventSourcing.Ids;
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
				case MessageTypes.CustomerDeleted:
					{
						return await HandleCustomerDeleted(message);
					}
				case MessageTypes.RentalDeclined:
					return await HandleRentalDeclined(message);
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

			await _customerRepository.CreateCustomerAsync(receivedCustomer.CustomerId, receivedCustomer.Email, receivedCustomer.Address, receivedCustomer.PostalCode, receivedCustomer.Residence);

			return true;
		}

		private async Task<bool> HandleCustomerDeleted(string message)
		{
			var receivedCustomer = JsonSerializer.Deserialize<CustomerMessageEvent>(message);

			await _customerRepository.DeleteCustomerAsync(receivedCustomer.CustomerId);

			return true;
		}

		private async Task<bool> HandleRentalRequested(string message)
		{
			var receivedRental = JsonSerializer.Deserialize<RentalMessageEvent>(message);

			RentalId rentalId = await _rentalRepository.CreateRental(receivedRental.CustomerId, receivedRental.RentalId, receivedRental.Price);
			await _invoiceRepository.CreateInvoice(receivedRental.CustomerId, rentalId.ToString());

			return true;
		}

		private async Task<bool> HandleRentalAccepted(string message)
		{
			var customerRental = JsonSerializer.Deserialize<RentalMessageEvent>(message);

			await _rentalRepository.Accept(customerRental.RentalId, customerRental.Price);

			return true;
		}

		private async Task<bool> HandleRentalDeclined(string message)
		{
			var customerRental = JsonSerializer.Deserialize<RentalMessageEvent>(message);

			await _rentalRepository.DeleteRental(customerRental.RentalId);

			return true;
		}

		private async Task<bool> HandleServiceCompleted(string message)
		{
			var receivedShipServiceObject = JsonSerializer.Deserialize<ShipServiceCompletedMessageEvent>(message);
			var invoice = await _invoiceRepository.GetLastInvoiceForCustomer(receivedShipServiceObject.CustomerId);
			await _invoiceRepository.AddShipServiceLineAsync(invoice.Id.ToString(), receivedShipServiceObject.ShipId, receivedShipServiceObject.ServiceId);

			return true;
		}

		private async Task<bool> HandleServiceCreated(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipServiceCudMessageEvent>(message);

			await _shipServiceRepository.CreateShipService(receivedShipService.ServiceId, receivedShipService.Name, receivedShipService.Price);

			return true;
		}

		private async Task<bool> HandleServiceDeleted(string message)
		{
			var shipService = JsonSerializer.Deserialize<ShipServiceCudMessageEvent>(message);

			await _shipServiceRepository.DeleteShipService(shipService.ServiceId);

			return true;
		}

		private async Task<bool> HandleServiceUpdated(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipServiceCudMessageEvent>(message);

			await _shipServiceRepository.UpdateShipService(receivedShipService.ServiceId, receivedShipService.Name, receivedShipService.Price);

			return true;
		}

		private async Task<bool> HandleShipDocked(string message)
		{
			var receivedShip = Newtonsoft.Json.JsonConvert.DeserializeObject<ShipDockedMessageEvent>(message);

			await _shipRepository.CreateShip(receivedShip.ShipId, receivedShip.CustomerId, receivedShip.ShipName);

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<ShipUndockedMessageEvent>(message);

			await _shipRepository.DeleteShip(receivedShip.ShipId);

			return true;
		}
	}
}
