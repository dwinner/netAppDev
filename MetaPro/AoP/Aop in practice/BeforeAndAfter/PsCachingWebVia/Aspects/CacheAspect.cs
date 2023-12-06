using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PostSharp.Aspects;

namespace PsCachingWebVia.Aspects
{
   [Serializable]
   public class CacheAspect : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
      {
         var key = GetCacheKey(args);
         if (HttpContext.Current.Cache[key] == null)
            return;

         args.ReturnValue = HttpContext.Current.Cache[key];
         args.FlowBehavior = FlowBehavior.Return;
      }

      public override void OnSuccess(MethodExecutionArgs args)
      {
         var key = GetCacheKey(args);
         HttpContext.Current.Cache[key] = args.ReturnValue;
      }

      private static string GetCacheKey(MethodExecutionArgs args)
      {
         var serializedArguments = Serialize(args.Arguments);
         var concatArguments = string.Join("_", serializedArguments);
         concatArguments = $"{args.Method.Name}_{concatArguments}";

         return concatArguments;
      }

      private static string[] Serialize(Arguments arguments)
      {
         var json = new JavaScriptSerializer();
         return arguments.Select(json.Serialize).ToArray();
      }
   }
}