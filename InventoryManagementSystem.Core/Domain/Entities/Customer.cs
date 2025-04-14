using InventoryManagementSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Domain.Entities
{
    public class Customer : BaseAuditableEntity<int>
    {
        public string Name { get; set; } // اسم العميل

      
        public int LoyaltyPoints { get; set; } = 0; // نقاط الولاء

        [StringLength(200)]
        public string? Address { get; set; } // العنوان

        [StringLength(50)]
        public string? CustomerType { get; set; } // "عادي", "تاجر", "شركة"

        // العلاقات
        public ICollection<Order> Orders { get; set; } 

        public string? TaxNumber { get; set; } // الرقم الضريبي

         public string? Notes { get; set; } // ملاحظات خاصة
 
    }
}
