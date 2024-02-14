#nullable disable

using System.Dynamic;
using System.Xml.Linq;

namespace DynamicXAttributes;

internal static class XExtensions
{
   public static dynamic DynamicAttributes(this XElement self) => new XWrapper(self);

   private class XWrapper(XElement e) : DynamicObject
   {
      public override bool TryGetMember(GetMemberBinder binder, out object result)
      {
         result = e.Attribute(binder.Name).Value;
         return true;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value)
      {
         e.SetAttributeValue(binder.Name, value);
         return true;
      }
   }
}