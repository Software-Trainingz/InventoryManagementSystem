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
    class StockMoveConfiguration :BaseAuditableEntityConfigurations<StockMove,int>
    {
        public override void Configure(EntityTypeBuilder<StockMove> builder)
        {
            base.Configure(builder);

            builder.Property(sm => sm.Reference)
           .IsRequired()
           .HasMaxLength(50);

            builder.Property(sm => sm.State)
                .HasDefaultValue("Draft")
                .HasMaxLength(20);

            builder.Property(sm => sm.MoveType)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(sm => sm.TransferCost)
                .HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasOne(sm => sm.Product)
                .WithMany(p => p.StokeMoves)
                .HasForeignKey(sm => sm.ProductId);

            builder.HasOne(sm => sm.SourceLocation)
                .WithMany(l => l.SourceMoves)
                .HasForeignKey(sm => sm.SourceLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sm => sm.DestinationLocation)
                .WithMany(l => l.DestinationMoves)
                .HasForeignKey(sm => sm.DestinationLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sm => sm.ResponsibleStaff)
                .WithMany()
                .HasForeignKey(sm => sm.ResponsibleStaffId);
        }
    }
}
