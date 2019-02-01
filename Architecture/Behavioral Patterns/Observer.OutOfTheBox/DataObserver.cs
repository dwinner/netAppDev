using System;
using static System.Console;

namespace Observer.OutOfTheBox
{
   public class DataObserver : IObserver<int>
   {
      private readonly string _name;

      public DataObserver(string observerName)
      {
         _name = observerName;
      }

      public void OnCompleted()
         => WriteLine("{0} :Completed", _name);

      public void OnError(Exception error)
         => WriteLine("{0} : Error", _name);

      public void OnNext(int value)
         => WriteLine("{1} :Generated data {0}", value, _name);
   }
}