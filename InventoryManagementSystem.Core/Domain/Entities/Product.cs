using InventoryManagementSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Domain.Entities
{
   public class Product :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Barcode { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public DateTime LastRestockDate { get; set; }
        public int MinimumStockLevel { get; set; } = 10;
        public string ? Category { get; set; }
        public string ? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int AvailableQuantity => QuantityOnHand - QuantityReserved;
        public bool NeedsRestock => AvailableQuantity < MinimumStockLevel;


        //RelationShip

        public int ? WarehousStaffId { get; set; }
        public required WarehouseStaff ManageBy { get; set; }


        public int? SupplierId { get; set; }
        public required Supplier Supplier { get; set; }




        public required ICollection<OrderLine> OrderLines { get; set; }

        public required ICollection<InventoryLine> InventoryLines { get; set; }

        public required ICollection<StockMove> StokeMoves { get; set; }

        public Location Location { get; set; }
        public int LocationId { get; set; } // Foreign key for Location





    }

}
