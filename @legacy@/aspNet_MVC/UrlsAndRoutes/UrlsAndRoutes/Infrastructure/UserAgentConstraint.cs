﻿using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
   /// <summary>
   ///    Ограничение маршрута для UserAgent'а
   /// </summary>
   public class UserAgentConstraint : IRouteConstraint
   {
      private readonly string _requiredUserAgent;

      public UserAgentConstraint(string requiredUserAgent)
      {
         _requiredUserAgent = requiredUserAgent;
      }

      public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
         RouteDirection routeDirection)
      {
         return httpContext.Request.UserAgent != null && httpContext.Request.UserAgent.Contains(_requiredUserAgent);
      }
   }
}