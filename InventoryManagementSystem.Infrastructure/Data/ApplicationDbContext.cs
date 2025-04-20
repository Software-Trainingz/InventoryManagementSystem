using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Infrastucture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<WarehouseStaff> WarehouseStaffs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockMove> StockMoves { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryLine> InventoryLines { get; set; }
    //    public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        // Changed from Categories to RestaurantCategories
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }

        // Keep this for Product categories
        public DbSet<Category> Categories { get; set; }

        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<OrderItemOption> OrderItemOptions { get; set; }
        //public DbSet<OptionItem> OptionItems { get; set; }
   //     public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure correct table mappings
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<RestaurantCategory>().ToTable("RestaurantCategories");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
