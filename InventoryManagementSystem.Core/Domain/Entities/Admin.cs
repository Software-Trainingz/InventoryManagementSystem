using InventoryManagementSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Domain.Entities
{
   public class Admin : BaseEntity<int>
    {
        public required string Name { get; set; }

        public required ICollection<Cashier> Cashiers { get; set; }

        public required ICollection<WarehouseStaff> WarehouseStaffs { get; set; }

    }
}
