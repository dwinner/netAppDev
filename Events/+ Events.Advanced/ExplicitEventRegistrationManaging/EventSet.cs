using System;
using System.Collections.Generic;
using System.Threading;

namespace ExplicitEventRegistrationManaging
{
   public sealed class EventSet
   {
      // Закрытый словарь служит для отображения EventKey -> Delegate
      private readonly Dictionary<EventKey, Delegate> _events = new Dictionary<EventKey, Delegate>();
      
      // Добавление отображения EventKey -> Delegate, если его не существует
      // И компоновка делегата с существующим ключом EventKey
      public void Add(EventKey eventKey, Delegate handler)
      {
         Monitor.Enter(_events);
         Delegate d;
         _events.TryGetValue(eventKey, out d);
         _events[eventKey] = Delegate.Combine(d, handler);
         Monitor.Exit(_events);
      }

      // Удаление делегата из EventKey (если он существует)
      // и ликвидация отображения EventKey -> Delegate,
      // когда удален последний делегат
      public void Remove(EventKey eventKey, Delegate handler)
      {
         Monitor.Enter(_events);
         Delegate sourceDel;
         if (_events.TryGetValue(eventKey, out sourceDel))
         {
            sourceDel=Delegate.Remove(sourceDel, handler);
            if (sourceDel != null)            
               _events[eventKey] = sourceDel;            
            else            
               _events.Remove(eventKey);            
         }

         Monitor.Exit(_events);
      }

      // Информирование о событии для обозначенного ключа EventKey
      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public void Raise(EventKey eventKey, object sender, EventArgs e)
      {
         Delegate sourceDelegate;
         Monitor.Enter(_events);
         _events.TryGetValue(eventKey, out sourceDelegate);
         Monitor.Exit(_events);

         if (sourceDelegate != null)
         {
            sourceDelegate.DynamicInvoke(sender, e);
         }
      }
   }
}