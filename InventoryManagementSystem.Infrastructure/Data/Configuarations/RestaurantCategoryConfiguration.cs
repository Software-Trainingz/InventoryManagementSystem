using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Infrastucture.Data.Configuarations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastucture.Data.Configuarations
{
    class RestaurantCategoryConfiguration : BaseAuditableEntityConfigurations<RestaurantCategory, int>
    {

        public override void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            base.Configure(builder);
            builder.Property(rc => rc.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(rc => rc.Description)
                .HasMaxLength(500);

            builder.Property(rc => rc.ImageUrl)
                .HasMaxLength(255);
        }

    }
    
}
