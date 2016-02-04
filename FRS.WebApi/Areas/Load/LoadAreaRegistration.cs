using System.Web.Mvc;

namespace FRS.WebApi.Areas.Load
{
    public class LoadAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Load";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Load_default",
                "Load/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
            );
        }
    }
}