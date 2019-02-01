using System.Collections.Generic;
using Castle.Core.Interceptor;
using Moq;
using NUnit.Framework;

namespace CastleDynamicProxy.UnitTesting
{
   [TestFixture]
   public class InterceptionTest
   {
      [Test]
      public void TestIntercept()
      {
         var myInterceptor = new MyInterceptor();

         var invocationMock = new Mock<IInvocation>();
         invocationMock.Setup(invocation => invocation.Method.Name).Returns("MyMethod");
         var inv = invocationMock.Object;

         myInterceptor.Intercept(inv);

         Assert.IsTrue(Log.Messages.Contains($"Before {inv.Method.Name}"));
         Assert.IsTrue(Log.Messages.Contains($"After {inv.Method.Name}"));
      }
   }

   public static class Log
   {
      private static readonly List<string> LogMessages = new List<string>();

      public static List<string> Messages => LogMessages;

      public static void Write(string aMessage)
      {
         LogMessages.Add(aMessage);
      }
   }

   public class MyInterceptor : IInterceptor
   {
      public void Intercept(IInvocation invocation)
      {
         Log.Write($"Before {invocation.Method.Name}");
         invocation.Proceed();
         Log.Write($"After {invocation.Method.Name}");
      }
   }
}