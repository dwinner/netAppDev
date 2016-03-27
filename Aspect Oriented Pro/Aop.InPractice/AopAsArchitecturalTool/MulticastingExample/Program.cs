// Множественное применение аспекты

using System;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace MulticastingExample
{
   internal static class Program
   {
      private static void Main()
      {
         var myClass = new MyClass();
         myClass.Method1();
         myClass.Method2();
      }
   }

   [Serializable]
   public class LogAspect : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
         => Console.WriteLine("Aspect was applied to {0}", args.Method.Name);
   }

   [Serializable]
   public class AnotherAspect : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
         => Console.WriteLine("Another aspect was applied to {0}", args.Method.Name);
   }

   [LogAspect(AspectPriority = 2, AttributeTargetElements = MulticastTargets.InstanceConstructor)]
   [AnotherAspect(AspectPriority = 1)]
   public class MyClass
   {
      public MyClass()
      {
      }

      public MyClass(int x)
      {
      }

      public void Method1()
      {
      }

      public void Method2()
      {
         Method3();
      }

      //[LogAspect(AttributeExclude = true)]
      public void Method3()
      {
      }
   }
}