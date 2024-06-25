using Moq;
using StructureMap;

namespace FatThinAspects.UnitTesting
{
   public static class StructureMapServiceLocator
   {
      public static IContainer DefaultImpl { get; } = new Container(expr =>
      {
         var stub = new Mock<IMyCrossCuttingConcern>();
         expr.For<IMyCrossCuttingConcern>().Use(stub.Object);
      });
   }
}