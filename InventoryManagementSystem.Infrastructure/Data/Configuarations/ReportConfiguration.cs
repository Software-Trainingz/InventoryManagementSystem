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
    class ReportConfiguration :BaseAuditableEntityConfigurations<Report,int>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);
            builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Format)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(r => r.StoragePath)
                .IsRequired()
                .HasMaxLength(255);

            // Relationships
            builder.HasOne(r => r.GenerateBy)
                .WithMany(a=>a.Reports)
                .HasForeignKey(r => r.AdminId)
               .OnDelete(DeleteBehavior.Restrict) ;


            builder.HasOne(r => r.Inventory)
                .WithMany()
                .HasForeignKey(r => r.InventoryId)
               .OnDelete(DeleteBehavior.Restrict) ;
                

            builder.HasOne(r => r.Order)
                .WithMany()
                .HasForeignKey(r => r.OrderId);

            builder.HasOne(r => r.Product)
                .WithMany(p=>p.Reports)
                .HasForeignKey(r => r.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
