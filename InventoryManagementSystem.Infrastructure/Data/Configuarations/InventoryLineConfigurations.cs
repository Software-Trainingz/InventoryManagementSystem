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
    class InventoryLineConfigurations :BaseAuditableEntityConfigurations<InventoryLine,int>
    {
        public override void Configure(EntityTypeBuilder<InventoryLine> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.TheoreticalQty)
            .HasColumnType("decimal(18,2)");

            builder.Property(i => i.CountedQty)
               .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Difference)
            .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Notes)
                .IsRequired(false)
                .HasMaxLength(200);

            // Single relationship to Inventory
            builder.HasOne(il => il.Inventory)
                .WithMany(i => i.InventoryLines)
                .HasForeignKey(il => il.InventoryId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to ClientSetNull

            // Single relationship to Product
            builder.HasOne(il => il.Product)
                .WithMany(p => p.InventoryLines)
                .HasForeignKey(il => il.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
