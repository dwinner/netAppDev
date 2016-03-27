using System;
using System.Reflection;
using PostSharp.Aspects;

namespace MethodBoundarySample.Aspects
{
   [Serializable]
   public sealed class ApplicationExceptionHandlerAspect : OnExceptionAspect
   {
      public override void OnException(MethodExecutionArgs args)
         => Console.WriteLine("Excpetion type {0} raised {1}{1}{2}",
            args.Exception.GetType().Name,
            Environment.NewLine,
            args.Exception.StackTrace);

      public override Type GetExceptionType(MethodBase targetMethod) => typeof (ApplicationException);
   }
}