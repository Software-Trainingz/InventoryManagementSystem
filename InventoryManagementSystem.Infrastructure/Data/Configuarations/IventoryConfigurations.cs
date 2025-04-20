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
    class IventoryConfigurations :BaseAuditableEntityConfigurations<Inventory, int>
    {
        public override void Configure(EntityTypeBuilder<Inventory> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.Status)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(i => i.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.ApprovedBy)
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(i => i.StartDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(i => i.EndDate)
                .HasDefaultValueSql("GetDate()");

            builder.HasOne(i => i.Location)
                .WithMany(l => l.Inventories)
                .HasForeignKey(i => i.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
    
}
