using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Entities;

namespace Portal.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        //Task<ProductViewModel> GetByIdAsModelAsync(int id);
        Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId);
        Task<IEnumerable<Product>> GetByAsync(string categoryId, string subcategoryId, int pageNumber, int pageSize);
    }
}
