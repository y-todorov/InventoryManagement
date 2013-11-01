using System;
using System.Diagnostics;
using System.Net;
using System.Timers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc;
using Microsoft.AspNet.SignalR;

namespace InventoryManagementMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }

        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }

        protected void Application_Start()
        {
            if (!SiteMapManager.SiteMaps.ContainsKey("sitemap"))
            {
                SiteMapManager.SiteMaps.Register<XmlSiteMap>("sitemap", sitemap =>
                    sitemap.LoadFrom("~/sitemap.sitemap"));
            }

            AreaRegistration.RegisterAllAreas();

         
            RegisterRoutes(RouteTable.Routes);
           
            RegisterAuth();
            
            // Yordan, test that site is not asleep after 20 mins. of inactivity
            // By the way this works very well :)
            Timer timerRequestPage = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds); // 10 minutes
            timerRequestPage.Elapsed += timer_Elapsed;
            timerRequestPage.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            string res = client.DownloadStringTaskAsync(new Uri("http://inventory.azurewebsites.net/")).Result;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            string exMessage = ex.Message;
            string exStackTrace = ex.StackTrace;
            Exception innerEx = ex.InnerException;
            if (innerEx != null)
            {
                string innerExMessage = innerEx.Message;
                string innerExstackTrace = innerEx.StackTrace;
                Debugger.Break();
            }
            Debugger.Break();
        }
        
    }
}