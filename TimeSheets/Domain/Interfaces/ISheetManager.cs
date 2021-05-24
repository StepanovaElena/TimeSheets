using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Models;
using TimeSheets.Models.Dto;

namespace TimeSheets.Domain.Interfaces
{
    public interface ISheetManager
    {
        Task<Sheet> GetItem(Guid id);
        Task<IEnumerable<Sheet>> GetItems();
        Task<Guid> Create(SheetRequest sheet);
        Task Update(Guid id, SheetRequest sheetRequest);
    }
}