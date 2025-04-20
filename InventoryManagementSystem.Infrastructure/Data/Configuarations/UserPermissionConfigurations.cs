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
    class UserPermissionConfigurations :BaseEntityConfigurations<UserPermissions,int>
    {
        public override void Configure(EntityTypeBuilder<UserPermissions> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Description)
     .IsRequired(false)
     .HasMaxLength(100);

            builder.Property(p => p.PermissionName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Module)
                .IsRequired()
                .HasMaxLength(50);

            // Configure Admin relationship
            builder.HasOne<Admin>(up=>up.Admin)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure many-to-many with WarehouseStaff
            builder.HasMany(up => up.WarehouseStaffs)
                .WithMany(ws => ws.UserPermissions)
                .UsingEntity<Dictionary<string, object>>(
                    "UserPermissionsWarehouseStaff",
                    j => j.HasOne<WarehouseStaff>()
                        .WithMany()
                        .HasForeignKey("WarehouseStaffsId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<UserPermissions>()
                        .WithMany()
                        .HasForeignKey("UserPermissionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                );
        }
    }
}
