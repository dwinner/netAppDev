using System;
using System.Collections.Generic;

namespace Observer
{
   /// <summary>
   ///    Subject impl
   /// </summary>
   public class SubjectImpl : ISubject<string>
   {
      private string _subjectState;

      public SubjectImpl()
      {
         Observers = new List<IObserver<string>>();
      }

      public string SubjectState
      {
         get { return _subjectState; }
         set
         {
            if (value != _subjectState)
            {
               _subjectState = value;
               Notify(value);
            }
         }
      }

      public List<IObserver<string>> Observers { get; }

      public bool Add(IObserver<string> observer)
      {
         if (!Observers.Contains(observer))
         {
            Observers.Add(observer);
            return true;
         }

         return false;
      }

      public void Add(params IObserver<string>[] observers)
         => Array.ForEach(observers, observer => Add(observer));

      public bool Remove(IObserver<string> observer)
         => Observers.Remove(observer);

      public void Notify(string state)
         => Observers.ForEach(observer => observer.Update(state));
   }
}