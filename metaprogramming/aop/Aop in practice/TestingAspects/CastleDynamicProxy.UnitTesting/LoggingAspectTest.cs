using System;
using Castle.Core.Interceptor;
using Moq;
using NUnit.Framework;

namespace CastleDynamicProxy.UnitTesting
{
   [TestFixture]
   public class LoggingAspectTest
   {
      [Test]
      public void TestIntercept()
      {
         var loggingServiceMock = new Mock<ILoggingService>();
         var loggingAspect = new MyLoggingAspectAttribute(loggingServiceMock.Object);
         var invocationMock = new Mock<IInvocation>();

         loggingAspect.Intercept(invocationMock.Object);

         loggingServiceMock.Verify(service => service.Write("Log start"));
         loggingServiceMock.Verify(service => service.Write("Log end"));
      }
   }

   #region Интерфейсы и реализации

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

   public interface IMyService
   {
      void DoSomething();
   }

   public class MyService : IMyService
   {
      private readonly IMyOtherService _myOtherService;

      public MyService(IMyOtherService myOtherService)
      {
         _myOtherService = myOtherService;
      }

      public void DoSomething()
      {
         Console.WriteLine("Something was done");
      }
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

   #endregion

   #region Аспекты

   public class MyLoggingAspectAttribute : IInterceptor
   {
      private readonly ILoggingService _loggingService;

      public MyLoggingAspectAttribute(ILoggingService loggingService)
      {
         _loggingService = loggingService;
      }

      public void Intercept(IInvocation invocation)
      {
         _loggingService.Write("Log start");
         invocation.Proceed();
         _loggingService.Write("Log end");
      }
   }

   #endregion

   /*
   class Program
    {
        public class ProxyHelper
        {
            readonly ProxyGenerator _proxyGenerator;

            public ProxyHelper()
            {
                _proxyGenerator = new ProxyGenerator();
            }

            public object Proxify<T, K>(object obj) where K: IInterceptor
            {
                var interceptor = (IInterceptor) ObjectFactory.GetInstance<K>();
                var result = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                    typeof (T), obj, interceptor);
                return result;
            }
        }

        static void Main(string[] args)
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Scan(scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                    });
                    var proxyHelper = new ProxyHelper();
                    x.For<IMyService>().Use<MyService>().EnrichWith(proxyHelper.Proxify<IMyService, MyLoggingAspect>);
                });

            var myObj = ObjectFactory.GetInstance<IMyService>();
            myObj.DoSomething();
        }
    }  
   */
}