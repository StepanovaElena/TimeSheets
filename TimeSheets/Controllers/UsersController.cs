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

        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] Guid id)
        {
            var result = _userManager.GetUser(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _userManager.GetUsers();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var id = await _userManager.CreateUser(request);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserRequest request)
        {            
            await _userManager.UpdateUser(id, request);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userManager.DeleteUser(id);

            return Ok();
        }
    }
}