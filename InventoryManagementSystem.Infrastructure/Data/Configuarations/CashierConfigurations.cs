using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Infrastucture.Data.Configuarations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastucture.Data.Configuarations
{
    class CashierConfigurations :BaseAuditableEntityConfigurations<Cashier,int>
    {
        public override void Configure(EntityTypeBuilder<Cashier> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.HireDate).IsRequired();
          //  builder.Property(c => c.IsActive).HasColumnName("ActiveStatus");

            // لا نحتاج لإعادة تعريف العلاقة هنا إذا تم تعريفها في AdminConfigurations
            
        }
    }
}
