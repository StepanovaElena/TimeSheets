using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Models;

namespace TimeSheets.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByLoginPass(string login, byte[] passwordHash);
        Task<User> GetUserByToken(string token);
        Task<IList<User>> GetUsers();
        Task Create(User user);
        Task Update(User user);
        Task Delete(Guid id);
    }
}