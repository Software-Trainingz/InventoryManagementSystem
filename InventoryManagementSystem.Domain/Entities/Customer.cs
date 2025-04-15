using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Domain.Common;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Customer : BaseAuditableEntity<int>
    {
        public string Name { get; set; } // اسم العميل

      
        public int LoyaltyPoints { get; set; } = 0; // نقاط الولاء

        public string? Address { get; set; } // العنوان

        public string? CustomerType { get; set; } // "عادي", "تاجر", "شركة"

        // العلاقات
        public virtual ICollection<Order> Orders { get; set; }= new HashSet<Order>();

        public string? TaxNumber { get; set; } // الرقم الضريبي

         public string? Notes { get; set; } // ملاحظات خاصة
 
    }
}
