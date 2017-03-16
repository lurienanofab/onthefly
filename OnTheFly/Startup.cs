using LNF.Impl;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(OnTheFly.Startup))]

namespace OnTheFly
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // data
            app.UseDataAccess();

            // mvc
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // webapi
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}