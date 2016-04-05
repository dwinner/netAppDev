// Извещения при отправке объекта в мусор

using System;
using System.Runtime.CompilerServices;

namespace _08_ConditionalWeakTableDemo
{
   internal static class Program
   {
      private static void Main()
      {
         var o = new object().GcWatch($"my object created at {DateTime.Now}");
         GC.Collect(); // Оповещение об отправке в мусор отсутствует
         GC.KeepAlive(o); // Убедимся, что объект, на который ссылается o, существует
         o = null;
         GC.Collect(); // Оповещение об отправке в мусор
      }
   }

   internal static class GcWatcher
   {
      // NOTE: аккуратнее обращайтесь с типами Strings из-за объектов-представителей MarshalByRefObject
      private static readonly ConditionalWeakTable<object, NotifyWhenGcDone<string>> ConditionalWeakTable =
         new ConditionalWeakTable<object, NotifyWhenGcDone<string>>();

      public static T GcWatch<T>(this T anObject, string aTag)
         where T : class
      {
         ConditionalWeakTable.Add(anObject, new NotifyWhenGcDone<string>(aTag));
         return anObject;
      }

      private sealed class NotifyWhenGcDone<T>
      {
         private readonly T _value;

         internal NotifyWhenGcDone(T value)
         {
            _value = value;
         }

         public override string ToString()
         {
            return _value.ToString();
         }

         ~NotifyWhenGcDone()
         {
            Console.WriteLine("GC'd: {0}", _value);
         }
      }
   }
}