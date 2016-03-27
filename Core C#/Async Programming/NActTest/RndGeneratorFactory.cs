
namespace NActTest
{
   public static class RndGeneratorFactory
   {
      public static IRndGenerator<int> NewSimpleRndGenerator()
      {
         return new RndGeneratorImpl();
      }
   }
}