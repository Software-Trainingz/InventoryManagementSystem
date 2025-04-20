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

namespace InventoryManagementSystem.Infrastructure.Data.Configuarations
{
    class ProductConfigurations :BaseAuditableEntityConfigurations<Product,int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            // Configure properties
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Barcode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.QuantityOnHand)
                .HasDefaultValue(0);

            builder.Property(p => p.QuantityReserved)
                .HasDefaultValue(0);

            builder.Property(p => p.MinimumStockLevel)
                .HasDefaultValue(10);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            //builder.Property(p => p.IsActive)
            //    .HasDefaultValue(true);

            builder.Property(p => p.IsAvailableOnline)
                .HasDefaultValue(false);

            builder.Property(p => p.PreparationTime)
                .HasDefaultValue(0f);

            //builder.Property(p => p.IsSyncedWithTalabat)
            //    .HasDefaultValue(false);

            // Configure computed columns (if supported by your database)
            builder.Property(p => p.AvailableQuantity)
                .HasComputedColumnSql("[QuantityOnHand] - [QuantityReserved]");

            //builder.Property(p => p.NeedsRestock)
            //    .HasComputedColumnSql("CASE WHEN ([QuantityOnHand] - [QuantityReserved]) < [MinimumStockLevel] THEN 1 ELSE 0 END");

            // Configure relationships
            builder.HasOne(p => p.Category)
                .WithMany(c=>c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ManageBy)
                .WithMany()
                .HasForeignKey(p => p.WarehousStaffId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Location)
                .WithMany(l =>l.Products)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Options)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Reports)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.InventoryLines)
                .WithOne(il => il.Product)
                .HasForeignKey(il => il.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.StokeMoves)
                .WithOne(sm => sm.Product)
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Restaurant>(p=>p.Restaurant)
                .WithMany(r=>r.Menu)
                .OnDelete(DeleteBehavior.NoAction);
        }
    
    }
}
