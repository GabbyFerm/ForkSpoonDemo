using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForkSpoonDemo.Models;
using ForkSpoonDemo.DTOs;
using AutoMapper;
using ForkSpoonDemo.Services;

namespace ForkSpoonDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        private readonly IMapper _mapper;

        public RecipesController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        // GET: api/Recipes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        // PUT: api/Recipes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeById(int id, UpdateRecipeDto updateRecipeDto)
        {
            var updatedRecipe = await _recipeService.UpdateRecipeAsync(id, updateRecipeDto);
            if (updatedRecipe == null) return NotFound();
            return Ok(updatedRecipe);
        }

        // POST: api/Recipes
        [HttpPost]
        public async Task<IActionResult> CreateARecipe(Recipe recipe)
        {
            var newRecipe = await _recipeService.CreateRecipeAsync(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { id = newRecipe.RecipeId }, newRecipe);
        }

        // DELETE: api/Recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipeToDelete = await _recipeService.DeleteRecipeAsync(id);
            if (!recipeToDelete) return NotFound();
            return NoContent();
        }
    }
}
