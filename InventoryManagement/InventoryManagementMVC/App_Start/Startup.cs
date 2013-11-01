using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(InventoryManagementMVC.Startup))]
namespace InventoryManagementMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            //HubConfiguration hc = new HubConfiguration()
            //{
            //    EnableDetailedErrors = true,
            //    EnableJavaScriptProxies = true,
            //    EnableJSONP = true,

            //};
            //app.MapSignalR(hc);
        }
    }
}