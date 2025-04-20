using InventoryManagementSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain.Entities
{
    public class InventoryLine :BaseAuditableEntity<int>
    {
        // الكمية النظرية الموجودة في النظام

        public decimal TheoreticalQty { get; set; }

        // الكمية الفعلية بعد الجرد
        public decimal CountedQty { get; set; }

        // الفرق بين النظرية والفعلي (تحسب تلقائيًا)
        public decimal Difference { get; set; }
        public string? Notes { get; set; }

        // العلاقات
        public int ? InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; } 

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}