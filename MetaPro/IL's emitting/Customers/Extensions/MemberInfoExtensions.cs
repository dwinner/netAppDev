using System.Reflection;
using Customers.Extensions.Descriptors;

namespace Customers.Extensions
{
   internal static class MemberInfoExtensions
   {
      internal static string GetName(this MemberInfo @this, Assembly containingAssembly)
         =>
            $"{new TypeDescriptor(@this.DeclaringType, containingAssembly, @this.DeclaringType != null && @this.DeclaringType.IsGenericType).Value}::{@this.Name}";
   }
}