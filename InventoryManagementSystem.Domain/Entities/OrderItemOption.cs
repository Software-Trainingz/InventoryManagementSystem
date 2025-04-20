using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class OrderItemOption :BaseAuditableEntity<int>
    {
        public int? OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public int OptionId { get; set; }
        public int OptionItemId { get; set; }
        public string OptionName { get; set; }
        public string OptionItemName { get; set; }
        public decimal AdditionalPrice { get; set; }
    }

}