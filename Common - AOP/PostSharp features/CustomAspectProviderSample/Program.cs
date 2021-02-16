// Собственный поставщик аспектов

namespace CustomAspectProviderSample
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   /*/public class ProviderAspect : IAspectProvider
   {
      public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
      {
         var type = (Type) targetElement;
         return type.GetMethods().Select(info => new AspectInstance[] {new AspectInstance() };)
      }
   }    */
}