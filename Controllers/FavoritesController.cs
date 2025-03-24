using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForkSpoonDemo.Models;
using ForkSpoonDemo.Services;

namespace ForkSpoonDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<IActionResult> GetAllFavorites()
        {
            var favorites = await _favoriteService.GetAllFavoritesAsync();
            return Ok(favorites);
        }

        // GET: api/favorites/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteById(int id)
        {
            var favorite = await _favoriteService.GetFavoriteByIdAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }

        // POST: api/favorites
        [HttpPost]
        public async Task<IActionResult> CreateFavorite(Favorite favorite)
        {
            var newFavorite = await _favoriteService.CreateFavoriteAsync(favorite);
            return CreatedAtAction(nameof(GetFavoriteById), new { id = newFavorite.FavoriteId }, newFavorite);
        }

        // PUT: api/favorites/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavoriteById(int id, Favorite favorite)
        {
            var updatedFavorite = await _favoriteService.UpdateFavoriteAsync(id, favorite);
            if (updatedFavorite == null) return NotFound();
            return Ok(updatedFavorite);
        }

        // DELETE: api/favorites/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var result = await _favoriteService.DeleteFavoriteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
