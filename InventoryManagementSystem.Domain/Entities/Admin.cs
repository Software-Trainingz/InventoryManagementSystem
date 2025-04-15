using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
   public class Admin : BaseEntity<int>
    {
        public required string Name { get; set; }

        public virtual ICollection<Cashier> Cashiers { get; set; }= new HashSet<Cashier>();

        public virtual ICollection<WarehouseStaff> WarehouseStaffs { get; set; } = new HashSet<WarehouseStaff>();

        public virtual ICollection<Report>  Reports { get; set; }= new HashSet<Report>();

        public virtual ICollection<UserPermissions> UserPermissions { get; set; }=new HashSet<UserPermissions>();

    }
}
