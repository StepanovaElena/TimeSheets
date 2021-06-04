using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Implementation
{
    public class ServiceManager : IServiceManager
    {
        public Task<Guid> Create(ServiceRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Service>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, ServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
