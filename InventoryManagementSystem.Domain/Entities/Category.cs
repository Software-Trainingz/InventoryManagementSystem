using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Category :BaseAuditableEntity<int>
    {
         public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

        // خصائص الربط مع طلبات
        public int? TalabatCategoryId { get; set; }
    }

}