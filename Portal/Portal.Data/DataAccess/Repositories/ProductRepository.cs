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

    public class DemoProductRepository : IProductRepository
    {
        private List<Product> _products;

        public DemoProductRepository(int count)
        {
            InitProducts(count);
        }
        private void InitProducts(int count)
        {
            if (_products == null)
                _products = new List<Product>();
            for (int i = 1; i <= count; i++)
            {
                _products.Add(new Product
                {
                    Id = i,
                    Name = "Product " + i,
                    ImageUrl = "http://www.freshfoodcentral.com/uploads/Potatoes/potatoes-20060407-02.jpg", 
                    CostPrice = i * 100,
                    Price = i * 10,
                    Subcategory = new Subcategory() { Name = "Subcategory", Category = new Category() { Name = "Category" } },
                });
            }
        }



        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return _products;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return _products.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByAsync(string queryString)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> CreateAsync(Product newModel)
        {
            if (newModel.Id != 0)
                throw new Exception("Id is not 0");
            if (_products.Any(p => p.Id == newModel.Id))
                throw new Exception("there is product/s with id of " + newModel.Id + " in DB");

            _products.Add(newModel);

            return newModel;
        }

        public async Task<Product> UpdateAsync(string id, Product updatedModel)
        {
            if (updatedModel.Id == 0)
                throw new Exception("Id is 0");
            if (!_products.Any(p => p.Id == updatedModel.Id))
                throw new Exception("there is no product/s with id of " + updatedModel.Id + " in DB");
            var prod = _products.Single(p => p.Id == updatedModel.Id);
            _products.Remove(prod);
            _products.Add(updatedModel);

            return updatedModel;
        }

        public async Task<Product> RemoveAsync(string id)
        {
            if (!_products.Any(p => p.Id == int.Parse(id)))
                throw new Exception("there is no product/s with id of " + id + " in DB to delete");

            var prod = _products.Single(p => p.Id == int.Parse(id));
            _products.Remove(prod);

            return prod;
        }

    }
}

