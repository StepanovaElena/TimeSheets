using System;

namespace TimeSheets.Models.Dto
{
    public class UserRequest
    {
        /// <summary>Имя пользователя</summary>
        public string Username { get; set; }

        /// <summary>Пароль пользователя</summary>
        public string Password { get; set; }

        /// <summary>Роль пользователя</summary>
        public string Role { get; set; }
    }
}