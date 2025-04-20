using InventoryManagementSystem.Domain.Common;
using System.Collections.ObjectModel;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Supplier :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public required string CompanyName { get; set; }

        public required string Address { get; set; }
        public virtual ICollection<Product> Products { get; set; }=new HashSet<Product> ();
    }
}