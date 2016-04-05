// Связывание с делегатом после привязки к членам объекта

using System;
using System.Diagnostics;
using Lib;
using static System.Console;
using static System.Delegate;
using static Lib.SomeType;

namespace BindToMemberCreateDelegateToMemberThenInvokeTheMember
{
   internal static class Program
   {
      private static void Main()
      {
         var t = typeof(SomeType);

         // Создание экземпляра (нельзя создать делегата в конструкторе)
         object[] args = { 12 }; // Аргументы конструктора
         WriteLine("x before constructor called: {0}", args[0]);
         var obj = Activator.CreateInstance(t, args);
         WriteLine("Type: {0}", obj.GetType());
         WriteLine("x after ctor returns: {0}", args[0]);

         // NOTE: Вы не можете создать делегата в поле
         // Вызов метода
         var mi = obj.GetType().GetMethod(nameof(SomeType.ToString), DefaultCallingFlags);
         var toString = (Func<string>)CreateDelegate(typeof(Func<string>), obj, mi);
         var s = toString();
         WriteLine("ToString: {0}", s);

         // Чтение и запись свойства
         var pi = obj.GetType().GetProperty(nameof(SomeType.SomeProp), typeof(int));
         var setSomeProp = (Action<int>)CreateDelegate(typeof(Action<int>), obj, pi.GetSetMethod());
         try
         {
            setSomeProp(0);
         }
         catch (ArgumentOutOfRangeException)
         {
            WriteLine("Property set catch.");
         }
         setSomeProp(2);

         var getSomeProp = (Func<int>)CreateDelegate(typeof(Func<int>), obj, pi.GetGetMethod());
         WriteLine("SomeProp: " + getSomeProp());

         // Добавление делегата в событие и удаление делегата из события
         var ei = obj.GetType().GetEvent("SomeEvent", DefaultCallingFlags);
         Debug.Assert(ei != null, "ei != null");
         var addSomeEvent =
            (Action<EventHandler>)CreateDelegate(typeof(Action<EventHandler>), obj, ei.GetAddMethod());
         addSomeEvent(EventCallback);
         var removeSomeEvent =
            (Action<EventHandler>)CreateDelegate(typeof(Action<EventHandler>), obj, ei.GetRemoveMethod());
         removeSomeEvent(EventCallback);
      }

      private static void EventCallback(object sender, EventArgs e)
      {
      }
   }
}