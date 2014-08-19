using System;
using System.Web.Optimization;

namespace KStore.Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery",
                    "//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js").Include(
            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/Admin/css", "//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css").Include(
              "~/Content/bootstrap.css",
              "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap", "//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js").Include(
          "~/Scripts/libs/bootstrap.js",
          "~/Scripts/libs/respond.js"));

            bundles.Add(
              new ScriptBundle("~/scripts/vendor")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/knockout-{version}.debug.js")
                .Include("~/scripts/toastr.js")
                .Include("~/scripts/Q.js")
                .Include("~/scripts/breeze.debug.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/moment.js",
                "~/scripts/jquery-ui-1.10.4.min.js",
                "~/Scripts/colorbox/jquery.colorbox-min.js",
                "~/Scripts/bxslider-4-master/plugins/jquery.easing.1.3.js",
                   "~/Scripts/bxslider-4-master/jquery.bxslider.min.js",
                "~/Scripts/colorbox/startLightbox.js")
              );
            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-responsive.css",
                "~/Scripts/bxslider-4-master/jquery.bxslider.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css",
                 "~/Content/themes/base/jquery.ui.css",
                "~/Content/colorbox.css")
                .Include("~/Content/Site.css")
              );


           
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");

            //ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}