using System;
using AcmeCarRental.AopVersion.Data;
using PostSharp.Aspects;

namespace AcmeCarRental.AopVersion.Aspects
{
   [Serializable]
   public class ExceptionAspectAttribute : OnExceptionAspect
   {
      public override void OnException(MethodExecutionArgs args)
      {
         if (Exceptions.Handle(args.Exception))
            args.FlowBehavior = FlowBehavior.Continue;
      }
   }
}