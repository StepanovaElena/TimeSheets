using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models.Dto;
using TimeSheets.Models;
using TimeSheets.Models.Dto;

namespace TimeSheets.Domain.Interfaces
{
    public interface IUserManager
    {  
        Task<User> GetUser(LoginRequest request);
        Task<User> GetUser(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task<Guid> CreateUser(UserRequest request);
        Task<User> UpdateUser(Guid id, UserRequest request);
        Task DeleteUser(Guid id);
    }
}