using Moq;
using StructureMap;

namespace PostSharpAspects.UnitTesting
{
   public static class StructureMapServiceLocator
   {
      public static IContainer DefaultImpl { get; } = new Container(expr =>
      {
         var loggingServiceMock = new Mock<ILoggingService>();
         expr.For<ILoggingService>().Use(loggingServiceMock.Object);
      });
   }
}