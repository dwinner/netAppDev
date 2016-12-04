using System;
using System.Collections.Generic;
using System.Reflection;
using StaticInterceptionViaMonoCecil;

namespace StaticInterceptionTest
{
   public class TestMethodInterceptionAttribute : MethodInterceptionAttribute
   {
      public override void OnEnter(MethodBase method, Dictionary<string, object> parameters)
      {
         Console.WriteLine("Entering method {0}...{1}", method.Name, Environment.NewLine);
         foreach (var paramName in parameters.Keys)
         {
            Console.WriteLine("Parameter {0} has value {1}{2}", paramName, parameters[paramName], Environment.NewLine);
         }
      }
   }
}