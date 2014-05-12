using System.Web.Mvc;

namespace Web.Areas.Merchant
{
    public class MerchantAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Merchant";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Merchant_default",
                "Merchant/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
