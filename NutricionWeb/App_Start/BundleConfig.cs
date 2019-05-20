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
                        "~/Scripts/fontawesome/all.js",
                "~/Scripts/chosen.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/fullcalendar/fullcalendar.js",
                      "~/Scripts/fullcalendar/lang/es.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/jscolor.js",
                      "~/Scripts/Chart.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/jquery-ui.css",
                "~/Content/site.css",
                "~/Content/MiHojita.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/fullcalendar.min.css",
                "~/Content/Chosen/chosen.min.css",
                "~/Content/Chart.min.css",
                "~/Content/sidebar.css"
            ).Include("~/Content/fontawesome.css", new CssRewriteUrlTransform()));
        }
    }
}
