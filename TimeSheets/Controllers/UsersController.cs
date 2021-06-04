using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserManager userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;

            _logger = logger;
            _logger.LogDebug("NLog зарегистрирован в UsersController");
        }

        /// <summary> Получение информации о пользователе по его Id </summary>
		/// <param name="id"> Id пользователя </param>
		/// <returns> Инорфмация о пользователе </returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] Guid id)
        {
            var result = _userManager.GetUser(id);

            return Ok(result);
        }

        /// <summary> Получение информации о всех активных пользователях </summary>
        /// <returns> Инорфмация о пользователях </returns>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _userManager.GetUsers();

            return Ok(result);
        }

        /// <summary> Создание нового пользователя </summary>
		/// <param name="request"> Запрос на создание пользователя </param>
		/// <returns> Id созданного пользователя </returns>
		[Authorize(Roles = "admin")]
        [HttpPost]

        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var response = await _userManager.CreateUser(request);

            return Ok(response);
        }

        /// <summary> Изменение существующего пользователя </summary>
		/// <param name="id"> Id изменяемого пользователя </param>
		/// <param name="request"> Запрос на изменение пользователя </param>
		[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserRequest request)
        {            
            await _userManager.UpdateUser(id, request);

            return Ok();
        }

        /// <summary>Удаление пользователя</summary>
		/// <param name="id">Id удаляемого пользователя</param>
		[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userManager.DeleteUser(id);

            return Ok();
        }
    }
}