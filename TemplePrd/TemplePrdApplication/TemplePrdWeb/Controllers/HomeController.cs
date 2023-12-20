using ArchitectureLibraryCacheData;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplePrdWeb.Controllers
{
    public class HomeController : Controller
    {
        private string clientSuffix;
        private long clientId;
        private string clientName;
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            string absoluteUri = Request.Url.AbsoluteUri;
            if (absoluteUri.ToUpper().IndexOf("FREMONT") > -1)
            {
                clientSuffix = "Fremont";
            }
            else
            {
                clientSuffix = "Sacramento";
            }
            clientId = long.Parse(Utilities.GetApplicationValue("ClientId" + clientSuffix));
            clientName = Utilities.GetApplicationValue("ClientName" + clientSuffix);
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
