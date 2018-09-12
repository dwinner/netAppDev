// Корректная сериализация одноэлементного объекта

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using static SingletonSerialization.Singleton;
using static System.DateTime;

namespace SingletonSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание массива с несколькими ссылками на один объект Singleton
         Singleton[] ones = { Instance, Instance };
         Console.WriteLine("Do both elements refer to the same object? {0}", (ones[0] == ones[1]));

         using (var stream = new MemoryStream())
         {
            var formatter = new BinaryFormatter();

            // Сериализация и десериализация элементов массива
            formatter.Serialize(stream, ones);
            stream.Position = 0;
            var onesClone = (Singleton[])formatter.Deserialize(stream);

            for (var i = 0; i < ones.Length; i++)
            {
               Debug.Assert(ones[i] == onesClone[i]);
            }
         }
      }
   }

   // Допустим один экземпляр на домен
   [Serializable]
   public sealed class Singleton : ISerializable
   {
      // Единственный экземпляр этого типа
      public DateTime Date = Now;

      // Поля экземпляра
      public string Name = "Jeff";

      private Singleton(SerializationInfo info, StreamingContext context)
      {
      }

      // Закрытый конструктор для создания однокомпонентного типа
      private Singleton()
      {
      }

      public static Singleton Instance { get; } = new Singleton();

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.SetType(typeof(SingletonSerializationHelper));
      }

      [Serializable]
      private sealed class SingletonSerializationHelper : IObjectReference
      {
         public object GetRealObject(StreamingContext context)
         {
            return Instance;
         }
      }
   }
}