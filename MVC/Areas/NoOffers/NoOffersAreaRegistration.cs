using System.Web.Mvc;

namespace MVC.Areas.NoOffers
{
    public class NoOffersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NoOffers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NoOffers_default",
                "NoOffers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}