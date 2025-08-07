using ArchitectureLibraryUtility;
using Microsoft.Owin;
using Owin;
using RetailSlnBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(RetailSlnWeb.Startup))]

namespace RetailSlnWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            CallStartupAction();
        }
        private void CallStartupAction()
        {
            long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            retailSlnBL.ItemCatalogCreateAll(null, null, null, null, null, clientId, "", "WebStartup", "");
            /*
            // Create an instance of the controller
            Controller controller = new BaseController(); // Replace HomeController with your controller name

            // Create a controller context (optional, but good practice for some scenarios)
            HttpContextWrapper httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Base"); // Replace Home with your controller name
            routeData.Values.Add("action", "Index"); // Replace StartupAction with your action name

            var requestContext = new RequestContext(httpContextWrapper, routeData);
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            */
            /*
            //ArchLibBL archLibBL = new ArchLibBL();
            //string temp = archLibBL.ViewToHtmlString(controller, "_Ummachi", null);
            // Create a controller context (optional, but good practice for some scenarios)
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Base"); // Replace Home with your controller name
            routeData.Values.Add("action", "ItemCatalogInit"); // Replace StartupAction with your action name

            var requestContext = new RequestContext(httpContext, routeData);
            controller.ControllerContext = new ControllerContext(requestContext, controller);

            // Call the action method
            controller.ItemCatalogInit(); // Replace StartupAction with your action name
            */
        }
    }
}
