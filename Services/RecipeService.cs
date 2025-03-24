using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkSpoonDemo.Models;
using ForkSpoonDemo.DTOs;

namespace ForkSpoonDemo.Services
{
    public class RecipeService : IRecipeService
    {

        private readonly ForkSpoonDbContext _context;

        public RecipeService(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(int id, UpdateRecipeDto updateRecipeDto)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);
            if (existingRecipe == null) return null;

            // Update properties only if they are provided
            if (!string.IsNullOrEmpty(updateRecipeDto.Title))
                existingRecipe.Title = updateRecipeDto.Title;

            if (!string.IsNullOrEmpty(updateRecipeDto.Ingredients))
                existingRecipe.Ingredients = updateRecipeDto.Ingredients;

            if (!string.IsNullOrEmpty(updateRecipeDto.Steps))
                existingRecipe.Steps = updateRecipeDto.Steps;

            if (!string.IsNullOrEmpty(updateRecipeDto.Category))
                existingRecipe.Category = updateRecipeDto.Category;

            if (!string.IsNullOrEmpty(updateRecipeDto.ImageUrl))
                existingRecipe.ImageUrl = updateRecipeDto.ImageUrl;

            await _context.SaveChangesAsync();
            return existingRecipe;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return false;
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
