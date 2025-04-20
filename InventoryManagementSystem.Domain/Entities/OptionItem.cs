using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class OptionItem :BaseAuditableEntity<int>
    {
        public int? OptionId { get; set; }
        public virtual ProductOption Option { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }

        // خصائص الربط مع طلبات
        public int? TalabatOptionItemId { get; set; }
    }

}