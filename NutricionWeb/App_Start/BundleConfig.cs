using System.Web;
using System.Web.Optimization;

namespace NutricionWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/fontawesome/all.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/fullcalendar/fullcalendar.js",
                      "~/Scripts/fullcalendar/lang/es.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js",
                      "~/Scripts/jscolor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/jquery-ui.css",
                "~/Content/site.css",
                "~/Content/MiHojita.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/fullcalendar.css"
            ).Include("~/Content/fontawesome.css", new CssRewriteUrlTransform()));
        }
    }
}
