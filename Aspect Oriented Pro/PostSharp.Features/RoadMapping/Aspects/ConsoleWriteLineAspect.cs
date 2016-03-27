using System;
using System.Reflection;
using PostSharp.Aspects;

namespace MethodBoundarySample.Aspects
{
   [Serializable]
   public sealed class ConsoleWriteLineAspect : OnMethodBoundaryAspect
   {
      private string _fullMethodName;

      public override bool CompileTimeValidate(MethodBase method) => !method.IsStatic;

      public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
      {
         if (method.DeclaringType != null)
         {
            _fullMethodName = $"{method.DeclaringType.Name}.{method.Name}";
         }
      }

      public override void OnEntry(MethodExecutionArgs args)
      {
         if (args.Method.DeclaringType != null)
         {
            Console.WriteLine("{0}.{1} OnEntry", args.Method.DeclaringType.Name, _fullMethodName);
         }
      }

      public override void OnSuccess(MethodExecutionArgs args)
      {
         if (args.Method.DeclaringType != null)
         {
            Console.WriteLine("{0}.{1} OnSuccess returns {2}", args.Method.DeclaringType.Name, args.Method.Name,
               args.ReturnValue);
         }
      }

      public override void OnExit(MethodExecutionArgs args)
      {
         if (args.Method.DeclaringType != null)
         {
            Console.WriteLine("{0}.{1} OnExit", args.Method.DeclaringType.Name, args.Method.Name);
         }
      }

      public override void RuntimeInitialize(MethodBase method)
      {
         if (method.Name.StartsWith("s"))
         {
            Console.ForegroundColor=ConsoleColor.DarkGreen;
         }
      }

      /*public override void OnException(MethodExecutionArgs args)
      {
         base.OnException(args);
      }*/
   }
}