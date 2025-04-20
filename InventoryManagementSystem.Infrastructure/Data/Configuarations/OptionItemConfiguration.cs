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
    class OptionItemConfiguration :BaseAuditableEntityConfigurations<OptionItem,int>
    {
        public override void Configure(EntityTypeBuilder<OptionItem> builder)
        {
            base.Configure(builder);

            builder.Property(oi => oi.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(oi => oi.AdditionalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<ProductOption>(oi=>oi.Option)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.OptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
