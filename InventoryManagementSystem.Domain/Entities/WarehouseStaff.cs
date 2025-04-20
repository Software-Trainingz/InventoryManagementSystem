using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class WarehouseStaff: BaseAuditableEntity<int>
    {

        public required string Name { get; set; }

        public required string Position { get; set; }

        public required string Shift { get; set; }

        public int?  ManageById { get; set; }
        public virtual Admin ManageBy { get; set; }


        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public virtual ICollection<StockMove> StokeMoves { get; set; } = new HashSet<StockMove>();

        public virtual ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>(); 



    }
}