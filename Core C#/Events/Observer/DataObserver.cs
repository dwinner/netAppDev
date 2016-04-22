using System;

namespace Inbox.Observer
{
   /// <summary>
   ///    Класс-получатель уведомлений
   /// </summary>
   public class DataObserver : IObserver<int>
   {
      private readonly string _name = "Observer";

      public DataObserver(string name)
      {
         _name = name;
      }

      public void OnNext(int value)
      {
         Console.WriteLine("{1}: Generated data {0}", value, _name);
      }

      public void OnError(Exception error)
      {
         Console.WriteLine("{0}: Error", _name);
      }

      public void OnCompleted()
      {
         Console.WriteLine("{0}: Completed", _name);
      }
   }
}