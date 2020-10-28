using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DogMatchMaker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleTable.EnableOptimizations = false;    //Allows google fonts to work
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();                           // <----- Add this line
        }
    }
}
