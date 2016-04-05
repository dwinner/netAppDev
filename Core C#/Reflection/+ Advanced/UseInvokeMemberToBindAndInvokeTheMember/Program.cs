// Использование InvokeMember для вызова метода

using System;
using System.Reflection;
using Lib;
using static System.Console;
using static System.Reflection.BindingFlags;
using static Lib.SomeType;

namespace UseInvokeMemberToBindAndInvokeTheMember
{
   internal static class Program
   {
      private static void Main()
      {
         var targetType = typeof(SomeType);

         // Создание экземпляра Type
         object[] args = { 12 }; // Аргументы конструктора
         WriteLine("x before constructor called: {0}", args[0]);
         var targetObject = targetType.InvokeMember(null, DefaultCallingFlags | CreateInstance, null, null, args);
         WriteLine("Type: {0}", targetObject.GetType());
         WriteLine("x after constructor returns: {0}", args[0]);

         // Чтение поля и запись в поле
         targetType.InvokeMember("_someField", DefaultCallingFlags | SetField, null, targetObject,
            new object[] { 5 });
         var v =
            (int)targetType.InvokeMember("_someField", DefaultCallingFlags | GetField, null, targetObject, null);
         WriteLine("someField: {0}", v);

         // Вызов метода
         var s =
            (string)
               targetType.InvokeMember(nameof(SomeType.ToString), DefaultCallingFlags | InvokeMethod, null,
                  targetObject, null);
         WriteLine("toString: {0}", s);

         // Чтение и запись свойства
         try
         {
            targetType.InvokeMember(nameof(SomeType.SomeProp), DefaultCallingFlags | SetProperty, null, targetObject,
               new object[] { 0 });
         }
         catch (TargetInvocationException e)// when (KindOfEx(e))
         {
            if (e.InnerException?.GetType() != typeof (ArgumentOutOfRangeException))
               throw;
            WriteLine("Property set catched");
         }

         targetType.InvokeMember(nameof(SomeType.SomeProp), DefaultCallingFlags | SetProperty, null, targetObject,
            new object[] { 2 });
         v =
            (int)
               targetType.InvokeMember(nameof(SomeType.SomeProp), DefaultCallingFlags | GetProperty, null, targetObject,
                  null);
         WriteLine("SomeProp: {0}", v);

         // Добавление в событие и удаление из события делегата путем вызова соответствующего метода
         EventHandler eventHandler = EventCallback;
         targetType.InvokeMember("add_SomeEvent", DefaultCallingFlags | InvokeMethod, null, targetObject,
            new object[] { eventHandler });
         targetType.InvokeMember("remove_SomeEvent", DefaultCallingFlags | InvokeMethod, null, targetObject,
            new object[] { eventHandler });
      }

      /*private static bool KindOfEx(Exception exception)
      {
         return exception.InnerException?.GetType() != typeof(ArgumentOutOfRangeException);
      }*/

      private static void EventCallback(object sender, EventArgs e)
      {
      }
   }
}