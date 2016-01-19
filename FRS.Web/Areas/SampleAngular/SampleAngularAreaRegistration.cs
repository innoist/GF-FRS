using System.Web.Mvc;

namespace FRS.Web.Areas.SampleAngular
{
    public class SampleAngularAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SampleAngular";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SampleAngular_default",
                "SampleAngular/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "FRS.Web.Areas.SampleAngular.Controllers" }
            );
        }
    }
}