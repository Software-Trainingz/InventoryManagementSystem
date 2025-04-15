namespace InventoryManagementSystem.Domain.Entities
{
    public class RestaurantCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();

        // خصائص الربط مع طلبات
        public int? TalabatCategoryId { get; set; }
    }

}