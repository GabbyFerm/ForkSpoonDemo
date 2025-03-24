using Bogus;
using ForkSpoonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkSpoonDemo.Seeders
{
    public class FavoriteDataSeeder
    {
        private readonly ForkSpoonDbContext _context;

        public FavoriteDataSeeder(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task SeedFavoritesAsync(int count)
        {
            // Check if there are already favorites in the database
            if (await _context.Favorites.AnyAsync())
            {
                return; // Skip seeding if favorites already exist
            }

            // Generate fake favorites
            var faker = new Faker<Favorite>()
                .RuleFor(f => f.UserId, f => f.Random.Int(1, 30)) // Assuming you have users with IDs 1 to 30
                .RuleFor(f => f.RecipeId, f => f.Random.Int(1, 30)) // Assuming you have recipes with IDs 1 to 30
                .RuleFor(f => f.DateFavorited, f => f.Date.Recent());

            var favorites = faker.Generate(count);

            await _context.Favorites.AddRangeAsync(favorites);
            await _context.SaveChangesAsync();
        }
    }
}