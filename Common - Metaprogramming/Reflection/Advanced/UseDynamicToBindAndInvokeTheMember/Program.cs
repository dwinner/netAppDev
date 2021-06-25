// Использование динамических типов для отражения

using System;
using Lib;
using Microsoft.CSharp.RuntimeBinder;
using static System.Activator;
using static System.Console;

namespace UseDynamicToBindAndInvokeTheMember
{
   internal static class Program
   {
      private static void Main()
      {
         var targetType = typeof(SomeType);

         // Создание экземпляра (нельзя создать делегата в конструкторе)
         object[] args = { 12 }; // Аргументы конструктора
         WriteLine("x before ctor called: {0}", args[0]);
         dynamic targetObject = CreateInstance(targetType, args);
         Console.WriteLine("Type: " + targetObject.GetType().ToString());
         WriteLine("x after ctor returns: {0}", args[0]);

         // Чтение и запись в поле
         try
         {
            targetObject._someField = 5;
            var v = (int)targetObject._someField;
            WriteLine("_someField: {0}", v);
         }
         catch (RuntimeBinderException e)
         {
            // Мы попадем сюда, если поле закрыто
            WriteLine("Failed to access field: {0}", e.Message);
         }

         // Вызов метода
         var s = (string)targetObject.ToString();
         WriteLine("ToString: {0}", s);

         // Чтение и запись свойства
         try
         {
            targetObject.SomeProp = 0;
         }
         catch (ArgumentOutOfRangeException)
         {
            WriteLine("Property set catch.");
         }

         targetObject.SomeProp = 2;
         var val = (int)targetObject.SomeProp;
         WriteLine("SomeProp: {0}", val);

         // Добавление делегата в событие и удаление делегата из события
         targetObject.SomeEvent += new EventHandler(EventCallback);
         targetObject.SomeEvent -= new EventHandler(EventCallback);
      }

      private static void EventCallback(object sender, EventArgs e)
      {
      }
   }
}