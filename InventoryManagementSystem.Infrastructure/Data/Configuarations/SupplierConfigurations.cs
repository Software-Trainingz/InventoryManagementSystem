using InventoryManagementSystem.Domain.Common;
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
    class SupplierConfigurations :BaseAuditableEntityConfigurations<Supplier,int>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);
            builder.Property(S=>S.Name).IsRequired()
                .HasMaxLength(50);
            builder.Property(S=>S.Address)
                .IsRequired() .HasMaxLength(50);

            builder.Property(S=>S.CompanyName)
                .IsRequired()
                .HasMaxLength (50);
        }
    }
}
