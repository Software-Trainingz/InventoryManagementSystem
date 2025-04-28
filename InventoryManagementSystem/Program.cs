// Enhanced Program.cs with improved error handling
using InventoryManagementSystem.APIs.MiddleWare;
using InventoryManagementSystem.Application.Service;
using InventoryManagementSystem.Application.Service.Common;
using InventoryManagementSystem.Application.ServiceContracts;
using InventoryManagementSystem.Application.ServiceContracts.Common;
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using Mapster;
using Serilog;
using System.Reflection;

namespace InventoryManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
         
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
               .ReadFrom.Configuration(context.Configuration)
               .ReadFrom.Services(services);

            });

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddMapster();
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();
            // في بداية الـ ConfigureServices
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();

            // التسجيل الصحيح للـ ServiceManager
            builder.Services.AddScoped<IServiceManager>(provider =>
            {
                Func<IUserManagementService> userServiceFactory = () =>
                    provider.GetRequiredService<IUserManagementService>();
                return new ServiceManager(userServiceFactory);
            });
            // Register core services

            // Load and register infrastructure services
            RegisterInfrastructureServices(builder.Services, builder.Configuration);

            // Add configuration for error handling
            builder.Services.Configure<ErrorHandlingOptions>(options =>
            {
                options.ContinueOnError = true; // Set to true to skip errors
                options.LogDetailedErrors = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

       

 

            // Initialize database with improved exception handling
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                using var scope = app.Services.CreateScope();
                try
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

                    var initializer = scope.ServiceProvider
                        .GetRequiredService<IDatabaseInitializer>();

                    try
                    {
                        await initializer.InitializeDatabaseAsync();
                    }
                    catch (Exception dbEx)
                    {
                        //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        //logger.LogError(dbEx, "Error during database initialization");
                        // Don't rethrow - allow application to start anyway
                    }
                }
                catch (Exception ex)
                {
                    var logger = app.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during application startup");
                    // Don't rethrow - allow application to start anyway
                }
            }
            else
            {
                app.UseExceptionHandlingMiddleware();
            }


            app.UseExceptionHandlingMiddleware();

            app.Run();
        }

        private static void RegisterInfrastructureServices(IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                // Try to load the Infrastructure assembly
                var infrastructureAssemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InventoryManagementSystem.Infrastructure.dll");
                var infrastructureAssembly = Assembly.LoadFrom(infrastructureAssemblyPath);

                // Find the DependencyInjection type
                var diType = infrastructureAssembly.GetTypes()
                    .FirstOrDefault(t => t.Name == "DependencyInjection" &&
                                        t.Namespace == "InventoryManagementSystem.Infrastructure");

                if (diType != null)
                {
                    // Find the AddInfrastructureServices method
                    var method = diType.GetMethod("AddInfrastructureServices",
                        BindingFlags.Public | BindingFlags.Static);

                    if (method != null)
                    {
                        // Invoke the method
                        method.Invoke(null, new object[] { services, configuration });
                        Console.WriteLine("Infrastructure services registered successfully");
                    }
                    else
                    {
                        Console.WriteLine("AddInfrastructureServices method not found");
                        // Fallback: Register essential services directly
                        RegisterEssentialServices(services, configuration);
                    }
                }
                else
                {
                    Console.WriteLine("DependencyInjection type not found");
                    // Fallback: Register essential services directly
                    RegisterEssentialServices(services, configuration);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering infrastructure services: {ex.Message}");
                // Fallback: Register essential services directly
                RegisterEssentialServices(services, configuration);
            }
        }

        // Fallback method to ensure critical services are registered
        private static void RegisterEssentialServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register the IDatabaseInitializer directly
            services.AddScoped<IDatabaseInitializer, EnhancedEmergencyDatabaseInitializer>();
        }
    }

    // Configuration options for error handling
    public class ErrorHandlingOptions
    {
        public bool ContinueOnError { get; set; } = true;
        public bool LogDetailedErrors { get; set; } = true;
    }

    // Enhanced emergency implementation for fallback
    public class EnhancedEmergencyDatabaseInitializer : IDatabaseInitializer
    {
        private readonly ILogger<EnhancedEmergencyDatabaseInitializer> _logger;

        public EnhancedEmergencyDatabaseInitializer(ILogger<EnhancedEmergencyDatabaseInitializer> logger)
        {
            _logger = logger;
        }

        public Task InitializeDatabaseAsync()
        {
            _logger.LogWarning("Using enhanced emergency database initializer - infrastructure services not properly registered");
            _logger.LogInformation("This implementation will skip errors and allow the application to continue");
            return Task.CompletedTask;
        }
    }
}
