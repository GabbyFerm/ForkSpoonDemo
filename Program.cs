using ForkSpoonDemo.Models;
using ForkSpoonDemo.Seeders;
using ForkSpoonDemo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace ForkSpoonDemo
{
    public class Program
    {
        public static async Task Main(string[] args) // Marked as async and changed return type to Task
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Add services to the container.
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFavoriteService, FavoriteService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();

            // Register the DbContext with the DI container
            builder.Services.AddDbContext<ForkSpoonDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Data seeders for dummy data
            builder.Services.AddScoped<UserDataSeeder>(); 
            builder.Services.AddScoped<FavoriteDataSeeder>();
            builder.Services.AddScoped<RecipeDataSeeder>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Seed the database with test data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<UserDataSeeder>();
                    await seeder.SeedUsersAsync(50); // Set desired amount here

                    var recipeSeeder = services.GetRequiredService<RecipeDataSeeder>();
                    await recipeSeeder.SeedRecipesAsync(50); // Seed recipes

                    var favoriteSeeder = services.GetRequiredService<FavoriteDataSeeder>();
                    await favoriteSeeder.SeedFavoritesAsync(50); // Seed favorites
                }
                catch (Exception ex)
                {
                    // Log the error (you can use a logging framework)
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }
            }

            app.Run();
        }
    }
}
