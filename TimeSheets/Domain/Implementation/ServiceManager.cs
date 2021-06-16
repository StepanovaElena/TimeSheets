using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Implementation
{
    public class ServiceManager : IServiceManager
    {
		private readonly IServiceRepo _repo;

		public ServiceManager(IServiceRepo repo)
		{
			_repo = repo;
		}

		public async Task<Service> GetItem(Guid id)
		{
			return await _repo.GetItem(id);
		}

		public async Task<IEnumerable<Service>> GetItems()
		{
			return await _repo.GetItems();
		}

		public async Task<Guid> Create(ServiceRequest request)
		{
			var Service = new Service()
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				IsDeleted = false,
			};

			await _repo.Add(Service);

			return Service.Id;
		}

		public async Task Update(Guid id, ServiceRequest request)
		{
			var item = await _repo.GetItem(id);

			if (item != null)
			{
				item.Name = request.Name;

				await _repo.Update(item);
			}
		}

		public async Task Delete(Guid id)
		{
			await _repo.Delete(id);
		}
	}
}
