using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
    public class UserPermissions :BaseEntity<int>
    {
        public required  string PermissionName { get; set; }

        public string ? Description { get; set; }

        public string Module { get; set; }

        public bool CanCreate { get; set; }
        public bool CanUpdate{ get; set; }
        public bool CanDelete{ get; set; }

        // Navigational Property
        public int ? AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        public virtual ICollection<Cashier> Cashiers { get; set; } = new HashSet<Cashier>();

        public virtual ICollection<WarehouseStaff> WarehouseStaffs { get; set; } = new HashSet<WarehouseStaff>();


    }
}
