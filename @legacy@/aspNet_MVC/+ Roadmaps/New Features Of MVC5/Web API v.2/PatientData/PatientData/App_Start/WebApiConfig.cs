﻿using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace PatientData
{
   public static class WebApiConfig
   {
      public static void Register(HttpConfiguration config)
      {
         // Web API configuration and services
         // Configure Web API to use only bearer token authentication.
         config.SuppressDefaultHostAuthentication();
         config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

         // var cors = new EnableCorsAttribute("*", "*", "GET");
         config.EnableCors();

         // Web API routes
         config.MapHttpAttributeRoutes();

         config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
            );
      }
   }
}