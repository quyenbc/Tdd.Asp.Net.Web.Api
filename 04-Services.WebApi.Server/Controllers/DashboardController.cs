using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using _04_Services.WebApi.Server.Models;

namespace _04_Services.WebApi.Server.Controllers
{
    public class DashboardController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var context = new GaugeDbContext())
            {
                return Ok(context.CircularGaugeOptions
                    .Include(x=>x.Ranges)
                    .Include(x=>x.Scale)
                    .ToList());
            }
        }
    }
}
