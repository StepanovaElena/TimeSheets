namespace Timesheets.Models.Dto
{
    /// <summary>Результат аутентификации пользователя</summary>
    public class LoginResponse
    {
        /// <summary>Токен доступа</summary>
        public string AccessToken { get; set; }
        /// <summary>Токен обновления</summary>
        public string RefreshToken { get; set; }
        /// <summary>Срок действия</summary>
        public long ExpiresIn { get; set; }
    }
}