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
    class CustomerConfigurations :BaseAuditableEntityConfigurations<Customer,int>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name)
             .IsRequired()
             .HasMaxLength(150)
             .HasColumnName("CustomerName"); // اسم العميل

            builder.Property(c => c.LoyaltyPoints)
                .HasDefaultValue(0); // نقاط الولاء

            builder.Property(c => c.Address)
                .HasMaxLength(500); // العنوان

            builder.Property(c => c.CustomerType)
                .HasMaxLength(30)
                .HasConversion<string>(); // نوع العميل ("عادي", "تاجر", "شركة")

            builder.Property(c => c.TaxNumber)
                .HasMaxLength(50); // الرقم الضريبي

            builder.Property(c => c.Notes)
                .HasMaxLength(2000); // ملاحظات خاصة

            // العلاقات
            // Relationships
            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // منع حذف العميل إذا كان لديه طلبات

            // الفهارس
            // Indexes
            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Customer_Name");

            builder.HasIndex(c => c.TaxNumber)
                .IsUnique()
                .HasFilter("[TaxNumber] IS NOT NULL")
                .HasDatabaseName("IX_Customer_TaxNumber");

            builder.HasIndex(c => c.CustomerType)
                .HasDatabaseName("IX_Customer_Type");

            // إعدادات إضافية
            // Additional settings
            builder.Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.LastModifiedOn)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
