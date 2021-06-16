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
    public class ServicesController : TimeSheetBaseController
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly IServiceManager _serviceManager;

        public ServicesController(
            IServiceManager serviceManager,
            ILogger<ServicesController> logger)
        {
            _serviceManager = serviceManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в ServiceController");
        }

        /// <summary>Получение информации об услуге по ее Id</summary>
        /// <param name="id">Id услуги</param>
        /// <returns>Инорфмация об услуге</returns>
        [Authorize(Roles = "admin, user, client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _serviceManager.GetItem(id);

            return Ok(result);
        }

        /// <summary>Получение информации о нескольких услугах</summary>
        /// <returns>Коллекция содержащая информацию об услугах</returns>
        [Authorize(Roles = "admin, user, client")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _serviceManager.GetItems();

            return Ok(result);
        }
        /// <summary>Создание нового услуги</summary>
        /// <param name="request">Закпрос на создание услуги</param>
        /// <returns>Id созданной услуги</returns>
        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceRequest request)
        {
            var id = await _serviceManager.Create(request);

            return Ok(id);
        }
        /// <summary>Изменение существующй услуги</summary>
        /// <param name="id">Id изменяемой услуги</param>
        /// <param name="request">Запрос на изменение услуги</param>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ServiceRequest request)
        {
            await _serviceManager.Update(id, request);

            return Ok();
        }

        /// <summary>Удаление услуги</summary>
        /// <param name="id">Id удаляемой услуги</param>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _serviceManager.Delete(id);

            return Ok();
        }
    }
}