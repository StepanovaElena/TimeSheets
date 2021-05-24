using System;
using System.Threading.Tasks;
using TimeSheets.Models;

namespace TimeSheets.Data.Interfaces
{
    public interface IContractRepo: IRepoBase<Contract>
    {
        Task<bool?> CheckContractIsActive(Guid id);
    }
}