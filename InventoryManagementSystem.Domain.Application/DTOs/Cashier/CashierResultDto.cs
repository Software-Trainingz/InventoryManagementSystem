using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Cashier
{
    public class CashierResultDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string HireDate { get; set; }
            public AdminDto Admin { get; set; }
            public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
        }
    
}
