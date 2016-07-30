using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Data;
using Portal.Entities;

namespace Portal.Services
{
    public class StoreService
    {
        private readonly IProductRepository _products;

        public StoreService() : this(new ProductRepository()) { }
        public StoreService(IProductRepository productRepository)
        {
            _products = productRepository;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _products.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _products.GetAllAsync();
        }
    }
}
