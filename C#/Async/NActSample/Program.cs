using NAct;

namespace NActSample
{
   internal static class Program
   {
      private static void Main()
      {
         var rndActor =
            ActorWrapper.WrapActor(RndGeneratorFactory.NewSimpleRndGenerator());
         /*var nextTask = */rndActor.GetNextNumberAsync();
      }
   }
}