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
    class OrderItemOptionConfiguration :BaseAuditableEntityConfigurations<OrderItemOption,int>
    {
        public override void Configure(EntityTypeBuilder<OrderItemOption> builder)
        {
            base.Configure(builder);
            builder.Property(oio => oio.OptionName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(oio => oio.OptionItemName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(oio => oio.AdditionalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Relationships
            builder.HasOne(oio => oio.OrderItem)
                .WithMany(oi => oi.SelectedOptions) // Assuming OrderItem has ICollection<OrderItemOption>
                .HasForeignKey(oio => oio.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade); // Delete options when order item is deleted

            // Indexes for better query performance
            builder.HasIndex(oio => oio.OrderItemId);
            builder.HasIndex(oio => oio.OptionId);
            builder.HasIndex(oio => oio.OptionItemId);

            // Optional: Composite index if you frequently query by both OptionId and OptionItemId
            builder.HasIndex(oio => new { oio.OptionId, oio.OptionItemId });
        }
    }
}
