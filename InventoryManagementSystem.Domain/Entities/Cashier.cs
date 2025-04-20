using InventoryManagementSystem.Domain.Common;
using System;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Cashier : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
        public DateTime HireDate { get; set; }

        // العلاقة مع Admin
        public int ? AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}