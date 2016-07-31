using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Portal.Entities;
using Portal.Services;
using Portal.Data;

namespace Portal.Controllers
{
    [RoutePrefix("Store")]
    public class StoreController : Controller
    {
        private readonly StoreService _store;

        #region ctor
        public StoreController() : this(new StoreService(new DemoProductRepository(100))) { }
        public StoreController(StoreService storeService)
        {
            _store = storeService;
        }
        #endregion ctor


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index(int? id)
        {
            try
            {
                var x = Request.Url;
                IEnumerable<Product> products = null;
                if (id.HasValue)
                {
                    var product = await _store.GetProduct(id.Value);
                    if (product != null)
                    {
                        products = new List<Product>() { product };             
                    }
                }
                else
                    products = await _store.GetProducts();

                return View(products);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}