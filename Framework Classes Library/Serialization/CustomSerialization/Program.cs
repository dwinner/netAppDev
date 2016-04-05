using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace CustomSerialization
{
   #region Типы для сериализации

   [Serializable]
   class StringData : ISerializable
   {
      private readonly string _dataItemOne = "First data block";
      private readonly string _dataItemTwo = "More data";

      public StringData() { }  // Нужен для сериализации

      protected StringData(SerializationInfo si, StreamingContext ctx)
      {   // Конструктор для десериализации            
         _dataItemOne = si.GetString("First_Item").ToLower();
         _dataItemTwo = si.GetString("dataItemTwo").ToLower();
      }

      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext ctx)
      {   // Сериализация            
         info.AddValue("First_Item", _dataItemOne.ToUpper());
         info.AddValue("dataItemTwo", _dataItemTwo.ToUpper());
      }
   }

   [Serializable]
   class MoreData
   {
      private string _dataItemOne = "First data block";
      private string _dataItemTwo = "More data";

      [OnSerializing]
      private void OnSerializing(StreamingContext context)
      {   // Вызывается перед сериализацией объекта            
         _dataItemOne = _dataItemOne.ToUpper();
         _dataItemTwo = _dataItemTwo.ToUpper();
      }

      [OnSerialized]
      private void OnSerialized(StreamingContext context)
      {
         // Вызывается после сериализации объекта
      }

      [OnDeserializing]
      private void OnDeserializing(StreamingContext context)
      {
         // Вызывается во время десериализации объекта
      }

      [OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {   // Вызывается сразу после десериализации объекта            
         _dataItemOne = _dataItemOne.ToLower();
         _dataItemTwo = _dataItemTwo.ToLower();
      }
   }

   #endregion

   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Custom Serialization *****");

         // Этот тип реализует ISerializable.
         var myData = new StringData();

         // Сохранить на локальный файл в формате SOAP
         var soapFormat = new SoapFormatter();
         using (Stream fStream = new FileStream("MyData.soap",
            FileMode.Create,
            FileAccess.Write,
            FileShare.None))
         {
            soapFormat.Serialize(fStream, myData);
         }
         Console.ReadLine();
      }
   }
}
