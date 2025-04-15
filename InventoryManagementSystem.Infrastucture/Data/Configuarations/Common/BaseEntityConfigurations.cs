using InventoryManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastucture.Data.Configuarations.Common
{
    abstract class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
         where TEntity : BaseEntity<TKey>
         where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }


}
