using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class RestaurantCategory : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();

        // خصائص الربط مع طلبات
        public int? TalabatCategoryId { get; set; }
    }

}