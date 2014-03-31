using System.Web.Mvc;
using System.Web.Routing;

namespace RecipiesWebFormApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.MapRoute(
                "Default",
                // Route name
                "{controller}/{action}/{id}",
                // URL with parameters
                new {controller = "Home", action = "Index", id = ""}
                // Parameter defaults
                );
        }
    }
}