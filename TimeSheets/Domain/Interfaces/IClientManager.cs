using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Domain.Interfaces
{
    public interface IClientManager : IBaseManager<Client, ClientRequest>
    {
    }
}