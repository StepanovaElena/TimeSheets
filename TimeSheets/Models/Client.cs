using System;

namespace TimeSheets.Models
{
    /// <summary> Информация о владельце контракта </summary>
    public class Client
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

        // Навигационное свойство
        public User User { get; set; }
    }
}