using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vota.Domain.Base
{
    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public class BaseService<TEntity> where TEntity : BaseModel, new()
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        /// <summary>
        /// Creates base service 
        /// </summary>
        /// <param name="baseRepository">Base repository</param>
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Get TEntities
        /// </summary>
        /// <returns>TEntities</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        /// <summary>
        /// Get TEntity by id 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Add TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _baseRepository.AddAsync(entity);
        }

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> ChangeAsync(TEntity entity)
        {
            return await _baseRepository.ChangeAsync(entity);
        }

        /// <summary>
        /// Delete TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>Action result</returns>
        public async Task RemoveAsync(TEntity entity)
        {
            await _baseRepository.RemoveAsync(entity);
        }

    }
}
