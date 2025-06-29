using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vota.Domain.Base
{
    /// <summary>
    /// Base service interface
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public interface IBaseService<TEntity> where TEntity : BaseModel, new()
    {
        /// <summary>
        /// Get TEntities
        /// </summary>
        /// <returns>TEntities</returns>
        public Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Add TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Get TEntity by id 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> ChangeAsync(TEntity entity);

        /// <summary>
        /// Delete TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>Action result</returns>
        public Task RemoveAsync(TEntity entity);
    }
}
