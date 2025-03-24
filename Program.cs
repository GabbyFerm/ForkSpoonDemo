using ForkSpoonDemo.Models;
using ForkSpoonDemo.Seeders;
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
            // Register the DbContext with the DI container
            builder.Services.AddDbContext<ForkSpoonDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<UserDataSeeder>(); // Register the seeder

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
                var seeder = services.GetRequiredService<UserDataSeeder>();
                await seeder.SeedUsersAsync(20); // Set desired amount here
            }

            app.Run();
        }
    }
}
