using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Cashier
{
    public class CashierCreateDto
    {
        public string Name { get; set; }

        public string HireDate { get; set; }

        public int? AdminId { get; set; }
    }

  
}
