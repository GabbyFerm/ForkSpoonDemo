using ForkSpoonDemo.DTOs;
using ForkSpoonDemo.Models;

namespace ForkSpoonDemo.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<User> UpdateUserByUsernameAsync(string username, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
