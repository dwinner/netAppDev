using System;

namespace EventsSample
{
   public class Consumer : INotifier<CarInfoEventArgs>
   {
      public string Name
      {
         get { return _name; }
      }
      private readonly string _name;

      public Consumer(string name)
      {
         _name = name;
      }

      public void Notify(object sender, CarInfoEventArgs eventArgs)
      {
         Console.WriteLine("{0}: car {1} is new", Name, eventArgs.Car);
      }
   }
}