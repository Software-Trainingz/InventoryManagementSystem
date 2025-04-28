using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs
{
   public class CustomerDto
    {
        public string Name { get; set; } // اسم العميل


        public int LoyaltyPoints { get; set; } = 0; // نقاط الولاء

        public string? Address { get; set; } // العنوان

    }
}
