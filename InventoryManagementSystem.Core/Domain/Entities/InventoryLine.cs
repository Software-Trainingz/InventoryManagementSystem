using InventoryManagementSystem.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class InventoryLine :BaseAuditableEntity<int>
    {
        // الكمية النظرية الموجودة في النظام

        public float TheoreticalQty { get; set; }

        // الكمية الفعلية بعد الجرد
        public float CountedQty { get; set; }

        // الفرق بين النظرية والفعلي (تحسب تلقائيًا)
        public float Difference => CountedQty - TheoreticalQty;

        public string? Notes { get; set; }

        // العلاقات
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}