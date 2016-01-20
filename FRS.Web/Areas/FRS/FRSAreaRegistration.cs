using System.Web.Mvc;

namespace FRS.Web.Areas.FRS
{
    public class FRSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FRS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FRS_default",
                "FRS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "FRS.Web.Areas.FRS.Controllers" }
            );
        }
    }
}