using System;

namespace Adapter.Canonical
{
   public sealed class Adaptee
   {
      public void SpecificRequest()
         => Console.WriteLine("Called Specific Request");
   }
}