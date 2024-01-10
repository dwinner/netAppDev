using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DynamicObjectDemo
{
   public class WroxDynamicObject : DynamicObject
   {
      private readonly Dictionary<string, object> _dynamicData = new Dictionary<string, object>();

      public override bool TryGetMember(GetMemberBinder binder, out object result)
      {
         bool success = false;
         if (_dynamicData.TryGetValue(binder.Name, out var value))
         {
            result = value;
            success = true;
         }
         else
         {
            result = "Property Not Found!";
         }

         return success;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value)
      {
         _dynamicData[binder.Name] = value;
         return true;
      }

      public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
      {
         dynamic method = _dynamicData[binder.Name];
         result = method((DateTime)args[0]);
         return result != null;
      }
   }
}