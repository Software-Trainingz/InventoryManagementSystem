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
    class OrdersItemConfigurations : BaseAuditableEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.Property(oi => oi.Quantity)
                .IsRequired();
            builder.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(oi => oi.LineTotal)
              .HasColumnType("decimal(18,2)")
              .IsRequired();

            builder.Property(oi => oi.Discount)
              .HasColumnType("decimal(18,2)")
              .IsRequired();

            builder.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(oi => oi.Notes)
              .IsRequired(false)
              .HasMaxLength(100);

          builder.HasOne(oi=>oi.Order)
                .WithMany(oi=>oi.OrderItems)
                .HasForeignKey(oi=>oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(oi=>oi.Product)
              .WithMany(m=>m.OrderItems)
              .HasForeignKey(oi => oi.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

        }
    }
    
}
