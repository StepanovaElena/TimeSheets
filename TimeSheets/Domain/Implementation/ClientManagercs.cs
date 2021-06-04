using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Implementation
{
    public class ClientManagercs : IClientManager
    {
        public Task<Guid> Create(ClientRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, ClientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
