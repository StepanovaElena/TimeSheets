using System;
using System.Collections.Generic;

namespace TimeSheets.Models
{
    /// <summary> Информация о предоставляемой услуге в рамках контракта </summary>
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; internal set; }

        // Навигационные свойства
        public ICollection<Sheet> Sheets { get; set; }
    }
}