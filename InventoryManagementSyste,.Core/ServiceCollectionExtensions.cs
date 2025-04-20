// File: InventoryManagementSystem.Core/ServiceExtensions.cs
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCoreServices(
            this IServiceCollection services)
        {
            // Register core services here
            // Example: services.AddScoped<IMyService, MyService>();

            return services;
        }
    }
}
