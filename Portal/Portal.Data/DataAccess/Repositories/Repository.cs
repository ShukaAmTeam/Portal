using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApiStoreClient _client;
        protected readonly string _routePrefix = "api/";

        public Repository(ApiStoreClient client, string routePrefix_key)
        {
            _client = client;
            _routePrefix = ApiStoreClientConfig.GetRoutPrefix(routePrefix_key);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _client.GetAsync<IEnumerable<TEntity>>(_routePrefix);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _client.GetAsync<TEntity>($"{_routePrefix}/{id}");
        }

        public async Task<IEnumerable<TEntity>> GetByAsync(string queryString)
        {
            return await _client.GetAsync<IEnumerable<TEntity>>($"{_routePrefix}?{queryString}");
        }
        public async Task<TEntity> CreateAsync(TEntity newModel)
        {
            return await _client.PostAsync<TEntity, TEntity>(_routePrefix, newModel);
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity updatedModel)
        {
            return await _client.PutAsync<TEntity, TEntity>($"{_routePrefix}/{id}", updatedModel);
        }

        public async Task<TEntity> RemoveAsync(string id)
        {
            return await _client.DeleteAsync<TEntity>($"{_routePrefix}/{id}");
        }
    }
}

