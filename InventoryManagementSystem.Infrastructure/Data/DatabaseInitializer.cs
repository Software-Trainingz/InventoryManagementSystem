// Enhanced DatabaseInitializer.cs with improved error handling
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using InventoryManagementSystem.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDatabaseSeeder _seeder;
        private readonly ILogger<DatabaseInitializer> _logger;
        private readonly bool _continueOnError;

        public DatabaseInitializer(
            ApplicationDbContext dbContext,
            IDatabaseSeeder seeder,
            ILogger<DatabaseInitializer> logger,
            bool continueOnError = true)
        {
            _dbContext = dbContext;
            _seeder = seeder;
            _logger = logger;
            _continueOnError = continueOnError;
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                // First try to migrate the database
                try
                {
                    await _dbContext.Database.MigrateAsync();
                    _logger.LogInformation("Database migration completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during database migration");
                    if (!_continueOnError)
                    {
                        throw;
                    }
                    _logger.LogWarning("Continuing despite migration error");
                }

                // Then try to seed the database
                try
                {
                    await _seeder.SeedDatabaseAsync();
                    _logger.LogInformation("Database seeding completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during database seeding");
                    if (!_continueOnError)
                    {
                        throw;
                    }
                    _logger.LogWarning("Continuing despite seeding error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical error during database initialization");
                if (!_continueOnError)
                {
                    throw;
                }
                _logger.LogWarning("Application will continue with potentially incomplete database initialization");
            }
        }
    }
}
