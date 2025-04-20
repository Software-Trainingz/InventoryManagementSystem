using InventoryManagementSystem.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Infrastructure.Json
{
    [JsonSourceGenerationOptions(
        WriteIndented = false,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(Admin))]
    [JsonSerializable(typeof(Cashier))]
    [JsonSerializable(typeof(Category))]
    [JsonSerializable(typeof(Customer))]
    [JsonSerializable(typeof(DeliveryMethod))]
    [JsonSerializable(typeof(Inventory))]
    [JsonSerializable(typeof(InventoryLine))]
    [JsonSerializable(typeof(Location))]
    [JsonSerializable(typeof(OptionItem))]
    [JsonSerializable(typeof(Order))]
    [JsonSerializable(typeof(OrderItem))]
    [JsonSerializable(typeof(Product))]
    [JsonSerializable(typeof(ProductOption))]
    [JsonSerializable(typeof(Report))]
    [JsonSerializable(typeof(Restaurant))]
    [JsonSerializable(typeof(Supplier))]
    [JsonSerializable(typeof(UserPermissions))]
    [JsonSerializable(typeof(WarehouseStaff))]
    [JsonSerializable(typeof(WorkingHours))]
    // إضافة أنواع المصفوفات والقوائم
    [JsonSerializable(typeof(List<Admin>))]
    [JsonSerializable(typeof(List<Cashier>))]
    [JsonSerializable(typeof(List<Category>))]
    [JsonSerializable(typeof(List<Customer>))]
    [JsonSerializable(typeof(List<DeliveryMethod>))]
    [JsonSerializable(typeof(List<Inventory>))]
    [JsonSerializable(typeof(List<InventoryLine>))]
    [JsonSerializable(typeof(List<Location>))]
    [JsonSerializable(typeof(List<OptionItem>))]
    [JsonSerializable(typeof(List<Order>))]
    [JsonSerializable(typeof(List<OrderItem>))]
    [JsonSerializable(typeof(List<Product>))]
    [JsonSerializable(typeof(List<ProductOption>))]
    [JsonSerializable(typeof(List<Report>))]
    [JsonSerializable(typeof(List<Restaurant>))]
    [JsonSerializable(typeof(List<Supplier>))]
    [JsonSerializable(typeof(List<UserPermissions>))]
    [JsonSerializable(typeof(List<WarehouseStaff>))]
    [JsonSerializable(typeof(List<WorkingHours>))]

    public partial class InventorySystemJsonContext : JsonSerializerContext
    {
        // Remove the manual GeneratedSerializerOptions definition completely
        // Remove the _defaultOptions field

        public static InventorySystemJsonContext Instance { get; } = new();
    }
}
