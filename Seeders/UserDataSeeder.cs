using Bogus;
using ForkSpoonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkSpoonDemo.Seeders
{
    public class UserDataSeeder
    {
        private readonly ForkSpoonDbContext _context;

        public UserDataSeeder(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task SeedUsersAsync(int count)
        {
            var existingUserCount = await _context.Users.CountAsync();
            if (existingUserCount >= count)
            {
                return; // Skip seeding if the desired count is already met
            }

            var faker = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            // Generate fake users without setting UserId
            var users = faker.Generate(count - existingUserCount);

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
