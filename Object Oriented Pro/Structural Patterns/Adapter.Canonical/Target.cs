using System;

namespace Adapter.Canonical
{
   public class Target
   {
      public virtual void Request()
         => Console.WriteLine("Called Target Request");
   }
}