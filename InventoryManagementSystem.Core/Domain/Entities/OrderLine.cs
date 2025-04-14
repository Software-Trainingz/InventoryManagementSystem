using InventoryManagementSystem.Core.Domain.Common;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class OrderLine :BaseEntity<int>
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal LineTotal => (UnitPrice - Discount) * Quantity;

        // RelationShip

        public int OrderId { get; set; }

        public Order Order { get; set; }


        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}