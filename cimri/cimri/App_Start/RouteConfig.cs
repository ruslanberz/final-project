﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cimri
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Category",
               url: "{controller}/{action}/{id}/{pageId}/{SpecIds}/{q}/{MinPrice}/{MaxPrice}",
               defaults: new { controller = "Home", action = "Category", id = UrlParameter.Optional, pageId = UrlParameter.Optional, SpecIds = UrlParameter.Optional, q = UrlParameter.Optional, MinPrice = UrlParameter.Optional, MaxPrice = UrlParameter.Optional }
           );

         

        }
    }
}
