using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBrokerDemo
{
   /// <summary>
   ///    Простая реализация брокера событий
   /// </summary>
   public class EventBroker
   {
      private readonly Dictionary<string, List<Delegate>> _subscriptions = new Dictionary<string, List<Delegate>>();

      private Dictionary<string, List<Delegate>> Subscriptions
      {
         get { return _subscriptions; }
      }

      public void Register(string eventId, Delegate method)
      {
         // Ассоциировать обработчик событий с событием eventId
         List<Delegate> delegates;
         if (!Subscriptions.TryGetValue(eventId, out delegates))
         {
            delegates = new List<Delegate>();
            Subscriptions[eventId] = delegates;
         }

         delegates.Add(method);
      }

      public void Unregister(string eventId, Delegate method)
      {
         // Отменить ассоциирование обработчика событий с союытием eventId
         List<Delegate> delegates;
         if (!Subscriptions.TryGetValue(eventId, out delegates))
         {
            return;
         }

         delegates.Remove(method);
         if (delegates.Count == 0)
         {
            Subscriptions.Remove(eventId);
         }
      }

      public void OnEvent(string eventId, params object[] args)
      {
         // Вызвать обработчики событий для данного eventId
         List<Delegate> delegates;
         if (!Subscriptions.TryGetValue(eventId, out delegates))
         {
            return;
         }

         foreach (var @delegate in delegates.Where(del => del.Method != null && del.Target != null))
         {
            @delegate.DynamicInvoke(args);
         }
      }
   }
}