using System;
using System.Linq;
using AcmeCarRental.AopVersion.Data;
using PostSharp.Aspects;

namespace AcmeCarRental.AopVersion.Aspects
{
   [Serializable]
   public class LoggingAspectAttribute : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
      {
         Console.WriteLine("{0}: {1}", args.Method.Name, DateTime.Now);

         foreach (var loggable in args.Arguments.OfType<ILoggable>())
         {
            Console.WriteLine(loggable.LogInformation());
         }
      }

      public override void OnSuccess(MethodExecutionArgs args)
      {
         Console.WriteLine("{0} complete: {1}", args.Method.Name, DateTime.Now);
      }
   }
}