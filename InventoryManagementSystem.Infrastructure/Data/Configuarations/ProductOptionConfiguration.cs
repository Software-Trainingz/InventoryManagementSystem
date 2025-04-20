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
    class ProductOptionConfiguration :BaseAuditableEntityConfigurations<ProductOption,int>
    {
        public override void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            base.Configure(builder);

            builder.Property(po => po.Name)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(po => po.Description)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(po => po.Product)
                .WithMany(p => p.Options)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            
          
        }

    }
}
