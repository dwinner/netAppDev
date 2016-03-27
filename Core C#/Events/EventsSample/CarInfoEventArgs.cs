using System;

namespace EventsSample
{
   public class CarInfoEventArgs : EventArgs
   {
      public string Car { get; private set; }

      public CarInfoEventArgs(string car)
      {
         Car = car;
      }
   }
}