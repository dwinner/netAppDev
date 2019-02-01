using System;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;
using PostSharp.Aspects;

namespace PostSharpAspects.UnitTesting
{
   [TestFixture]
   public class MyLoggingAspectTest
   {
      [Test]
      public void RestrictedAttribute()
      {
         var testType = typeof(MyService);
         var allMethods = testType.GetMethods();
         var restricted = allMethods.Where(info => info.Name == nameof(MyService.DoSomething));
         foreach (
            var aspect in
               restricted.Select(methodInfo => Attribute.GetCustomAttribute(methodInfo, typeof(MyLoggingAspect))))
         {
            Assert.That(aspect, Is.Not.Null);
         }
      }

      [Test]
      public void TestIntercept()
      {
         var args = new MethodExecutionArgs(null, Arguments.Empty);
         var loggingAspect = new MyLoggingAspect();
         loggingAspect.RuntimeInitialize(null);

         loggingAspect.OnEntry(args);
         loggingAspect.OnSuccess(args);

         var loggingServiceMock = new Mock<ILoggingService>();
         loggingServiceMock.Verify(service => service.Write("Log start"));
         loggingServiceMock.Verify(service => service.Write("Log end"));
      }
   }

   public interface IMyService
   {
      void DoSomething();
   }

   public class MyService : IMyService
   {
      private readonly IMyOtherService _otherService;

      public MyService(IMyOtherService otherService)
      {
         _otherService = otherService;
      }

      [MyLoggingAspect]
      public void DoSomething() => Console.WriteLine("Something was done");
   }

   public interface ILoggingService
   {
      void Write(string aMessage);
   }

   public class LoggingService : ILoggingService
   {
      public void Write(string aMessage)
      {
         Console.WriteLine("Logging: {0}", aMessage);
      }
   }

   public interface IMyOtherService
   {
      void DoOtherThing();
   }

   public class MyOtherService : IMyOtherService
   {
      public void DoOtherThing()
      {
      }
   }

   [Serializable]
   public class MyLoggingAspect : OnMethodBoundaryAspect
   {
      [NonSerialized]
      private ILoggingService _loggingService;

      public override void RuntimeInitialize(MethodBase method)
      {
         _loggingService = StructureMapServiceLocator.DefaultImpl.GetInstance<ILoggingService>();
      }

      public override void OnEntry(MethodExecutionArgs args)
      {
         _loggingService.Write("Log start");
      }

      public override void OnSuccess(MethodExecutionArgs args)
      {
         _loggingService.Write("Log end");
      }
   }
}