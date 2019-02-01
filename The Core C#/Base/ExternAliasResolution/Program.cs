extern alias W1;
extern alias W2;
using System;
using Widget = W2::Library.Widget;

namespace ExternAliasResolution
{
   internal static class Program
   {
      private static void Main()
      {
         var w1 = new W1::Library.Widget();
         var w2 = new Widget();
         Console.WriteLine(w1.GetHashCode());
         Console.WriteLine(w2.GetHashCode());
      }
   }
}