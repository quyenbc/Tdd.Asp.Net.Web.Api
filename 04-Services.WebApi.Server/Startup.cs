using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(_04_Services.WebApi.Server.Startup))]

namespace _04_Services.WebApi.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
       //     ConfigureAuth(app);
        }
    }
}
