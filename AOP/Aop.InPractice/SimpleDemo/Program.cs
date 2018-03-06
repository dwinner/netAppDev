using System;
using PostSharp.Aspects;

namespace SimpleDemo
{
   internal class Program
   {
      private static void Main()
      {
         var myClass = new MyClass();
         myClass.MyMethod();
      }
   }

   public class MyClass
   {
      [MyAspect]
      public void MyMethod()
      {
         Console.WriteLine("Hello, AOP!");
      }
   }

   [Serializable]
   public class MyAspectAttribute : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args) => Console.WriteLine("Before the method");

      public override void OnExit(MethodExecutionArgs args) => Console.WriteLine("After the method");
   }
}