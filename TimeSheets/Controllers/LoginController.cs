using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Models.Dto;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController: TimeSheetBaseController
    {
        private readonly IUserManager _userManager;
        private readonly ILoginManager _loginManager;

        public LoginController(IUserManager userManager, ILoginManager loginManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.GetUser(request);

            if (user == null)
            {
                return Unauthorized();
            }

            var loginResponse = await _loginManager.Authenticate(user);

            return Ok(loginResponse);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
                return BadRequest(new { message = "Token is required" });

            var loginResponse = await _loginManager.RefreshToken(request);

            if (loginResponse == null)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(loginResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
                return BadRequest(new { message = "Token is required" });

            var loginResponse = await _loginManager.RevokeToken(request);

            if (!loginResponse)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
    }
}