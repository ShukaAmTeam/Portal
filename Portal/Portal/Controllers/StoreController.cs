using Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    [RoutePrefix("Store")]
    public class StoreController : Controller
    {
        private readonly StoreService _store;

        #region ctor
        public StoreController() : this(new StoreService()) { }
        public StoreController(StoreService storeService)
        {
            _store = storeService;
        }
        #endregion ctor


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index(int? id)
        {
            if (!id.HasValue)
                ViewData["products"] = await _store.GetProducts();
            else
                return View(await _store.GetProduct(id.Value));

            return View();
        }
    }
}