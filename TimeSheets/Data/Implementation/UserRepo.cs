using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Interfaces;
using TimeSheets.Models;

namespace TimeSheets.Data.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly TimeSheetDbContext _context;

        public UserRepo(TimeSheetDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByLoginPass(string login, byte[] passwordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == login && x.PasswordHash == passwordHash);
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Token == token));
        }

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}