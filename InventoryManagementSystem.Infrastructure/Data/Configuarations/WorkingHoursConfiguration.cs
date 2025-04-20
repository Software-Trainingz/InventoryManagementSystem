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
    class WorkingHoursConfiguration :BaseEntityConfigurations<WorkingHours,int>
    {
        public override void Configure(EntityTypeBuilder<WorkingHours> builder)
        {
            base.Configure(builder);

            builder.Property(wh => wh.DayOfWeek)
                .IsRequired()
                .HasConversion<string>(); // Store as string in database

            builder.Property(wh => wh.OpenTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(wh => wh.CloseTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(wh => wh.IsClosed)
                .HasDefaultValue(false);

            // Relationship with Restaurant
            builder.HasOne(wh => wh.Restaurant)
                .WithMany(r => r.WorkingHours)
                .HasForeignKey(wh => wh.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            // Index for better query performance
            builder.HasIndex(wh => new { wh.RestaurantId, wh.DayOfWeek })
                .IsUnique();
        }
    }
}
