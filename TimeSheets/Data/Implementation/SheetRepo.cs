using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Interfaces;
using TimeSheets.Models;

namespace TimeSheets.Data.Implementation
{
    public class SheetRepo: ISheetRepo
    {
        private readonly TimesheetDbContext _context;

        public SheetRepo(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task<Sheet> GetItem(Guid id)
        {
            var result = await _context.Sheets.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Sheet>> GetItems()
        {
            var result =  await _context.Sheets.ToListAsync();
            
            return result;
        }

        public async Task Add(Sheet item)
        {
            await _context.Sheets.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Sheet item)
        {
            _context.Sheets.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}