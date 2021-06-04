﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Data.Implementation
{
    public class ContractRepo : IContractRepo
    {
        private readonly TimeSheetDbContext _dbContext;

        public ContractRepo(TimeSheetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Contract> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contract>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Add(Contract item)
        {
            throw new NotImplementedException();
        }

        public Task Create(ContractRequest item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Contract item)
        {
            _dbContext.Contracts.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            var contract = await _dbContext.Contracts.FindAsync(id);
            var now = DateTime.Now;
            var isActive = now <= contract?.DateEnd && now >= contract?.DateStart;

            return isActive;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}