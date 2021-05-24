using System;
using System.Threading.Tasks;

namespace TimeSheets.Domain.Interfaces
{
    public interface IContractManager
    {
        Task<bool?> CheckContractIsActive(Guid id);
    }
}