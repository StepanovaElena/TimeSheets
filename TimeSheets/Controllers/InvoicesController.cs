using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class InvoicesController : TimeSheetBaseController
    {
        private readonly ILogger<InvoicesController> _logger;
        private readonly IInvoiceManager _invoiceManager;
		private readonly IContractManager _contractManager;

		public InvoicesController(
			IInvoiceManager invoiceManager,
            ILogger<InvoicesController> logger)
        {
            _invoiceManager = invoiceManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в InvoiceController");
        }

		/// <summary> Получение информации о счете по его Id </summary>
		/// <param name="id"> Id счета </param>
		/// <returns> Инорфмация о счете </returns>
		[Authorize(Roles = "admin, client")]
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var result = await _invoiceManager.GetItem(id);

			return Ok(result);
		}

		/// <summary> Получение информации о нескольких счетах </summary>
		/// <returns> Коллекция содержащая информацию о счетах </returns>
		[Authorize(Roles = "admin")]
		[HttpGet]
		public async Task<IActionResult> GetItems()
		{
			var result = await _invoiceManager.GetItems();

			return Ok(result);
		}

		/// <summary> Создание нового счета </summary>
		/// <param name="request"> Запрос на создание счета </param>
		/// <returns> Id созданного счета </returns>
		
		[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] InvoiceRequest request)
		{
			var isAllowedToCreate = await _contractManager.CheckContractIsActive(request.ContractId);

			if (isAllowedToCreate != null && !(bool)isAllowedToCreate)
			{
				return BadRequest($"Contract {request.ContractId} is not active or not found.");
			}

			var id = await _invoiceManager.Create(request);
			return Ok(id);
		}

		/// <summary> Изменение существующего счета </summary>
		/// <param name="id"> Id изменяемого счета </param>
		/// <param name="request"> Запрос на изменение счета </param>
		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] InvoiceRequest request)
		{
			await _invoiceManager.Update(id, request);

			return Ok();
		}

		/// <summary> Удаление счета </summary>
		/// <param name="id"> Id удаляемого счета </param>
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			await _invoiceManager.Delete(id);

			return Ok();
		}
	}
}