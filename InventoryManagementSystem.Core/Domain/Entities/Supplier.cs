using InventoryManagementSystem.Core.Domain.Common;
using System.Collections.ObjectModel;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Supplier :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public required string CompanyName { get; set; }

        public required  string Address { get; set; }
        public ICollection<Product> Products { get; set; }


    }
}