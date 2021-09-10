using System.Collections.Generic;

namespace YieldDemo
{
   public class YieldCollection
   {
      public IEnumerator<string> GetEnumerator()
      {
         yield return "Hello";
         yield return "From";
         yield return "Yield";
      }
   }
}