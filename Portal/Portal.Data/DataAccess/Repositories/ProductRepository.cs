using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Entities;

namespace Portal.Data
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        #region ctor

        public ProductRepository() : base(ApiStoreClient.Instance, "product") { }
        //public ProductRepository(ApiStoreClient client)
        //{
        //    _client = client;
        //}
        
        #endregion ctor

        
        public async Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId)
        {
            return await _client.GetAsync<IEnumerable<Product>>(
                $"{_routePrefix}?categoryId={categoryId}&subcategoryId={subcategoryId}");
        }

        public async Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId, int pageNumber, int pageSize)
        {
            return await _client.GetAsync<IEnumerable<Product>>(
                $"{_routePrefix}?pageNumber={pageNumber}&pageSize={pageSize}&categoryId={categoryId}&subcategoryId={subcategoryId}");

        }
    }
}
