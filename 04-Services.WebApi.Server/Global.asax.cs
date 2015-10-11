using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using _04_Services.WebApi.Server.Models;

namespace _04_Services.WebApi.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(corsAttr);

            AreaRegistration.RegisterAllAreas();
            //the original web config
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //GlobalConfiguration.Configure(_04_Services.WebApi.WebApiConfig.Register);

            

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DashboardConfig.Seed();
        }


    }
}
