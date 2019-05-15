using System;
using System.Configuration;
using Ninject.Activation;

namespace DataMigration.Business
{
   public class ConfigurationProvider : Provider<string>
   {
      protected override string CreateInstance(IContext context)
      {
         if (context.Request.Target == null)
         {
            throw new Exception("Target required.");
         }

         var paramName = context.Request.Target.Name;
         var value = ConfigurationManager.AppSettings[paramName];
         if (string.IsNullOrEmpty(value))
         {
            value = ConfigurationManager.ConnectionStrings[paramName].ConnectionString;
         }

         return value;
      }
   }
}