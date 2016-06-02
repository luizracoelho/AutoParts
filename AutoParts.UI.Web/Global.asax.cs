using AutoParts.UI.Web.App_Start;
using AutoParts.UI.Web.Globalization;
using AutoParts.UI.Web.Mappers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutoParts.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}
