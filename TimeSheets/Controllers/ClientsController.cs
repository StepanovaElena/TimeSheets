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
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientManager _clientManager;

        public ClientsController(
            IClientManager clientManager,
            ILogger<ClientsController> logger)
        {
            _clientManager = clientManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в ContractController");
        }

        /// <summary> Получение информации о клиенте по его Id </summary>
        /// <param name="id"> Id клиента </param>
        /// <returns> Инорфмация о клиенте </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _clientManager.GetItem(id);

            return Ok(result);
        }

        /// <summary> Получение информации об активных клиентах </summary>
        /// <returns> Информацию о клиентах </returns>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _clientManager.GetItems();

            return Ok(result);
        }

        /// <summary> Создание нового клиента </summary>
        /// <param name="request"> Запрос на создание клиента </param>
        /// <returns> Id созданного клиента </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientRequest request)
        {
            var id = await _clientManager.Create(request);

            return Ok(id);
        }

        /// <summary> Изменение существующего клиента </summary>
        /// <param name="id"> Id изменяемого клиента </param>
        /// <param name="request"> Запрос на изменение клиента </param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ClientRequest request)
        {
            await _clientManager.Update(id, request);

            return Ok();
        }
        /// <summary> Удаление клиента </summary>
        /// <param name="id"> Id удаляемого клиента </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _clientManager.Delete(id);

            return Ok();
        }
    }
}