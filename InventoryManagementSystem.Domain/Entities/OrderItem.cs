using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderItem
    {
        public int LineId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
        public float LineTotal { get; set; }
        public virtual ICollection<OrderItemOption> SelectedOptions { get; set; } = new HashSet<OrderItemOption>();
        public string Notes { get; set; }
    }

}