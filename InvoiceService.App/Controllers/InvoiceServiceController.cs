using InvoiceService.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InvoiceService.App.Controllers
{
	[Route("api/invoice")]
	public class InvoiceServiceController : ControllerBase
	{
		private readonly IInvoiceRepository _invoiceRepository;

		public InvoiceServiceController(IInvoiceRepository invoiceRepository)
		{
			_invoiceRepository = invoiceRepository;
		}

		[HttpGet("overview/{id}")]
		public async Task<IActionResult> GetOverview(string id)
		{
			IActionResult response = null;

			try
			{
				if (!string.IsNullOrWhiteSpace(id))
				{
					var invoice = await _invoiceRepository.GetInvoicesForCustomer(id);
					response = Ok(invoice);
				}
				else
				{
					response = NotFound();
				}
			}
			catch (Exception)
			{
				response = StatusCode(500);
			}

			return response;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			IActionResult response = null;

			try
			{
				if (!string.IsNullOrWhiteSpace(id))
				{
					var invoice = await _invoiceRepository.GetInvoice(id);
					response = Ok(invoice);
				}
				else
				{
					response = NotFound();
				}
			}
			catch (Exception)
			{
				response = StatusCode(500);
			}

			return response;
		}
	}
}
