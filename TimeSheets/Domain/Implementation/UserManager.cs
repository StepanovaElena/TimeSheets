using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models.Dto;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Infrastructure.Extensions;
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
            request.EnsureNotNull(nameof(request));

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = GetPasswordHash(request.Password),
                Role = request.Role
            };

            await _userRepo.Create(user);

            return user.Id;
        }

        public async Task<User> GetUser(LoginRequest request)
        {
            var passwordHash = GetPasswordHash(request.Password);
            var user = await _userRepo.GetUserByLoginPass(request.Login, passwordHash);

            return user;
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

        /// <summary>Возвращает хэш пароля</summary>
		/// <param name="password">Хэшируемый пароль</param>
		/// <returns>SHA1 хэш пароля</returns>
        private static byte[] GetPasswordHash(string password)
        {
            using var sha1 = new SHA1CryptoServiceProvider();

            return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
        }
    }
}