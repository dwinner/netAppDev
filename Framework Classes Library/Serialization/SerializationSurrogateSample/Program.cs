// Суррогаты сериализации

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace SerializationSurrogateSample
{
   internal static class Program
   {
      private static void Main()
      {
         using (var stream = new MemoryStream())
         {
            // 1. Создание желаемого модуля форматирования
            IFormatter formatter = new SoapFormatter();

            // 2. Создание объекта SurrogateSelector
            var surrogateSelector = new SurrogateSelector();

            // 3. Селектор выбирает наш суррогат для объекта DateTime
            surrogateSelector.AddSurrogate(typeof(DateTime), formatter.Context,
               new UniversalToLocalTimeSerializationSurrogate());

            // NOTE: AddSurrogate можно вызывать более одного раза и зарегистрировать несколько суррогатов

            // 4. Модуль форматирования использует наш селектор
            formatter.SurrogateSelector = surrogateSelector;

            // Создание объекта DateTime с локальным временем машины и его сериализация
            var localTimeBeforeSerialize = DateTime.Now;
            formatter.Serialize(stream, localTimeBeforeSerialize);

            // Поток выводит универсальное время в виде строки, проверяя, что все работает
            stream.Position = 0;
            Console.WriteLine(new StreamReader(stream).ReadToEnd());

            // Десериализация универсального времени и преобразование объекта DateTime в локальное время
            stream.Position = 0;
            var localTimeAfterDeserialize = (DateTime)formatter.Deserialize(stream);

            // Проверка корректности работы
            Console.WriteLine("{0} = {1}", nameof(localTimeBeforeSerialize), localTimeBeforeSerialize);
            Console.WriteLine("{0} = {1}", nameof(localTimeAfterDeserialize), localTimeAfterDeserialize);
         }
      }
   }
}