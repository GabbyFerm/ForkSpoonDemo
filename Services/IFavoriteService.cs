using ForkSpoonDemo.Models;

namespace ForkSpoonDemo.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync();
        Task<Favorite> GetFavoriteByIdAsync(int id);
        Task<Favorite> CreateFavoriteAsync(Favorite favorite);
        Task<Favorite> UpdateFavoriteAsync(int id, Favorite favorite);
        Task<bool> DeleteFavoriteAsync(int id);
    }
}
