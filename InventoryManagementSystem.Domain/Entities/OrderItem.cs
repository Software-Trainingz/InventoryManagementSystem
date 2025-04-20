using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderItem : BaseAuditableEntity<int>
    {
         public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LineTotal { get; set; }
        public virtual ICollection<OrderItemOption> SelectedOptions { get; set; } = new HashSet<OrderItemOption>();
        public string ? Notes { get; set; }
    }

}