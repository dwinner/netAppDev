// Простой пример сериализации

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Demo
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание графа объектов для последующей сериализации
         var objectGraph = new List<string> { "Jeff", "Kristin", "Aidan", "Grant" };
         var stream = SerializeToMemory(objectGraph);

         // Обнуляем все для данного примера
         stream.Position = 0;

         // Десериализация объектов и проверка работоспособности
         objectGraph = (List<string>)DeserializeFromMemory(stream);
         objectGraph.ForEach(Console.WriteLine);
      }

      private static object DeserializeFromMemory(Stream stream)
      {
         // Задание форматирования при сериализации
         var formatter = new BinaryFormatter();

         // Заставляем модуль форматирования десериализовать объекты из потока
         return formatter.Deserialize(stream);
      }

      private static Stream SerializeToMemory(object objectGraph)
      {
         // Конструирование потока, который будет содержать сериализованные объекты
         var stream = new MemoryStream();

         // Задание форматирования при сериализации
         var formatter = new BinaryFormatter();

         // Заставляем модуль форматирования сериализовать объекты в поток
         formatter.Serialize(stream, objectGraph);

         // Возвращение потока сериализованных объектов вызывающему методу
         return stream;
      }
   }
}