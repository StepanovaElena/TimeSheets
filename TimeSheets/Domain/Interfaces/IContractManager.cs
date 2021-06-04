using System;
using System.Threading.Tasks;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Interfaces
{
    public interface IContractManager : IBaseManager<Contract, ContractRequest>
    {
        Task<bool?> CheckContractIsActive(Guid id);
    }
}