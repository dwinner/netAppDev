using System.Threading;
using static System.Console;

namespace Marshalling
{
   /// <summary>
   ///    Класс, не допускающий продвижение
   /// </summary>
   public sealed class NonMarshalableType
   {
      public NonMarshalableType()
      {
         WriteLine($"Executing in {Thread.GetDomain().FriendlyName}");
      }
   }
}