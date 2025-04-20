using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Restaurant : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public float MinimumOrderAmount { get; set; }
        public float AverageRating { get; set; }
        public int EstimatedDeliveryTimeMinutes { get; set; }
        //public bool IsOpen { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<WorkingHours> WorkingHours { get; set; } = new HashSet<WorkingHours>();
        public virtual ICollection<Product> Menu { get; set; } = new HashSet<Product>();

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public int ? CategoryId { get; set; }
        public virtual RestaurantCategory Category { get; set; }

        // خصائص الربط مع طلبات
        public int? TalabatRestaurantId { get; set; }
    }

}