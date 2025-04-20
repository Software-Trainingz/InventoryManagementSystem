using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Infrastucture.Data.Configuarations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastucture.Data.Configuarations
{
    class OrderConfigurations : BaseAuditableEntityConfigurations<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            // Configure properties
            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasDefaultValueSql("Getdate()");

            builder.Property(o => o.DeliveryFee)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.BuyerEmail)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.Street)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Country)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(o => o.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.SpecialInstructions)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(o => o.EstimatedDeliveryTime)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(o => o.TalabatOrderStatus)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.TalabatOrderId)
                .IsRequired();

            builder.Property(o => o.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.LastName)
                .HasMaxLength(50)
                .IsRequired();

            // Explicitly configure foreign key properties
            builder.Property(o => o.CashierId)
                .IsRequired(false); // Make nullable if not all orders have cashiers

            builder.Property(o => o.RestaurantId)
                .IsRequired(false); // Make nullable if needed

            builder.Property(o => o.CustomerId)
                .IsRequired();

            //builder.Property(o => o.DeliveryMethodId)
            //    .IsRequired(false); // Make nullable if needed

            // Configure relationships with proper navigation properties
            builder.HasOne(o => o.Cashier)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CashierId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(o => o.DeliveryMethod)
            //    .WithMany(d => d.Orders)
            //    .HasForeignKey(o => o.DeliveryMethodId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired(false);

            //// Configure index for frequently queried fields
            builder.HasIndex(o => o.OrderDate);
            builder.HasIndex(o => o.Status);
            builder.HasIndex(o => o.TalabatOrderId)
                .IsUnique();
        }
    }
}