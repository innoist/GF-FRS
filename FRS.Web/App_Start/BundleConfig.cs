using System.Web.Optimization;

namespace Cares.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery/files").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval/files").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr/files").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/Bundle/BaseLibs")
            bundles.Add(new ScriptBundle("~/Bundle/BaseLibs/files")
                .Include("~/Scripts/jquery-ui-1.10.4.js")
                .Include("~/Scripts/json2.js")
                .Include("~/Scripts/Plugins/angular.js")
                .Include("~/Scripts/knockout-3.1.0.js")
                .Include("~/Scripts/odometer.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/RichTextEditor/ckeditor.js")
                .Include("~/Scripts/knockout.validation.js")
                .Include("~/Scripts/underscore.js")
                .Include("~/Scripts/underscore-ko-1.6.0.js")
                .Include("~/Scripts/numeral/numeral.min.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/require.js")
                .Include("~/Scripts/amplify.js")
                .Include("~/Scripts/respond.js")
                .Include("~/Scripts/charts.js")
                .Include("~/Scripts/jquery.knob.js")
                .Include("~/Scripts/flot/jquery.flot.js")
                .Include("~/Scripts/flot/jquery.flot.resize.min.js")
                .Include("~/Scripts/flot/jquery.flot.pie.min.js")
                .Include("~/Scripts/flot/jquery.flot.stack.min.js")
                .Include("~/Scripts/flot/jquery.flot.orderBars.js")
                .Include("~/Scripts/flot/jquery.flot.axislabels.js")
                .Include("~/Scripts/flot/jquery.flot.crosshair.min.js")
                .Include("~/Scripts/flot/jquery.flot.categories.min.js")
                .Include("~/Scripts/flot/jquery.flot.animator.js")
                .Include("~/Scripts/jquery-barcode.js")
                .Include("~/Scripts/Calendar/jquery.plugin.js")
                .Include("~/Scripts/Calendar/jquery.calendars.js")
                .Include("~/Scripts/Calendar/jquery.calendars.plus.js")
                .Include("~/Scripts/Calendar/jquery.calendars.picker.js")
                .Include("~/Scripts/Calendar/jquery.calendars.islamic.js")
            );

           bundles.Add(new ScriptBundle("~/Scripts/bootstrap/files")
                .Include("~/Scripts/bootstrap/bootstrap.js")
                 );
           bundles.Add(new StyleBundle("~/Content/css/files")
                       .Include(
                                 "~/Content/CSS/layout.css",
                                 "~/Content/CSS/light.css",
                                 "~/Content/CSS/components.css",
                                 "~/Content/CSS/architecture.css",
                                 "~/Content/toastr.css",
                                 "~/Content/site.css",
                                 "~/Content/odometer-theme-car.css",
                                 "~/Content/Calendar/jquery.calendars.picker.css")

                           );
           bundles.Add(new StyleBundle("~/Content/bootstrap-ltr/files")
            .Include("~/Content/bootstrap.css")
              
                );

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap/rtl/files")
                .Include("~/Scripts/bootstrap/rtl/bootstrap-rtl.js")
                );

            bundles.Add(new StyleBundle("~/Content/bootstrap-rtl/files")
            .Include("~/Content/bootstrap-rtl.css")
                );

            bundles.Add(new StyleBundle("~/Content/themes/base/css/files").Include(
                                        "~/Content/themes/base/jquery.ui.core.css",
                                        "~/Content/themes/base/jquery.ui.resizable.css",
                                        "~/Content/themes/base/jquery.ui.selectable.css",
                                        "~/Content/themes/base/jquery.ui.accordion.css",
                                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                                        "~/Content/themes/base/jquery.ui.button.css",
                                        "~/Content/themes/base/jquery.ui.dialog.css",
                                        "~/Content/themes/base/jquery.ui.slider.css",
                                        "~/Content/themes/base/jquery.ui.tabs.css",
                                        "~/Content/themes/base/jquery.ui.datepicker.css",
                                        "~/Content/themes/base/jquery.ui.progressbar.css",
                                        "~/Content/themes/base/jquery.ui.theme.css"));

        }
    }
}
