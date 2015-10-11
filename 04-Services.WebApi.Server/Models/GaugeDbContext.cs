using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Services.WebApi.Server.Models
{
    public class GaugeDbContext : DbContext
    {
        public IDbSet<CircularGaugeOptions> CircularGaugeOptions { get; set; }


    }
}
