using InventoryManagementSystem.Core.Domain.Common;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Cashier :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }


        // RelationShip
        public ICollection<Order>  Orders { get; set; }

        public int AdminId { get; set; }
        public required Admin ManagedBy { get; set; }
    }
}