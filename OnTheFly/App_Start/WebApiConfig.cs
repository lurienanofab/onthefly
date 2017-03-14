using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OnTheFly
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			config.Filters.Add(new OnTheFlyAuthorizeAttribute());

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "ApiInterlock",
				routeTemplate: "api/interlock/{action}",
				defaults: new { controller = "ApiInterlock" }
			);

			config.Routes.MapHttpRoute(
				name: "ApiInterlockDefault",
				routeTemplate: "api/interlock",
				defaults: new { controller = "ApiInterlock", action = "GetState" }
			);

			config.Routes.MapHttpRoute(
				name: "ApiClientCode",
				routeTemplate: "api/clientcode/getlatestcode",
				defaults: new { controller = "ApiClientCode", action = "GetLatestCode" }
			);

			config.Routes.MapHttpRoute(
				name: "ApiCardSwipe",
				routeTemplate: "api/reservation/cardswipe",
				defaults: new { controller = "ApiReservation", action = "CardSwipe" }
			);

			//config.Routes.MapHttpRoute(
			//	name: "ApiReservationStart",
			//	routeTemplate: "api/reservation/start",
			//	defaults: new { controller = "ApiReservation", action = "Start" }
			//);

			//config.Routes.MapHttpRoute(
			//	name: "ApiReservationStop",
			//	routeTemplate: "api/reservation/stop",
			//	defaults: new { controller = "ApiReservation", action = "Stop" }
			//);

			config.Routes.MapHttpRoute(
				name: "ApiReservation",
				routeTemplate: "api/reservation/{action}",
				defaults: new { controller = "ApiReservation" }
			);
		}
	}
}
