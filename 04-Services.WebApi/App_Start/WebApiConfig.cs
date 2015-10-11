using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace _04_Services.WebApi
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            //configuration.Routes.MapHttpRoute(
            //name: "API with action",
            //routeTemplate: "api/{controller}/{action}",
            //defaults: new
            //{
            //    controller = "Person",
            //    action = RouteParameter.Optional,
            //});
            

            configuration.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Person",
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                }
                //,
                //constraints: new
                //{
                //    id = @"\d+"
                //}
            );

            configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

        }


    }
}
