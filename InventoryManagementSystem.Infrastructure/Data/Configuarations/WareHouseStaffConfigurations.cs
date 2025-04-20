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
    class WareHouseStaffConfigurations :BaseAuditableEntityConfigurations<WarehouseStaff,int>
    {
        public override void Configure(EntityTypeBuilder<WarehouseStaff> builder)
        {
            base.Configure(builder);
            builder.Property(W => W.Name)
          .IsRequired()
          .HasMaxLength(50);

            builder.Property(W => W.Shift)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(W => W.Position)
                .IsRequired()
                .HasMaxLength(50);

            // Configure Admin relationship
            builder.HasOne<Admin>(a=>a.ManageBy)
                .WithMany(W => W.WarehouseStaffs)
                .HasForeignKey(W => W.ManageById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
