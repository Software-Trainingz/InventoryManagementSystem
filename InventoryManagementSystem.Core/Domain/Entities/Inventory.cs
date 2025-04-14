using InventoryManagementSystem.Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Inventory : BaseAuditableEntity<int>
    {

        
        public string Name { get; set; } // مثال: "جرد ربع سنوي لمخزن القاهرة"

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } // null يعني الجرد لسه مستمر
        public string Status { get; set; } = "Draft"; // Draft, InProgress, Completed, Cancelled

        public string? ApprovedBy { get; set; } // اسم المدير الاي وافق

         public ICollection<InventoryLine> InventoryLines { get; set; } 

        public int? LocationId { get; set; }
        public Location? Location { get; set; }

       
       
    }
}