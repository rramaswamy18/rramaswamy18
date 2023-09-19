using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.Controllers
{
    public class SVCCTempleFremontController : Controller
    {
        private const string LOCATION_NAME_DESC = "FREMONT";
        private const string LOCATION_NAME_DESC1 = "Fremont";
        private string execUniqueId = Utilities.CreateExecUniqueId();
        // GET: SVCCTempleFremont
        public ActionResult Index()
        {
            return View();
        }
    }
}
