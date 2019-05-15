using Ninject.Extensions.Factory;
using System.Linq;
using System.Reflection;

namespace Telecom.Business
{
   public class NameAttributeInstanceProvider : StandardInstanceProvider
   {
      protected override string GetName(MethodInfo methodInfo, object[] arguments)
      {
         var nameAttribute = methodInfo
            .GetCustomAttributes(typeof(BindingNameAttribute), true)
            .FirstOrDefault() as BindingNameAttribute;
         if (nameAttribute != null)
         {
            return nameAttribute.Name;
         }

         return base.GetName(methodInfo, arguments);
      }
   }
}