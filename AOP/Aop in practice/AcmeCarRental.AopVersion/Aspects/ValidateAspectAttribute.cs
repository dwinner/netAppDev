using System;
using PostSharp.Aspects;

namespace AcmeCarRental.AopVersion.Aspects
{
   [Serializable]
   public class ValidateAspectAttribute : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
      {
         var parameters = args.Method.GetParameters();
         var arguments = args.Arguments;
         for (var i = 0; i < arguments.Count; i++)
         {
            if (arguments[i] == null)
               throw new ArgumentNullException(parameters[i].Name);

            if (arguments[i] is int && (int)arguments[i] <= 0)
               throw new ArgumentException("", parameters[i].Name);
         }
      }
   }
}