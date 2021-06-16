using System.Threading.Tasks;
using Timesheets.Models.Dto;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Interfaces
{
    public interface ILoginManager
    {
		/// <param name="user">Пользователь</param>
		/// <returns>Результат аутентификации пользователя</returns>
		Task<LoginResponse> Authenticate(User user);
		Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
		Task<bool> RevokeToken(RevokeTokenRequest request);

	}
}