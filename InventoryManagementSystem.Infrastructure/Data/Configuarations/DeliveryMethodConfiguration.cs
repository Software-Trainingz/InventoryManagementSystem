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
    class DeliveryMethodConfiguration :BaseEntityConfigurations<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);
            builder.Property(dm => dm.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(dm => dm.Description)
                .HasMaxLength(500);

            builder.Property(dm => dm.Cost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(dm => dm.DeliveryTime)
                .IsRequired()
                .HasMaxLength(50); // e.g., "30-45 minutes"

            //builder.Property(dm => dm.IsActive)
            //    .HasDefaultValue(true);

            //// Index for frequently queried active delivery methods
            //builder.HasIndex(dm => dm.IsActive);

            // Relationship with Orders (configured on Order side)
            //builder.HasMany(dm => dm.Orders)
            //    .WithOne(o => o.DeliveryMethod)
            //    .HasForeignKey(o => o.DeliveryMethodId)
            //    .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if used in orders
        }
    }
}
