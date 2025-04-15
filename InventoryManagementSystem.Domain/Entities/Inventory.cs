using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Inventory : BaseAuditableEntity<int>
    {

        
        public string Name { get; set; } // مثال: "جرد ربع سنوي لمخزن القاهرة"

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } // null يعني الجرد لسه مستمر
        public string Status { get; set; } = "Draft"; // Draft, InProgress, Completed, Cancelled

        public string? ApprovedBy { get; set; } // اسم المدير الاي وافق

         public  virtual ICollection<InventoryLine> InventoryLines { get; set; } = new HashSet<InventoryLine>(); 

        public int? LocationId { get; set; }
        public virtual Location? Location { get; set; }

        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();



    }
}