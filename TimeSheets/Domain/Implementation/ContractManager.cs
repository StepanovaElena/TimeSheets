using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Implementation
{
    public class ContractManager: IContractManager
    {
        private readonly IContractRepo _contractRepo;

        public ContractManager(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            return await _contractRepo.CheckContractIsActive(id);
        }

        public Task<Guid> Create(ContractRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Contract> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contract>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, ContractRequest request)
        {
            throw new NotImplementedException();
        }
    }
}