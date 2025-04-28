using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.WareHouseStaff
{
  public  class WareHouseStaffReturnDto
    {
        public  string Name { get; set; }

        public  string Position { get; set; }

        public  string Shift { get; set; }

        public  AdminDto ManageBy { get; set; }


        public Product Products { get; set; } 
        public StockMoveDto StokeMoves { get; set; } 

        public string UserPermissions { get; set; }


    }
}
