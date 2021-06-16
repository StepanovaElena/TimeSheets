using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Interfaces;
using TimeSheets.Models;

namespace TimeSheets.Data.Implementation
{
    public class ContractRepo : IContractRepo
    {
        private readonly TimeSheetDbContext _context;

        public ContractRepo(TimeSheetDbContext context)
        {
            _context = context;
        }

        public async Task<Contract> GetItem(Guid id)
        {
            var result = await _context.Contracts.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Contract>> GetItems()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task Add(Contract item)
        {
            await _context.Contracts.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contract item)
        {
            _context.Contracts.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            var now = DateTime.Now;
            var isActive = now <= contract?.DateEnd && now >= contract?.DateStart;

            return isActive;
        }

        public async Task Delete(Guid id)
        {
            var item = await _context.Contracts.FindAsync(id);

            if (item != null)
            {
                item.IsDeleted = true;
                _context.Contracts.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}