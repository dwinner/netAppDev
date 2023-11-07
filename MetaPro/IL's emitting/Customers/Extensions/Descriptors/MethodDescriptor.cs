using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spackle.Extensions;
using Spackle.Reflection.Extensions;

namespace Customers.Extensions.Descriptors
{
   internal sealed class MethodDescriptor : Descriptor
   {
      private MethodDescriptor()
      {         
      }

      internal MethodDescriptor(MethodBase method, Assembly containingAssembly)
      {
         var parts = new List<string>();
         AddLocation(method, parts);
         AddCallingConventions(method, parts);
         AddReturnValue(method, containingAssembly, parts);
         parts.Add($"{method.GetName(containingAssembly)}{GetMethodArgumentInformation(method, containingAssembly)}");
         Value = string.Join(" ", parts.ToArray());
      }

      private static void AddReturnValue(MethodBase method, Assembly containingAssembly, List<string> parts)
      {
         var info = method as MethodInfo;
         var descriptor = info?.ReturnType != null ? new TypeDescriptor(info.ReturnType, containingAssembly).Value : "void";
         parts.Add(descriptor);
      }

      private static void AddLocation(MethodBase method, List<string> parts)
      {
         if (!method.IsStatic)
         {
            parts.Add("instance");
         }
      }

      private static void AddCallingConventions(MethodBase method, List<string> parts)
      {
         var callingConventions = method.GetCallingConventions();
         if (callingConventions.Length > 0)
         {
            parts.Add(callingConventions);
         }
      }

      private static string GetMethodArgumentInformation(MethodBase method, Assembly containingAssembly)
      {
         var information = "(";
         var argumentTypes = method.GetParameterTypes();
         if (argumentTypes.Length > 0)
         {
            information += string.Join(", ",
               argumentTypes
                  .Select(type => new List<string> {new TypeDescriptor(type, containingAssembly).Value})
                  .Select(argumentDescriptor => string.Join(" ", argumentDescriptor.ToArray()))
                  .ToArray());
         }

         information += ")";
         return information;
      }
   }
}