using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
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



        // الخصائص الجديدة للتكامل مع طلبات
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }
        public bool IsAvailableOnline { get; set; }
        public float PreparationTime { get; set; }
        public virtual ICollection<ProductOption> Options { get; set; } = new HashSet<ProductOption>();

        // خصائص الربط مع طلبات
        public int? TalabatProductId { get; set; }
        public DateTime? LastSyncedWithTalabat { get; set; }
        public bool IsSyncedWithTalabat { get; set; }



        //RelationShip

        public int ? WarehousStaffId { get; set; }
        public virtual WarehouseStaff ManageBy { get; set; }

        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();


        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }




        public virtual ICollection<OrderItem> OrderLines { get; set; } = new HashSet<OrderItem>();

        public virtual ICollection<InventoryLine> InventoryLines { get; set; } = new HashSet<InventoryLine>();

        public virtual ICollection<StockMove> StokeMoves { get; set; } = new HashSet<StockMove>();

        public virtual Location Location { get; set; }
        public int LocationId { get; set; } // Foreign key for Location





    }

}
