using System.Web.Optimization;

namespace SeoAnalyzer.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-3.3.1.js",
                        "~/Scripts/popper.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/datatables.js"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/theme.css",
                      "~/Content/datatables.css"));
        }
    }
}
