﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoogleMapsSample
{
   public class Global : HttpApplication
   {
      void Application_Start(object sender, EventArgs e)
      {
         // Code that runs on application startup
         AreaRegistration.RegisterAllAreas();
         GlobalConfiguration.Configure(WebApiConfig.Register);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
      }
   }
}