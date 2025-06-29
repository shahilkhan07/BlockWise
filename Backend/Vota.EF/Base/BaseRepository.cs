using Vota.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vota.EF.Base
{
    public abstract class BaseRepository<TEntity, TDbContext> where TEntity : BaseModel, new()
    {
        private readonly VotaDbContext _context;

        /// <summary>
        /// Create repository
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(VotaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get TEntity
        /// </summary>
        /// <returns>TEntities</returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }

        /// <summary>
        /// Add TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            //entity.Id = Guid.NewGuid();
            entity.IsActive = true;
            entity.CreatedAt = DateTime.UtcNow;
            entity.ModifiedAt = DateTime.UtcNow;
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Get TEntity by id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>TEntity</returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await GetQuery().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public virtual async Task<TEntity> ChangeAsync(TEntity entity)
        {
            entity.ModifiedAt = DateTime.UtcNow;
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Update(entity);
            }
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public virtual async Task<TEntity> RemoveAsync(TEntity entity)
        {
            entity.IsActive = false;
            entity.ModifiedAt = DateTime.UtcNow;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected virtual IQueryable<TEntity> GetQuery()
        {
            var query = _context.Set<TEntity>().AsNoTracking().Where(x => x.IsActive == true);
            return query;
        }
    }
}
