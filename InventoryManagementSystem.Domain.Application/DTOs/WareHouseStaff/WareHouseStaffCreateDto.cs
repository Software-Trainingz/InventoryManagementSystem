using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.WareHouseStaff
{
   public class WareHouseStaffCreateDto
    {
        public DateTime MoveDate { get; set; } = DateTime.Now; // تاريخ الحركة

        public float Quantity { get; set; } // الكمية المنقولة

        public string Reference { get; set; } // رقم الإشارة (أمر شحن/رقم إذن)

        public string State { get; set; } = "Draft"; // حالة الحركة (مسودة/مؤكدة/ملغية)
        public string? Notes { get; set; } // ملاحظات (مثال: "نقل سريع لفرع الأسكندرية")

        public string MoveType { get; set; }

        public decimal? TransferCost { get; set; }

        public int? ProductId { get; set; }

    }
}
