using InventoryManagementSystem.Core.Domain.Common;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class StockMove :BaseAuditableEntity<int>
    {
        public DateTime MoveDate { get; set; } = DateTime.Now; // تاريخ الحركة

        public float Quantity { get; set; } // الكمية المنقولة

        public string Reference { get; set; } // رقم الإشارة (أمر شحن/رقم إذن)

        public string State { get; set; } = "Draft"; // حالة الحركة (مسودة/مؤكدة/ملغية)
        public string? Notes { get; set; } // ملاحظات (مثال: "نقل سريع لفرع الأسكندرية")

        public string MoveType { get; set; }

        public decimal? TransferCost { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SourceLocationId { get; set; } // المخزن المصدر
        public Location SourceLocation { get; set; }

        public int DestinationLocationId { get; set; } // المخزن الهدف
        public Location DestinationLocation { get; set; }

        public int? ResponsibleStaffId { get; set; } // الموظف المسؤول
        public WarehouseStaff? ResponsibleStaff { get; set; }
    }
}