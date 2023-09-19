using System.Web;
using System.Web.Optimization;

namespace SchoolPrdWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //Begin Ravi Sep 4 2023
            bundles.Add(new StyleBundle("~/Content/css0").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap.css",
                      "~/Content/meanmenu.css",
                      "~/Include/StyleSheet1.css",
                      "~/Content/Site0.css"));
            bundles.Add(new ScriptBundle("~/bundles/scripts0").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery-ui.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.unobtrusive.js",
                      "~/Scripts/jquery.validate.unobtrusive-ajax.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.meanmenu.js",
                      "~/Include/javaScriptCode-{version}.js"));
            //End Ravi Sep 4 2023
        }
    }
}
