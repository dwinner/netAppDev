/**
 * Библиотека акторов для .NET
 */

using System.Threading.Tasks;
using NAct;

namespace NActTest
{
   class Program
   {
      static void Main(string[] args)
      {
         IRndGenerator<int> rndActor =
            ActorWrapper.WrapActor(RndGeneratorFactory.NewSimpleRndGenerator());
         Task<int> nextTask = rndActor.GetNextNumber();
         // int number = await nextTask;
      }
   }
}
