using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkSpoonDemo.Models;
using ForkSpoonDemo.DTOs;

namespace ForkSpoonDemo.Services
{
    public class UserService : IUserService
    {
        private readonly ForkSpoonDbContext _context;

        public UserService(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return null;

            // Update only non-null fields
            if (!string.IsNullOrEmpty(user.Username)) existingUser.Username = user.Username;
            if (!string.IsNullOrEmpty(user.Email)) existingUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Password)) existingUser.Password = user.Password;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> UpdateUserByUsernameAsync(string username, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            // Update fields using the DTO
            if (!string.IsNullOrEmpty(updateUserDto.Email)) user.Email = updateUserDto.Email;
            if (!string.IsNullOrEmpty(updateUserDto.Password)) user.Password = updateUserDto.Password;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
