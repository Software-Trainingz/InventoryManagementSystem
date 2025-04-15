using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Cashier :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }


        // RelationShip
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public int AdminId { get; set; }
        public virtual Admin ManagedBy { get; set; }

        public virtual ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
    }
}