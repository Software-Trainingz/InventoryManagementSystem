using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Entities
{
   public class Product :BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Barcode { get; set; }

        public  string ? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public DateTime LastRestockDate { get; set; }
        public int MinimumStockLevel { get; set; } = 10;
        public string ? ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }



        // الخصائص الجديدة للتكامل مع طلبات
        public int ? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool IsAvailableOnline { get; set; }
        public float PreparationTime { get; set; }
        public virtual ICollection<ProductOption> Options { get; set; } = new HashSet<ProductOption>();

        // خصائص الربط مع طلبات
        public int? TalabatProductId { get; set; }
        public DateTime? LastSyncedWithTalabat { get; set; }



        //RelationShip

        public int ? WarehousStaffId { get; set; }
        public virtual WarehouseStaff ManageBy { get; set; }

        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();


        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }



        public int ? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public virtual ICollection<InventoryLine> InventoryLines { get; set; } = new HashSet<InventoryLine>();

        public virtual ICollection<StockMove> StokeMoves { get; set; } = new HashSet<StockMove>();

        public virtual Location Location { get; set; }
        public int? LocationId { get; set; } // Foreign key for Location





    }

}
