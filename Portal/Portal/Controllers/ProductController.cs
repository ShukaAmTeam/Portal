using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Entities;
using Portal.Services;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        private readonly ProductService _products;

        #region ctor
        public ProductController() : this(new ProductService()) { }
        public ProductController(ProductService productService)
        {
            _products = productService;
        }
        #endregion ctor

        // GET: Product
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
    }
}