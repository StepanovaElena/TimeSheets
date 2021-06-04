using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        public Task<Guid> Create(InvoiceRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Invoice>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, InvoiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
