using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Entities;
using Portal.Services;
using System.Threading.Tasks;
using Portal.Data;

namespace Portal.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        private readonly ProductService _products;

        #region ctor
        public ProductController() : this(new ProductService(new DemoProductRepository(100))) { }
        public ProductController(ProductService productService)
        {
            _products = productService;
        }
        #endregion ctor

        // GET: Product/
        [Route("")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var p = await _products.GetProduct();
                return View(p);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
       
        // GET: Product/Details/{id}
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return HttpNotFound();
                
                var product = await _products.GetProductById(id.Value);

                if (product == null)                
                    return HttpNotFound();
                
                return View(product);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}