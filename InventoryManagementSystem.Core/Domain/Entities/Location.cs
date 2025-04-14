using InventoryManagementSystem.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Location :BaseAuditableEntity<int>
    {
      public string Name { get; set; } // اسم المخزن (مثال: "مخزن القاهرة الرئيسي")

        public string Code { get; set; } // كود مختصر (مثال: "CAI-WH01")


        public string Type { get; set; } // نوع المكان (مخزن/فرع/منفذ بيع)

        public string Address { get; set; } // العنوان التفصيلي

        public bool IsActive { get; set; } = true; // هل المكان شغال ولا مقفول

        public ICollection<Inventory> Inventories { get; set; } // الجردات اللي اتعملت في المخزن ده
        public ICollection<StockMove> SourceMoves { get; set; } // حركات الصادر من المخزن
        public ICollection<StockMove> DestinationMoves { get; set; } // حركات الوارد للمخزن
        public ICollection<Product> Products { get; set; } // المنتجات الموجودة في المخزن ده

    }
}