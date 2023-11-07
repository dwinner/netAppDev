using System;
using System.Collections.Generic;
using System.Reflection;

namespace StaticInterceptionViaMonoCecil
{
   [AttributeUsage(AttributeTargets.Method)]
   public class MethodInterceptionAttribute : Attribute
   {
      public virtual void OnEnter(MethodBase method, Dictionary<string, object> parameters)
      {
      }

      public virtual void OnExit()
      {         
      }
   }
}