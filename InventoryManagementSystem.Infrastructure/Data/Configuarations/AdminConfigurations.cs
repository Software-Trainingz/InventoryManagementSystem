using InventoryManagementSystem.Domain.Common;
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
    class AdminConfigurations :BaseEntityConfigurations<Admin,int>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();

            // تعريف العلاقات من جانب الأدمن
            builder.HasMany(a => a.Cashiers)
                   .WithOne(c => c.Admin)
                   .HasForeignKey(c => c.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
