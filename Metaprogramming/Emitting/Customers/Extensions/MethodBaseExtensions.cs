using System.Collections.Generic;
using System.Reflection;

namespace Customers.Extensions
{
   public static class MethodBaseExtensions
   {
      internal static string GetCallingConventions(this MethodBase @this)
      {
         var callingConventions = new List<string>();
         if ((@this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
         {
            callingConventions.Add("vararg");
         }

         if ((@this.CallingConvention & CallingConventions.ExplicitThis) == CallingConventions.ExplicitThis)
         {
            callingConventions.Add("explicit");
         }

         return string.Join(" ", callingConventions.ToArray()).Trim();
      }
   }
}