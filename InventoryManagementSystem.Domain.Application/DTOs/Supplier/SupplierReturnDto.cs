using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Supplier
{
  public  class SupplierReturnDto
    {
        public required string Name { get; set; }

        public required string CompanyName { get; set; }

        public required string Address { get; set; }
        public ProductDto Products { get; set; }
    }
}
