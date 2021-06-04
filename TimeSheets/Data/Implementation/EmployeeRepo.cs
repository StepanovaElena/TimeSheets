using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Interfaces;
using TimeSheets.Models;

namespace TimeSheets.Data.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly TimeSheetDbContext _context;

        public EmployeeRepo(TimeSheetDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetItem(Guid id)
        {
            var result = await _context.Employees.FindAsync(id);

            return !result.IsDeleted ? result : null;
        }

        public async Task<IEnumerable<Employee>> GetItems()
        {
            var result = await _context.Employees.ToListAsync();

            return result.FindAll(x => !x.IsDeleted);
        }

        public async Task Add(Employee item)
        {
            await _context.Employees.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Employee item)
        {
            _context.Employees.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var result = await _context.Employees.FindAsync(id);
            result.IsDeleted = true;

            _context.Employees.Update(result);
            await _context.SaveChangesAsync();
        }
    }
}