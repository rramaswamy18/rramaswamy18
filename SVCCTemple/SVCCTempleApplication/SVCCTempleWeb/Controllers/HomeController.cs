using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string absoluteUri = Request.Url.AbsoluteUri;
            if (absoluteUri.ToUpper().IndexOf("FREMONT") > -1)
            {
                return RedirectToAction("Index", "SVCCTempleFremont");
            }
            else
            {
                return RedirectToAction("Index", "SVCCTempleSacramento");
            }
        }
    }
}
