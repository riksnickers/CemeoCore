using System.Web;
using System.Web.Optimization;

namespace CeMeOCore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/assets/css/home.css",
                      "~/Content/site.css"));

            //Load Cemeo bundle jqeury
            bundles.Add(new ScriptBundle("~/bundles/cemeo").Include(
                "~/Scripts/cemeo.js",
                "~/assets/js/images.js"
                ));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/assets/css/bootstrap.css",
                      "~/assets/css/sb-admin.css",
                      "~/assets/css/font-awesome.min.css",
                      "~/assets/css/jquery-ui.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/assets/js/jquery-1.10.2.js",
                "~/assets/js/chart-data-flot.js",
                "~/assets/js/jquery.flot.js",
                "~/assets/js/jquery.flot.pie.js",
                "~/assets/js/jquery.flot.resize.js",
                "~/assets/js/jquery.flot.tooltip.min.js",
                "~/assets/js/jquery-ui.js",
                "~/assets/js/bootstrap.js",
                "~/assets/js/images.js",
                "~/assets/js/tablesorter/jquery.tablesorter.js",
                "~/assets/js/tablesorter/tables.js"
                ));
        }
    }
}
