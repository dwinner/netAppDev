/**
 * Создание динамических экземпляров объектов
 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace ExpandoObjects
{
   static class Program
   {
      static void Main()
      {
         // Создать объект, представляющий псарню
         dynamic dogKennel = new ExpandoObject();

         // Установить некоторые полезные свойства
         dogKennel.Address = "1234 DogBone Way";
         dogKennel.City = "Fairbanks";
         dogKennel.State = "Alaska";
         dogKennel.Owner = new ExpandoObject();
         dogKennel.Owner.Name = "Ginger";

         // Установить коллекцию динамических объектов собак
         dogKennel.Dogs = new List<dynamic>();

         // Создать некоторые объекты собак
         dynamic thor = new ExpandoObject();
         thor.Name = "Thor";
         thor.Breed = "Siberian Husky";
         dynamic appollo = new ExpandoObject();
         appollo.Name = "Apollo";
         appollo.Breed = "Siberian Husky";

         // Поместить собак на псарню
         dogKennel.Dogs.Add(thor);
         dogKennel.Dogs.Add(appollo);

         PrintExpandoObject(dogKennel, "Ginger's Kennel");

         Console.ReadKey();
      }

      private static void PrintExpandoObject(dynamic dynamicObject, string name, int indent = 0)
      {
         Func<int, string> createPad = n => string.Join(string.Empty, Enumerable.Repeat("    ", n));
         Console.WriteLine("{0}{1}:", createPad(indent), name);
         ++indent;
         var dict = (IDictionary<string, object>)dynamicObject;
         foreach (var property in dict)
         {
            if (property.Value is ExpandoObject)
            {
               // Рекурсия для вывода на консоль вложенного ExpandoObject
               PrintExpandoObject(property.Value, property.Key, indent);
            }
            else
            {
               if (property.Value is IEnumerable<dynamic>)
               {
                  string collName = property.Key;
                  Console.WriteLine("Коллекция {0}{1}:", createPad(indent), collName);
                  int index = 0;
                  foreach (var item in (IEnumerable<dynamic>)property.Value)
                  {
                     string itemName = string.Format("{0}[{1}]", collName, index++);
                     // Рекурсия для данного экземпляра
                     PrintExpandoObject(item, itemName, indent + 1);
                  }
               }
               else
               {
                  Console.WriteLine("{0}{1} = {2}", createPad(indent), property.Key, property.Value);
               }
            }
         }
      }
   }
}
