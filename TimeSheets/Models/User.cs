using System;
using System.Collections.Generic;

namespace TimeSheets.Models
{
    /// <summary> Информация о пользователе системы </summary>
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public IList<RefreshToken> RefreshTokens { get; set; }
    }
}