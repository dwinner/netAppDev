using System;
using StructureMap;

namespace PsLazyLoading
{
   public static class ObjectFactory
   {
      private static readonly IContainer Container = new Container(expression =>
      {
         expression.For<IMyService>().Use<MyService>();
         expression.For<SlowConstructor>();
      });

      public static object GetInstance(Type aType) => Container.GetInstance(aType);
   }
}