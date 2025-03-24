using ForkSpoonDemo.DTOs;
using ForkSpoonDemo.Models;

namespace ForkSpoonDemo.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeAsync(int id, UpdateRecipeDto updateRecipeDto);
        Task<bool> DeleteRecipeAsync(int id);
    }
}
