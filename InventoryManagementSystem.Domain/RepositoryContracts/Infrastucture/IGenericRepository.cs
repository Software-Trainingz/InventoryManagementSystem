using InventoryManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture
{
  public  interface IGenericRepository<TEntity,TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool  withNoTracking=false);
        Task<TEntity?> GetAsync(TKey id);

        void Add(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity  entity);


    }
}
