using System.Web.Optimization;

namespace RecipiesWebFormApp
{
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;
            BundleTable.EnableOptimizations = true;


            // this way no other request is made for the map file
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/*.js")
                .Include("~/Scripts/*.map")
                .Include("~/Scripts/kendo/js/*.js")
                .Include("~/Scripts/kendo/js/*.map")
                .Include("~/Scripts/kendo/js/cultures/*.js")
                .Include("~/Scripts/kendo/js/cultures/*.map"));


            bundles.Add(new StyleBundle("~/Content/bundles/css")
                .Include("~/Content/Bundles/*.css")
                .Include("~/Content/Bundles/BlueOpal/*.png")
                .Include("~/Content/Bundles/BlueOpal/*.gif"))
                ;



        }
    }
}