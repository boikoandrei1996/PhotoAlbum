using System.Web;
using System.Web.Optimization;

namespace WebApplicationPL
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax")
                .Include("~/Scripts/jquery.unobtrusive-ajax*")
                .Include("~/Content/Scripts/like_action.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js",
                         "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/scripts/common")
                .Include("~/Content/Scripts/common.js"));

            bundles.Add(new StyleBundle("~/styles/common")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/Styles/common.css"));
            
            bundles.Add(new ScriptBundle("~/scripts/uploadImageInput")
                .Include("~/Content/Scripts/upload_image_input.js"));

            bundles.Add(new StyleBundle("~/styles/uploadImageInput")
                .Include("~/Content/Styles/upload_image_input.css"));
        }
    }
}
