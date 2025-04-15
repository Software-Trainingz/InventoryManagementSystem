using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
   public class Report :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public required string Type { get; set; }

        public required string Format { get; set; }
        public required string Parameters { get; set; }

        public required string StoragePath { get; set; }


        public int AdminId { get; set; }
        public virtual Admin GenerateBy { get; set; }


        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


    }

}
