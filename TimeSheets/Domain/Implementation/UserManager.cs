using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Models;
using TimeSheets.Models.Dto;

namespace TimeSheets.Domain.Implementation
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepo _userRepo;

        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Guid> CreateUser(UserRequest request)
        {    
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username
            };

            await _userRepo.Create(user);

            return user.Id;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepo.GetUsers();
        }

        public async Task<User> UpdateUser(Guid id, UserRequest request)
        {
            var user = new User
            {
                Id = id,
                Username = request.Username                
            };

            await _userRepo.Update(user);
            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepo.Delete(id);
        }
    }
}