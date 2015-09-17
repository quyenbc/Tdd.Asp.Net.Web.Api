using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace _04_Services.WebApi
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    controller = "Person",
                    id = RouteParameter.Optional
                });

            configuration.Formatters.XmlFormatter.UseXmlSerializer = true;



            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            

            //configuration.Services.Replace(
            //    typeof (IHttpControllerActivator),
            //    new CompositionRoot());
        }


    }
}
