using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _04_Services.Tdd.WebApi.Tests.Controllers
{
    public class EchoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
