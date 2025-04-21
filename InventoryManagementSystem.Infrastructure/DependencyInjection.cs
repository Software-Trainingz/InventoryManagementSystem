// File: InventoryManagementSystem.Infrastructure/DependencyInjection.cs
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using InventoryManagementSystem.Infrastructure.Data;
using InventoryManagementSystem.Infrastructure.Json;
using InventoryManagementSystem.Infrastructure.UnitOfWorks;
using InventoryManagementSystem.Infrastucture.Data;
// Remove typo namespaces
// using InventoryManagementSystem.Infrastucture.Data;
// using InventoryManagementSystem.Infrastucture.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Infrastructure // Fixed namespace (was Infrastucture)
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices( // Fixed method name (was singular)
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                       .UseLazyLoadingProxies();
            });

            // Register Database Seeder
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services;
        }
    }
}
  