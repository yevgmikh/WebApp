﻿using System;
using System.Web.Http;

namespace Laba2V19
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "WApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.
                Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}
