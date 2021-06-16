using System;
using System.Collections.Generic;

namespace TimeSheets.Models
{
    /// <summary> Информация о сотруднике </summary>
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}