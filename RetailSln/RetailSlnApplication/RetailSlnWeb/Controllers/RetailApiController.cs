using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public class RetailApiController : Controller
    {
        // GET: RetailApi
        public ActionResult Index()
        {
            return View();
        }
    }
}
