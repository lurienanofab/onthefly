using LNF;
using LNF.Impl.DependencyInjection.Web;
using LNF.Web;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnTheFly
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServiceProvider.Current = IOC.Resolver.GetInstance<ServiceProvider>();

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