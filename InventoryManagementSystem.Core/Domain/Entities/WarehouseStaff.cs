using InventoryManagementSystem.Core.Domain.Common;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class WarehouseStaff: BaseAuditableEntity<int>
    {

        public required string Name { get; set; }

        public required string Position { get; set; }

        public required string Shift { get; set; }

        public int AdminId { get; set; }
        public required Admin ManageBy { get; set; }


        public required ICollection<Product> Products { get; set; }

        public ICollection<StockMove> StokeMoves { get; set; }


    }
}