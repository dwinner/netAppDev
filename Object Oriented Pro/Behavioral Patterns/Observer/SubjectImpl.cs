using System.Collections.Generic;

namespace Observer
{
   /// <summary>
   /// Реализация субъекта
   /// </summary>
   public class SubjectImpl : ISubject<string>
   {
      public string SubjectState
      {
         get
         {
            return _subjectState;
         }
         set
         {
            if (value != _subjectState)
            {
               _subjectState = value;
               Notify(value);
            }
         }
      }
      private string _subjectState;

      public IList<IObserver<string>> Observers { get; private set; }      

      public SubjectImpl()
      {
         Observers = new List<IObserver<string>>();
      }

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
      {
         foreach (var observer in observers)
         {
            Add(observer);
         }
      }

      public bool Remove(IObserver<string> observer)
      {
         return Observers.Remove(observer);
      }

      public void Notify(string state)
      {
         foreach (var observer in Observers)
         {
            observer.Update(state);
         }
      }
   }
}