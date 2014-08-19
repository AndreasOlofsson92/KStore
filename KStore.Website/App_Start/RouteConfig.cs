using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KStore.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            System.Web.Routing.RouteTable.Routes.MapRoute(
          name: "HotTowelMvc",
          url: "{controller}/{action}/{id}",
          defaults: new
          {
              controller = "HotTowel",
              action = "Index",
              id = UrlParameter.Optional
          }
      );

            



        }
    }
}