using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace PostSharpAspectRoles
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
   [AspectRoleDependency(AspectDependencyAction.Require, StandardRoles.Tracing)]
   public class AuthorizationAttribute : OnMethodBoundaryAspect
   {
   }

   [Serializable]
   [ProvideAspectRole(StandardRoles.Tracing)]
   public class TracingAttribute : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
      {
      }
   }

   public class MyClass
   {
      [Authorization]
      public void MyMethod()
      {
      }
   }
}