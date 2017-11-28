using System.Web;
using System.Web.Optimization;

namespace Talisman.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.scrollTo.min.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/MainScript.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                        "~/Scripts/slidernv.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                        "~/Scripts/posPlugin2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));



            bundles.Add(new StyleBundle("~/Content/Main/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/SiteMain.css"));

            bundles.Add(new StyleBundle("~/Content/Main/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/SiteMain.css",
                      "~/Content/CallBacknv.css",
                      "~/Content/Loginnv.css"));

            bundles.Add(new StyleBundle("~/Content/Articles/css").Include(
                      "~/Content/Articlesnv.css"));

            bundles.Add(new StyleBundle("~/Content/FB/css").Include(
                      "~/Content/Feedbacknv.css"));

            bundles.Add(new StyleBundle("~/Content/Service/css").Include(
                      "~/Content/Servicesnv.css"));

            bundles.Add(new StyleBundle("~/Content/Contacts/css").Include(
                      "~/Content/contactsnv.css"));

            bundles.Add(new StyleBundle("~/Content/Plugin/css").Include(
                      "~/Content/pluginNew.css",
                      "~/Content/Position.css"));  
        }
    }
}