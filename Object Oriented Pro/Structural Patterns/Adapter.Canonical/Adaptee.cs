using System;

namespace Adapter.Canonical
{
   public class Adaptee
   {
      public void SpecificRequest()
      {
         Console.WriteLine("Called Specific Request");
      }
   }
}