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
        private readonly TimeSheetDbContext _context;

        public SheetRepo(TimeSheetDbContext context)
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

        public async Task Delete(Guid id)
        {
            var item = await _context.Sheets.FindAsync(id);
            if (item != null)
            {
                item.IsDeleted = true;
                _context.Sheets.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Sheet>> GetItemsForInvoice(Guid contractId, DateTime dateStart, DateTime dateEnd)
        {
            var sheets = await _context.Sheets
                .Where(x => x.ContractId == contractId)
                .Where(x => x.Date <= dateEnd && x.Date >= dateStart)
                .Where(x => x.InvoiceId == null)
                .ToListAsync();

            return sheets;
        }
    }
}