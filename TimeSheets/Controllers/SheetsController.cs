using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class SheetsController: TimeSheetBaseController
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly ISheetManager _sheetManager;
        private readonly IContractManager _contractManager;
        
        public SheetsController(
            ISheetManager sheetManager, 
            IContractManager contractManager, 
            ILogger<SheetsController> logger)
        {
            _sheetManager = sheetManager;
            _contractManager = contractManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в SheetsController");
        }

        /// <summary> Получение информации по карточке учета рабочего времени по ее Id </summary>
		/// <param name="id"> Id карточки учета рабочего времени </param>
		/// <returns> Информация о карточке учета рабочего времени </returns>	
        [Authorize(Roles = "user, admin")]
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var result = _sheetManager.GetItem(id);
            
            return Ok(result);
        }

        /// <summary> Получение информации о нескольких карточках учета рабочего времени </summary>
        /// <returns> Информация о карточках учета рабочего времени </returns>
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _sheetManager.GetItems();

            return Ok(result);
        }

        /// <summary> Создание новой карточки учета рабочего времени </summary>
		/// <param name="request"> Запрос на создание новой карточки учета рабочего времени</param>
		/// <returns> Id созданной карточки </returns>
		[Authorize(Roles = "admin, user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SheetRequest sheet)
        {
            var isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);

            if (isAllowedToCreate !=null && !(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {sheet.ContractId} is not active or not found.");
            }
            
            var id = await _sheetManager.Create(sheet);

            return Ok(id);
        }

        /// <summary> Изменение существующей карточки учета рабочего времени </summary>
		/// <param name="id"> Id изменяемой карточки учета рабочего времени </param>
		/// <param name="request"> Запрос на изменение карточки учета рабочего времени </param>
		[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SheetRequest sheet)
        {
            var isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);

            if (isAllowedToCreate !=null && !(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {sheet.ContractId} is not active or not found.");
            }

            await _sheetManager.Update(id, sheet);

            return Ok();
        }

        /// <summary> Удаление карточки учета рабочего времени </summary>
		/// <param name="id">Id удаляемой учета рабочего времени </param>
		[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _sheetManager.Delete(id);

            return Ok();
        }
    }
}