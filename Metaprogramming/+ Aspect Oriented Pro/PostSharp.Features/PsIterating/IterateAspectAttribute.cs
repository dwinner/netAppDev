using System;
using System.Reflection;
using PostSharp.Aspects;

namespace PsIterating
{
   [Serializable]
   public sealed class IterateAspectAttribute : OnMethodBoundaryAspect
   {
      public override void OnYield(MethodExecutionArgs args)
         => Console.WriteLine(MethodBase.GetCurrentMethod().Name);

      public override void OnResume(MethodExecutionArgs args)
         => Console.WriteLine(MethodBase.GetCurrentMethod().Name);

      public override void OnEntry(MethodExecutionArgs args)
         => Console.WriteLine(MethodBase.GetCurrentMethod().Name);

      public override void OnExit(MethodExecutionArgs args)
         => Console.WriteLine(MethodBase.GetCurrentMethod().Name);

      public override void OnSuccess(MethodExecutionArgs args)
         => Console.WriteLine(MethodBase.GetCurrentMethod().Name);
   }
}