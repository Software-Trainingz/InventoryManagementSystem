using InventoryManagementSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Location :BaseAuditableEntity<int>
    {
      public string Name { get; set; } // اسم المخزن (مثال: "مخزن القاهرة الرئيسي")

        public string Code { get; set; } // كود مختصر (مثال: "CAI-WH01")


        public string Type { get; set; } // نوع المكان (مخزن/فرع/منفذ بيع)

        public string Address { get; set; } // العنوان التفصيلي

        public bool IsActive { get; set; } = true; // هل المكان شغال ولا مقفول

        public virtual ICollection<Inventory> Inventories { get; set; } = new HashSet<Inventory>(); // الجردات اللي اتعملت في المخزن ده
        public virtual ICollection<StockMove> SourceMoves { get; set; } = new HashSet<StockMove>(); // حركات الصادر من المخزن
        public virtual ICollection<StockMove> DestinationMoves { get; set; } = new HashSet<StockMove>(); // حركات الوارد للمخزن
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>(); // المنتجات الموجودة في المخزن ده

    }
}