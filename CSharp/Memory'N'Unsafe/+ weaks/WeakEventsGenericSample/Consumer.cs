using System;
using System.Windows;

namespace Wrox.ProCSharp.Delegates
{
   public class Consumer : IWeakEventListener
   {
      private readonly string _name;

      public Consumer(string name)
      {
         _name = name;
      }

      public void NewCarIsHere(object sender, CarInfoEventArgs e)
      {
         Console.WriteLine("{0}: car {1} is new", _name, e.Car);
      }

      bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
      {
         NewCarIsHere(sender, e as CarInfoEventArgs);
         return true;
      }
   }
}
