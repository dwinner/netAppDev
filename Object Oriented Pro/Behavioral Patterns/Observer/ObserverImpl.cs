using System;

namespace Observer
{
   /// <summary>
   ///    Observer impl
   /// </summary>
   public class ObserverImpl : IObserver<string>
   {
      private readonly string _gap;
      private readonly string _name;
      private string _state;

      public ObserverImpl(ISubject<string> subject, string name, string gap)
      {
         _name = name;
         _gap = gap;
         subject.Add(this);
      }

      public void Update(string subjectState)
      {
         _state = subjectState;
         Console.WriteLine("{0}{1}: {2}", _gap, _name, _state);
      }
   }
}