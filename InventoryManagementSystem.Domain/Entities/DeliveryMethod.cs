using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class DeliveryMethod :BaseEntity<int>
    {
         public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        // Properties to link with TalabatMart
        public int? TalabatDeliveryMethodId { get; set; }
    }

}