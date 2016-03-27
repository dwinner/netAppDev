using System;
using System.Reflection;
using PostSharp.Aspects;

namespace CompileTimeInitializeExample
{
   internal static class Program
   {
      private static void Main()
      {
         var myClass = new MyClass();
         myClass.MyMethod();
      }
   }

   [Serializable]
   public class MyLoggingAspect : OnMethodBoundaryAspect
   {
      private string _methodName;

      public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
      {
         _methodName = method.Name;
      }

      public override void OnEntry(MethodExecutionArgs args) => Console.WriteLine("Method was called: {0}", _methodName);
   }

   public class MyClass
   {
      [MyLoggingAspect]
      public void MyMethod() => Console.WriteLine("Code in mymethod");
   }
}