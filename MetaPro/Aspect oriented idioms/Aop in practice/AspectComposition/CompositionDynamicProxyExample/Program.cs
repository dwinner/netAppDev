using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using StructureMap;
using StructureMap.Graph;

namespace CompositionDynamicProxyExample
{
   internal class Program
   {
      private static void Main(string[] args)
      {
      }
   }

   public static class ObjectFactory
   {
      public static IContainer Locator
      {
         get
         {
            return new Container(expr =>
            {
               expr.Scan(scanner =>
               {
                  scanner.TheCallingAssembly();
                  scanner.WithDefaultConventions();
               });
               var proxyHelper = new ProxyHelper();
               /*expr.For<IMyClass>().EnrichAllWith(r =>
                  proxyHelper.Proxify<IMyClass, Aspect1>(
                     proxyHelper.Proxify<IMyClass, Aspect2>(r))
                  );*/
            });
         }
      }
   }

   public interface IMyClass
   {
      void MyMethod();
   }

   public class MyClass : IMyClass
   {
      public void MyMethod()
      {
         Console.WriteLine("My method");
      }
   }

   public class Aspect1 : IInterceptor
   {
      public void Intercept(IInvocation invocation)
      {
         Console.WriteLine("Aspect 2");
         invocation.Proceed();
      }
   }

   public class Aspect2 : IInterceptor
   {
      public void Intercept(IInvocation invocation)
      {
         Console.WriteLine("Aspect 2");
         invocation.Proceed();
      }
   }

   public class ProxyHelper
   {
      private readonly ProxyGenerator _proxyGenerator;

      public ProxyHelper()
      {
         _proxyGenerator = new ProxyGenerator();
      }

      public object Proxify<T, TK>(object obj) where TK : IInterceptor
         =>
            _proxyGenerator.CreateInterfaceProxyWithTargetInterface(typeof(T), obj,
               ObjectFactory.Locator.GetInstance<TK>());
   }
}