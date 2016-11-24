using System;

namespace Framework
{
   public class EventAggregator<TEvent>
      where TEvent : EventArgs
   {
      private static EventAggregator<TEvent> _eventAggregatorImpl;

      private EventAggregator()
      {
      }

      public static EventAggregator<TEvent> Instance
         => _eventAggregatorImpl ?? (_eventAggregatorImpl = new EventAggregator<TEvent>());

      public event Action<object, TEvent> Event;

      public void Publish(object source, TEvent ev) => Event?.Invoke(source, ev);
   }
}