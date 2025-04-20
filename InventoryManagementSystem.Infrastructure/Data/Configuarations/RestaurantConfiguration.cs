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
    class RestaurantConfiguration :BaseAuditableEntityConfigurations<Restaurant,int>
    {
        public override void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            base.Configure(builder);
            builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            builder.Property(r => r.LogoUrl)
                .HasMaxLength(255);

            builder.Property(r => r.CoverImageUrl)
                .HasMaxLength(255);

            builder.Property(r => r.MinimumOrderAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.PhoneNumber)
                .HasMaxLength(20);

            // Relationships
            builder.HasOne<RestaurantCategory>(r=>r.Category)
                .WithMany(c => c.Restaurants)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.CategoryId);
        }
    }
}
