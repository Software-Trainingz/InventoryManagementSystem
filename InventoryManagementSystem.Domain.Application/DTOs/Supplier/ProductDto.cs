using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Supplier
{
   public class ProductDto
    {
        public required string Name { get; set; }
        public required string Barcode { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public DateTime LastRestockDate { get; set; }
        public int MinimumStockLevel { get; set; } = 10;
        public string? ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }

        public int? CategoryId { get; set; }
        public bool IsAvailableOnline { get; set; }
        public float PreparationTime { get; set; }

     






    }
}
