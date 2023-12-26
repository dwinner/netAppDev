using System;

namespace EventsSample
{
   public class CarInfoEventArgs : EventArgs
   {
      public CarInfoEventArgs(string car) => Car = car;

      public string Car { get; }
   }
}