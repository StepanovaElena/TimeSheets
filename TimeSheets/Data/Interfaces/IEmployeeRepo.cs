using System;
using System.Threading.Tasks;
using TimeSheets.Models;

namespace TimeSheets.Data.Interfaces
{
    public interface IEmployeeRepo: IRepoBase<Employee>
    {
        Task Delete(Guid id);
    }
}