// Связывание члена с последующим вызовом

using System;
using System.Reflection;
using Lib;
using static System.Console;
using static System.Diagnostics.Debug;
using static Lib.SomeType;

namespace BindToMemberThenInvokeTheMember
{
   internal static class Program
   {
      private static void Main()
      {
         var type = typeof(SomeType);
         var ctor = type.GetConstructor(new[] { typeof(int).MakeByRefType() });
         object[] args = { 12 }; // аргументы конструктора
         WriteLine("x before constructor called: {0}", args[0]);
         Assert(ctor != null, "ctor != null");
         var targetObject = ctor.Invoke(args);
         WriteLine("Type: {0}", targetObject.GetType());
         WriteLine("x after constructor returns: {0}", args[0]);

         // Чтение поля и запись в поле
         var fieldInfo = targetObject.GetType().GetField("_someField", DefaultCallingFlags);
         Assert(fieldInfo != null, "fieldInfo != null");
         fieldInfo.SetValue(targetObject, 33);
         WriteLine("someField: {0}", fieldInfo.GetValue(targetObject));

         // Вызов метода
         var methodInfo = targetObject.GetType().GetMethod(nameof(SomeType.ToString), DefaultCallingFlags);
         var s = (string)methodInfo.Invoke(targetObject, null);
         Console.WriteLine("ToString: {0}", s);

         // Чтение и запись свойства
         var propertyInfo = targetObject.GetType().GetProperty(nameof(SomeType.SomeProp), typeof(int));
         try
         {
            propertyInfo.SetValue(targetObject, 0, null);
         }
         catch (TargetInvocationException e)
         {
            if (e.InnerException.GetType() != typeof(ArgumentOutOfRangeException)) throw;
            Console.WriteLine("Property set catched");
         }
         propertyInfo.SetValue(targetObject, 2, null);
         WriteLine("SomeProp: {0}", propertyInfo.GetValue(targetObject, null));

         // Добавление делегата в событие и удаление делегата из события
         var eventInfo = targetObject.GetType().GetEvent("SomeEvent", DefaultCallingFlags);
         EventHandler ts = EventCallback;
         Assert(eventInfo != null, "eventInfo != null");
         eventInfo.AddEventHandler(targetObject, ts);
         eventInfo.RemoveEventHandler(targetObject, ts);
      }

      private static void EventCallback(object sender, EventArgs e)
      {
      }
   }
}