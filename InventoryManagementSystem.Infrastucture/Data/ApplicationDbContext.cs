using InventoryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Infrastucture.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options):base(options)
        {
           
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<WarehouseStaff> WarehouseStaffs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockMove> StockMoves { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderLines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryLine> InventoryLines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
