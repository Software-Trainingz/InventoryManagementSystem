using InventoryManagementSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Inventory : BaseAuditableEntity<int>
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string? ApprovedBy { get; set; }

        // Relationships
        public int ? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<InventoryLine> InventoryLines { get; set; } = new HashSet<InventoryLine>();
    }
}