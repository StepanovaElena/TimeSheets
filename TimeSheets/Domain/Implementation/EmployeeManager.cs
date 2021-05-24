using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto;

namespace TimeSheets.Domain.Implementation
{
    public class EmployeeManager: IEmployeeManager
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeManager(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Guid> Create(EmployeeRequest employeeRequest)
        {
            var employee = new Employee()
            {
                UserId = employeeRequest.UserId
            };

            await _employeeRepo.Add(employee);

            return employee.Id;
        }

        public async Task Delete(Guid id)
        {
            await _employeeRepo.Delete(id);
        }

        public async Task<Employee> GetItem(Guid id)
        {
            return await _employeeRepo.GetItem(id);
        }

        public async Task<IEnumerable<Employee>> GetItems()
        {
            return await _employeeRepo.GetItems();
        }

        public async Task Update(Guid id, EmployeeRequest employeeRequest)
        {
            var employee = new Employee()
            {
                Id = id,
                UserId = employeeRequest.UserId
                
            };

            await _employeeRepo.Update(employee);
        }
    }
}