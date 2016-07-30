using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    [RoutePrefix("Store")]
    public class StoreController : Controller
    {
        // GET: Store
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}