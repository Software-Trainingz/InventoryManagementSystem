using InventoryManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastucture.Data.Configuarations.Common
{
    class BaseAuditableEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x=>x.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastModifiedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CreatedOn)
                .IsRequired();


            builder.Property(x => x.LastModifiedOn)
                .IsRequired();
         }
    }

}
