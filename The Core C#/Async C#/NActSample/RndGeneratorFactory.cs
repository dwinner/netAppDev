namespace NActSample
{
   public static class RndGeneratorFactory
   {
      public static IRndGenerator<int> NewSimpleRndGenerator() => new RndGeneratorImpl();
   }
}