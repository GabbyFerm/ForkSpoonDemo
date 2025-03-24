using Bogus;
using ForkSpoonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkSpoonDemo.Seeders
{
    public class RecipeDataSeeder
    {
        private readonly ForkSpoonDbContext _context;

        public RecipeDataSeeder(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task SeedRecipesAsync(int count)
        {
            if (await _context.Recipes.AnyAsync()) return; // Check if data already exists

            for (int i = 1; i <= count; i++)
            {
                var recipe = new Recipe
                {
                    Title = $"Test Recipe {i}",
                    Ingredients = $"Test Ingredients {i}",
                    Steps = $"Test Steps {i}",
                    Category = $"Test Category {i}",
                    ImageUrl = $"test{i}.jpg",
                    CreatedBy = 1 // Assuming a user with ID 1 exists
                };

                _context.Recipes.Add(recipe);
            }

            await _context.SaveChangesAsync();
        }
    }
}
