using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class EmployeesController : TimeSheetBaseController
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeManager _employeeManager;

        public EmployeesController(
            IEmployeeManager employeeManager,
            ILogger<EmployeesController> logger)
        {
            _employeeManager = employeeManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в EmployeesController");
        }

        /// <summary> Получение информации о сотруднике по его Id </summary>
		/// <param name="id"> Id сотрудника </param>
		/// <returns> Инорфмация о сотруднике </returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] Guid id)
        {
            var result = _employeeManager.GetItem(id);

            return Ok(result);
        }

        /// <summary> Получение информации о нескольких сотрудниках </summary>
		/// <returns> Информацию о сотрудниках </returns>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _employeeManager.GetItems();
            return Ok(result);
        }

        /// <summary> Создание записи о новом сотруднике </summary>
        /// <param name="request"> Запрос на создание записи </param>
        /// <returns> Id созданной записи </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest employeeRequest)
        {    
            var id = await _employeeManager.Create(employeeRequest);

            return Ok(id);
        }

        /// <summary> Изменение существующей записи о сотруднике </summary>
        /// <param name="id"> Id изменяемой записи </param>
        /// <param name="request"> Запрос на изменение </param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EmployeeRequest employeeRequest)
        { 
            await _employeeManager.Update(id, employeeRequest);

            return Ok();
        }

        /// <summary>Удаление записи о сотруднике </summary>
        /// <param name="id"> Id удаляемой записи </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {            
            await _employeeManager.Delete(id);

            return Ok();
        }
    }
}