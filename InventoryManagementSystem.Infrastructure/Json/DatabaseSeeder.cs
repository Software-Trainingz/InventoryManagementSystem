using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using InventoryManagementSystem.Infrastructure.Data;
using InventoryManagementSystem.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Json
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly bool _continueOnError;

        public DatabaseSeeder(ApplicationDbContext dbContext, ILogger<DatabaseSeeder> logger, bool continueOnError = true)
        {
            _dbContext = dbContext;
            _logger = logger;
            _continueOnError = continueOnError;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        public async Task SeedDatabaseAsync()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await _dbContext.Database.MigrateAsync();
                _logger.LogInformation("Seeding database...");

                // 1. Seed core independent entities first
                var admins = await SeedAdminsAsync();
                var locations = await SeedEntitiesAsync<Location>("Seeds/Location.json");
                var categories = await SeedEntitiesAsync<Category>("Seeds/Category.json");
                var restaurantCategories = await SeedEntitiesAsync<RestaurantCategory>("Seeds/RestaurantCategory.json");
                var suppliers = await SeedEntitiesAsync<Supplier>("Seeds/Supplier.json");
                await SeedCustomersAsync(); // Add Customers early

                // 2. Seed entities with basic dependencies
                var restaurants = await SeedRestaurantsAsync(restaurantCategories);
                var warehouseStaff = await SeedWarehouseStaffAsync(admins);
                var cashiers = await SeedCashiersAsync(admins);

            //   await SeedEntitiesAsync<DeliveryMethod>("Seeds/DeliveryMethod.json");

                // 3. Seed Orders before OrderItems
                await SeedEntitiesAsync<Order>("Seeds/Order.json");

                // 4. Seed entities with complex dependencies
                await SeedProductsAsync(categories, locations, suppliers, restaurants, warehouseStaff);
                await SeedInventoriesAsync(locations);
                await SeedWorkingHoursAsync(restaurants);

                // 5. Seed ProductOptions before OptionItems
           //     await SeedProductOptionsAsync();

                // 6. Seed remaining entities
                await SeedReportsAsync();
                await SeedUserPermissionsAsync();
                await SeedStockMovesAsync();
                await SeedOrderItemsAsync();
                await SeedOrderItemOptionsAsync();
            //    await SeedOptionItemsAsync();

                await transaction.CommitAsync();
                _logger.LogInformation("Database seeded successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Database seeding failed");

                if (!_continueOnError)
                    throw;
            }
        }

        // New method to reset and seed the database from scratch
        //public async Task ResetAndSeedDatabaseAsync()
        //{
        //    try
        //    {
        //        // Delete the database
        //        await _dbContext.Database.EnsureDeletedAsync();
        //        _logger.LogInformation("Database deleted successfully");

        //        // Create a new database and apply migrations
        //        await _dbContext.Database.MigrateAsync();
        //        _logger.LogInformation("Database created and migrations applied");

        //        // Seed the database in correct order
        //        await SeedDatabaseInOrderAsync();

        //        _logger.LogInformation("Database reset and seeded successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error resetting and seeding database");
        //        throw;
        //    }
        //}

        private async Task SeedDatabaseInOrderAsync()
        {
            // 1. Seed independent entities first
            await SeedAdminsAsync();
            await SeedEntitiesAsync<Location>("Seeds/Location.json");
            await SeedEntitiesAsync<Category>("Seeds/Category.json");
            await SeedEntitiesAsync<RestaurantCategory>("Seeds/RestaurantCategory.json");
            await SeedEntitiesAsync<Supplier>("Seeds/Supplier.json");
            await SeedCustomersAsync(); // Add Customers early

            // 2. Seed entities with simple dependencies
            await SeedRestaurantsAsync(await _dbContext.RestaurantCategories.ToListAsync());
            await SeedWarehouseStaffAsync(await _dbContext.Admins.ToListAsync());
            await SeedCashiersAsync(await _dbContext.Admins.ToListAsync());

            // 3. Seed Orders before OrderItems
            await SeedEntitiesAsync<Order>("Seeds/Order.json");

            // 4. Seed Products and related entities
            await SeedProductsAsync(
                await _dbContext.Categories.ToListAsync(),
                await _dbContext.Locations.ToListAsync(),
                await _dbContext.Suppliers.ToListAsync(),
                await _dbContext.Restaurants.ToListAsync(),
                await _dbContext.WarehouseStaffs.ToListAsync());
                


            // 5. Seed ProductOptions before OptionItems
         //  await SeedProductOptionsAsync();

            // 6. Seed remaining entities
            await SeedEntitiesAsync<DeliveryMethod>("Seeds/DeliveryMethod.json");
            await SeedInventoriesAsync(await _dbContext.Locations.ToListAsync());
            await SeedWorkingHoursAsync(await _dbContext.Restaurants.ToListAsync());

            // 7. Seed entities that depend on multiple other entities
            await SeedOrderItemsAsync();
            await SeedOrderItemOptionsAsync();
         //   await SeedOptionItemsAsync();
            await SeedReportsAsync();
            await SeedUserPermissionsAsync();
            await SeedStockMovesAsync();
        }

        private async Task<List<Admin>> SeedAdminsAsync()
        {
            try
            {
                if (await _dbContext.Admins.AnyAsync())
                    return await _dbContext.Admins.ToListAsync();

                var admins = await LoadEntitiesFromJson<Admin>("Seeds/Admin.json");

                if (!admins.Any())
                {
                    admins.Add(new Admin { Name = "Default Admin" });
                    _logger.LogWarning("No admins found in JSON, created default admin");
                }

                await _dbContext.Admins.AddRangeAsync(admins);
                await _dbContext.SaveChangesAsync();
                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding admins");
                if (!_continueOnError) throw;
                return new List<Admin>();
            }
        }

        private async Task SeedCustomersAsync()
        {
            try
            {
                if (await _dbContext.Customers.AnyAsync())
                    return;

                var customers = await LoadEntitiesFromJson<Customer>("Seeds/Customer.json");

                if (!customers.Any())
                {
                    // Create a default customer if none exist
                    customers.Add(new Customer
                    {
                        Name = "Default Customer",
                        CreatedOn = DateTime.Now,
                        LastModifiedOn = DateTime.Now,
                        CreatedBy = "System",
                        LastModifiedBy = "System"
                    });
                    _logger.LogWarning("No customers found in JSON, created default customer");
                }
                else
                {
                    // Ensure no NULL CustomerName values
                    foreach (var customer in customers)
                    {
                        if (string.IsNullOrEmpty(customer.Name))
                        {
                            customer.Name = "Unnamed Customer";
                            _logger.LogWarning($"Fixed NULL CustomerName for Customer ID: {customer.Id}");
                        }
                    }
                }

                await _dbContext.Customers.AddRangeAsync(customers);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding customers");
                if (!_continueOnError) throw;
            }
        }

        private async Task<List<T>> SeedEntitiesAsync<T>(string filePath) where T : class
        {
            try
            {
                if (await _dbContext.Set<T>().AnyAsync())
                    return await _dbContext.Set<T>().ToListAsync();

                var entities = await LoadEntitiesFromJson<T>(filePath);

                if (entities.Any())
                {
                    await _dbContext.Set<T>().AddRangeAsync(entities);
                    await _dbContext.SaveChangesAsync();
                }

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error seeding {typeof(T).Name}");
                if (!_continueOnError) throw;
                return new List<T>();
            }
        }

        private async Task<List<Restaurant>> SeedRestaurantsAsync(List<RestaurantCategory> categories)
        {
            try
            {
                if (await _dbContext.Restaurants.AnyAsync())
                    return await _dbContext.Restaurants.ToListAsync();

                var restaurants = await LoadEntitiesFromJson<Restaurant>("Seeds/Restaurant.json");

                if (!restaurants.Any())
                    return new List<Restaurant>();

                var validCategoryIds = categories.Select(c => c.Id).ToList();

                foreach (var restaurant in restaurants)
                {
                    restaurant.CategoryId = ValidateForeignKey(restaurant.CategoryId, validCategoryIds);

                    if (string.IsNullOrEmpty(restaurant.Name))
                    {
                        restaurant.Name = "Unnamed Restaurant";
                    }
                }

                await _dbContext.Restaurants.AddRangeAsync(restaurants);
                await _dbContext.SaveChangesAsync();
                return restaurants;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding restaurants");
                if (!_continueOnError) throw;
                return new List<Restaurant>();
            }
        }

        private async Task<List<WarehouseStaff>> SeedWarehouseStaffAsync(List<Admin> admins)
        {
            try
            {
                if (await _dbContext.WarehouseStaffs.AnyAsync())
                    return await _dbContext.WarehouseStaffs.ToListAsync();

                var staff = await LoadEntitiesFromJson<WarehouseStaff>("Seeds/WarehouseStaff.json");

                if (!staff.Any())
                    return new List<WarehouseStaff>();

                var validAdminIds = admins.Select(a => a.Id).ToList();

                foreach (var s in staff)
                {
                    s.ManageById = ValidateForeignKey(s.ManageById, validAdminIds);
                }

                await _dbContext.WarehouseStaffs.AddRangeAsync(staff);
                await _dbContext.SaveChangesAsync();
                return staff;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding warehouse staff");
                if (!_continueOnError) throw;
                return new List<WarehouseStaff>();
            }
        }

        private async Task<List<Cashier>> SeedCashiersAsync(List<Admin> admins)
        {
            try
            {
                if (await _dbContext.Cashiers.AnyAsync())
                    return await _dbContext.Cashiers.ToListAsync();

                var cashiers = await LoadEntitiesFromJson<Cashier>("Seeds/Cashier.json");

                if (!cashiers.Any())
                    return new List<Cashier>();

                var validAdminIds = admins.Select(a => a.Id).ToList();

                foreach (var cashier in cashiers)
                {
                    cashier.AdminId = ValidateForeignKey(cashier.AdminId, validAdminIds);
                }

                await _dbContext.Cashiers.AddRangeAsync(cashiers);
                await _dbContext.SaveChangesAsync();
                return cashiers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding cashiers");
                if (!_continueOnError) throw;
                return new List<Cashier>();
            }
        }

        private async Task SeedProductsAsync(
            List<Category> categories,
            List<Location> locations,
            List<Supplier> suppliers,
            List<Restaurant> restaurants,
            List<WarehouseStaff> warehouseStaff)
        {
            try
            {
                if (await _dbContext.Products.AnyAsync())
                    return;

                var products = await LoadEntitiesFromJson<Product>("Seeds/Product.json");

                if (!products.Any())
                    return;

                var validCategoryIds = categories.Select(c => c.Id).ToList();
                var validLocationIds = locations.Select(l => l.Id).ToList();
                var validSupplierIds = suppliers.Select(s => s.Id).ToList();
                var validRestaurantIds = restaurants.Select(r => r.Id).ToList();
                var validStaffIds = warehouseStaff.Select(w => w.Id).ToList();

                foreach (var product in products)
                {
                    product.CategoryId = ValidateForeignKey(product.CategoryId, validCategoryIds);
                    product.LocationId = ValidateForeignKey(product.LocationId, validLocationIds);
                    product.SupplierId = ValidateForeignKey(product.SupplierId, validSupplierIds);
                    product.RestaurantId = ValidateForeignKey(product.RestaurantId, validRestaurantIds);
                    product.WarehousStaffId = ValidateForeignKey(product.WarehousStaffId, validStaffIds);
                }

                await _dbContext.Products.AddRangeAsync(products);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding products");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedInventoriesAsync(List<Location> locations)
        {
            try
            {
                if (await _dbContext.Inventories.AnyAsync())
                    return;

                var inventories = await LoadEntitiesFromJson<Inventory>("Seeds/Inventory.json");

                if (!inventories.Any())
                    return;

                var validLocationIds = locations.Select(l => l.Id).ToList();

                foreach (var inventory in inventories)
                {
                    // Handle both null and invalid cases
                    if (!inventory.LocationId.HasValue || inventory.LocationId.Value == 0 || !validLocationIds.Contains(inventory.LocationId.Value))
                    {
                        inventory.LocationId = validLocationIds.Any() ? validLocationIds.First() : 0;
                    }
                }

                await _dbContext.Inventories.AddRangeAsync(inventories);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding inventories");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedWorkingHoursAsync(List<Restaurant> restaurants)
        {
            try
            {
                if (await _dbContext.WorkingHours.AnyAsync())
                    return;

                var workingHours = await LoadEntitiesFromJson<WorkingHours>("Seeds/WorkingHours.json");

                if (!workingHours.Any())
                    return;

                var validRestaurantIds = restaurants.Select(r => r.Id).ToList();

                foreach (var wh in workingHours)
                {
                    wh.RestaurantId = ValidateForeignKey(wh.RestaurantId, validRestaurantIds);
                }

                await _dbContext.WorkingHours.AddRangeAsync(workingHours);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding working hours");
                if (!_continueOnError) throw;
            }
        }

        private int? ValidateForeignKey(int? foreignKeyId, List<int> validIds)
        {
            if (!foreignKeyId.HasValue || foreignKeyId.Value == 0)
                return null;

            if (validIds.Contains(foreignKeyId.Value))
                return foreignKeyId;

            return validIds.Any() ? validIds.First() : null;
        }

        private async Task SeedReportsAsync()
        {
            try
            {
                if (await _dbContext.Reports.AnyAsync())
                    return;

                var reports = await LoadEntitiesFromJson<Report>("Seeds/Report.json");

                if (reports.Any())
                {
                    // Validate InventoryId foreign keys
                    var validInventoryIds = await _dbContext.Inventories.Select(i => i.Id).ToListAsync();

                    // Get a default inventory ID if any exist
                    int defaultInventoryId = validInventoryIds.Any() ? validInventoryIds.First() : 0;

                    foreach (var report in reports)
                    {
                        if (!validInventoryIds.Contains(report.InventoryId))
                        {
                            report.InventoryId = defaultInventoryId;
                        }
                    }

                    await _dbContext.Reports.AddRangeAsync(reports);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding reports");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedUserPermissionsAsync()
        {
            try
            {
                if (await _dbContext.UserPermissions.AnyAsync())
                    return;

                var permissions = await LoadEntitiesFromJson<UserPermissions>("Seeds/UserPermissions.json");

                if (permissions.Any())
                {
                    await _dbContext.UserPermissions.AddRangeAsync(permissions);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding user permissions");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedStockMovesAsync()
        {
            try
            {
                if (await _dbContext.StockMoves.AnyAsync())
                    return;

                var stockMoves = await LoadEntitiesFromJson<StockMove>("Seeds/StockMove.json");

                if (stockMoves.Any())
                {
                    await _dbContext.StockMoves.AddRangeAsync(stockMoves);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding stock moves");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedOrderItemsAsync()
        {
            try
            {
                if (await _dbContext.OrderItems.AnyAsync())
                    return;

                var orderItems = await LoadEntitiesFromJson<OrderItem>("Seeds/OrderItem.json");

                if (!orderItems.Any())
                    return;

                // Validate OrderId foreign keys
                var validOrderIds = await _dbContext.Orders.Select(o => o.Id).ToListAsync();
                _logger.LogInformation($"Found {validOrderIds.Count} valid Order IDs");

                var validItems = new List<OrderItem>();
                foreach (var item in orderItems)
                {
                    // Fix: Check if OrderId has value before comparing
                    if (item.OrderId.HasValue && validOrderIds.Contains(item.OrderId.Value))
                    {
                        validItems.Add(item);
                    }
                    else
                    {
                        _logger.LogWarning($"Skipping OrderItem with invalid OrderId: {item.OrderId}");
                    }
                }

                if (validItems.Any())
                {
                    await _dbContext.OrderItems.AddRangeAsync(validItems);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning("No valid OrderItems found with matching Order IDs");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding order items");
                if (!_continueOnError) throw;
            }
        }

        private async Task SeedOrderItemOptionsAsync()
        {
            try
            {
                if (await _dbContext.OrderItemOptions.AnyAsync())
                    return;

                var orderItemOptions = await LoadEntitiesFromJson<OrderItemOption>("Seeds/OrderItemOption.json");

                if (orderItemOptions.Any())
                {
                    await _dbContext.OrderItemOptions.AddRangeAsync(orderItemOptions);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding order item options");
                if (!_continueOnError) throw;
            }
        }

        // Method to seed ProductOptions before OptionItems
        //private async Task SeedProductOptionsAsync()
        //{
        //    try
        //    {
        //        if (await _dbContext.ProductOptions.AnyAsync())
        //            return;

        //        var productOptions = await LoadEntitiesFromJson<ProductOption>("Seeds/ProductOption.json");

        //        // Log for debugging
        //        _logger.LogInformation($"Loaded {productOptions.Count} ProductOptions from JSON");

        //        if (productOptions.Any())
        //        {
        //            // Check for duplicate IDs
        //            var distinctIds = productOptions.Select(po => po.Id).Distinct().Count();
        //            if (distinctIds < productOptions.Count)
        //            {
        //                _logger.LogWarning("Found duplicate IDs in ProductOptions JSON. Fixing duplicates...");

        //                // Create a dictionary to track seen IDs
        //                var seenIds = new HashSet<int>();
        //                var uniqueOptions = new List<ProductOption>();

        //                foreach (var option in productOptions)
        //                {
        //                    if (!seenIds.Contains(option.Id))
        //                    {
        //                        seenIds.Add(option.Id);
        //                        uniqueOptions.Add(option);
        //                    }
        //                    else
        //                    {
        //                        _logger.LogWarning($"Skipping duplicate ProductOption with ID: {option.Id}, Name: {option.Name}");
        //                    }
        //                }

        //                productOptions = uniqueOptions;
        //                _logger.LogInformation($"After removing duplicates: {productOptions.Count} ProductOptions");
        //            }

        //            // Validate ProductId foreign keys
        //            var validProductIds = await _dbContext.Products.Select(p => p.Id).ToListAsync();
        //            foreach (var option in productOptions)
        //            {
        //                // Fix: Check if ProductId has value before comparing
        //                if (!option.ProductId.HasValue || !validProductIds.Contains(option.ProductId.Value))
        //                {
        //                    option.ProductId = validProductIds.Any() ? validProductIds.First() : 1;
        //                    _logger.LogWarning($"Fixed invalid ProductId for ProductOption: {option.Name}");
        //                }
        //            }

        //            await _dbContext.ProductOptions.AddRangeAsync(productOptions);
        //            await _dbContext.SaveChangesAsync();

        //            // Log for debugging
        //            _logger.LogInformation("ProductOptions seeded successfully");
        //        }
        //        else
        //        {
        //            _logger.LogWarning("No ProductOptions found in JSON file");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error seeding product options");
        //        if (!_continueOnError) throw;
        //    }
        //}

        //private async Task SeedOptionItemsAsync()
        //{
        //    try
        //    {
        //        if (await _dbContext.OptionItems.AnyAsync())
        //            return;

        //        // Check if ProductOptions exist
        //        var productOptionsCount = await _dbContext.ProductOptions.CountAsync();
        //        _logger.LogInformation($"Found {productOptionsCount} ProductOptions in database");

        //        // If no ProductOptions exist, seed them first
        //        if (productOptionsCount == 0)
        //        {
        //            _logger.LogWarning("No ProductOptions found in database. Seeding ProductOptions first...");
        //            await SeedProductOptionsAsync();

        //            // Check again after seeding
        //            productOptionsCount = await _dbContext.ProductOptions.CountAsync();
        //            _logger.LogInformation($"After seeding: Found {productOptionsCount} ProductOptions in database");

        //            // If still no ProductOptions, we can't proceed
        //            if (productOptionsCount == 0)
        //            {
        //                _logger.LogError("Failed to seed ProductOptions. Cannot seed OptionItems.");
        //                return;
        //            }
        //        }

        //        // Get valid ProductOption IDs
        //        var validOptionIds = await _dbContext.ProductOptions.Select(po => po.Id).ToListAsync();
        //        _logger.LogInformation($"Valid ProductOption IDs: {string.Join(", ", validOptionIds)}");

        //        var optionItems = await LoadEntitiesFromJson<OptionItem>("Seeds/OptionItem.json");
        //        _logger.LogInformation($"Loaded {optionItems.Count} OptionItems from JSON");

        //        if (!optionItems.Any())
        //            return;

        //        // Filter out items with invalid OptionId
        //        var validItems = new List<OptionItem>();
        //        foreach (var item in optionItems)
        //        {
        //            if (item.OptionId.HasValue && validOptionIds.Contains(item.OptionId.Value))
        //            {
        //                validItems.Add(item);
        //            }
        //            else
        //            {
        //                _logger.LogWarning($"Skipping OptionItem '{item.Name}' with invalid OptionId: {item.OptionId}");
        //            }
        //        }

        //        if (validItems.Any())
        //        {
        //            _logger.LogInformation($"Adding {validItems.Count} valid OptionItems to database");
        //            await _dbContext.OptionItems.AddRangeAsync(validItems);
        //            await _dbContext.SaveChangesAsync();
        //            _logger.LogInformation("OptionItems seeded successfully");
        //        }
        //        else
        //        {
        //            _logger.LogWarning("No valid OptionItems found with matching ProductOption IDs");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error seeding option items");
        //        if (!_continueOnError) throw;
        //    }
        //}

        private async Task<List<T>> LoadEntitiesFromJson<T>(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                if (!File.Exists(fullPath))
                {
                    _logger.LogWarning($"File not found: {fullPath}");
                    return new List<T>();
                }

                var jsonData = await File.ReadAllTextAsync(fullPath);
                return JsonSerializer.Deserialize<List<T>>(jsonData, _jsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading {typeof(T).Name} from JSON");
                if (!_continueOnError) throw;
                return new List<T>();
            }
        }
    }
}
