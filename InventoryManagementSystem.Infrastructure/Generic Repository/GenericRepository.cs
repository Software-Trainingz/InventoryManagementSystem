using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.RepositoryContracts.Infrastucture;
using InventoryManagementSystem.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Generic_Repository
{
  public  class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            return withTracking ?
                     await _dbContext.Set<TEntity>().ToListAsync() :
                     await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        }
        public async Task<TEntity?> GetAsync(TKey id)
        
           => await _dbContext.Set<TEntity>().FindAsync(id);
        
        public void Add(TEntity entity)
         =>_dbContext.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
       => _dbContext.Set<TEntity>().Update(entity);
   
    }
}
