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
    class LocationConfiguration :BaseAuditableEntityConfigurations<Location,int>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            base.Configure(builder);
            builder.Property(l => l.Name)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(l => l.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.Address)
                .HasMaxLength(200);

            //builder.Property(l => l.IsActive)
            //    .HasDefaultValue(true);
             
        }
    }
}
