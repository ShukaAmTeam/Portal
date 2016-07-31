using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Data;
using Portal.Entities;

namespace Portal.Services
{
    public class ProductService
    {
        private readonly IProductRepository _products;

        public ProductService() : this(new ProductRepository()) { }
        public ProductService(IProductRepository productRepository)
        {
            _products = productRepository;
        }
             
        public async Task<Product> GetProduct()
        {
            return await _products.GetByIdAsync(1);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _products.GetAllAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _products.GetByIdAsync(id);
        }
    }
}
