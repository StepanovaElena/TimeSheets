using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto;

namespace TimeSheets.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class SheetsController: ControllerBase
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly ISheetManager _sheetManager;
        private readonly IContractManager _contractManager;
        
        public SheetsController(ISheetManager sheetManager, IContractManager contractManager, ILogger<SheetsController> logger)
        {
            _sheetManager = sheetManager;
            _contractManager = contractManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в SheetsController");
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] Guid id)
        {
            var result = _sheetManager.GetItem(id);
            
            return Ok(result);
        }        
        
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _sheetManager.GetItems();

            return Ok(result);
        }

        /// <summary> Возвращает запись табеля </summary>
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

        /// <summary> Обновляет запись табеля </summary>
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
    }
}