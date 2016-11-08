using System.Linq;

namespace PsCodeContracts
{
   public class NameReverser
   {
      public string For([StartsWithBee] string aName) => new string(aName.Reverse().ToArray());
   }
}