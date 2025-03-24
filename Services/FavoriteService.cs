using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkSpoonDemo.Models;

namespace ForkSpoonDemo.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ForkSpoonDbContext _context;

        public FavoriteService(ForkSpoonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
        {
            return await _context.Favorites.ToListAsync();
        }

        public async Task<Favorite> GetFavoriteByIdAsync(int id)
        {
            return await _context.Favorites.FindAsync(id);
        }

        public async Task<Favorite> CreateFavoriteAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<Favorite> UpdateFavoriteAsync(int id, Favorite favorite)
        {
            var existingFavorite = await _context.Favorites.FindAsync(id);
            if (existingFavorite == null) return null;

            // Update properties
            existingFavorite.UserId = favorite.UserId;
            existingFavorite.RecipeId = favorite.RecipeId;
            existingFavorite.DateFavorited = favorite.DateFavorited;

            await _context.SaveChangesAsync();
            return existingFavorite;
        }

        public async Task<bool> DeleteFavoriteAsync(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}