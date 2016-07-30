using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all <TEntity> objects
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get <TEntity> object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);
        //Task<ProductViewModel> GetByIdAsModelAsync(int id);

        /// <summary>
        /// Get <TEntity> object by query string
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByAsync(string queryString);
        Task<TEntity> CreateAsync(TEntity newModel);
        Task<TEntity> UpdateAsync(string id, TEntity updatedModel);
        Task<TEntity> RemoveAsync(string id);
    }
}
