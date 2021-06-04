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
    [Route("[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> _logger;
        private readonly IContractManager _contractManager;

        public ContractsController(
            IContractManager contractManager,
            ILogger<ContractsController> logger)
        {
            _contractManager = contractManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в ContractController");
        }

		/// <summary> Получение информации о контракте по его Id </summary>
		/// <param name="id"> Id контракта </param>
		/// <returns> Информация о контракте </returns>
		[Authorize(Roles = "admin, user, client")]
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var result = await _contractManager.GetItem(id);

			return Ok(result);
		}

		/// <summary> Получение информации о нескольких контрактах</summary>
		/// <returns> Информацию о контрактах </returns>
		[Authorize(Roles = "admin, user, client")]
		[HttpGet]
		public async Task<IActionResult> GetItems()
		{
			var result = await _contractManager.GetItems();

			return Ok(result);
		}
		/// <summary> Создание нового контракта </summary>
		/// <param name="request"> Закпрос на создание контракта </param>
		/// <returns> Id созданного контракта </returns>
		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Create([FromBody] ContractRequest request)
		{
			var id = await _contractManager.Create(request);

			return Ok(id);
		}
		/// <summary> Изменение существующего контракта </summary>
		/// <param name="id"> Id изменяемого контракта </param>
		/// <param name="request"> Запрос на изменение контракта </param>
		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ContractRequest request)
		{
			await _contractManager.Update(id, request);

			return Ok();
		}

		/// <summary> Удаление контракта </summary>
		/// <param name="id"> Id удаляемого конракта </param>
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			await _contractManager.Delete(id);

			return Ok();
		}
	}
}