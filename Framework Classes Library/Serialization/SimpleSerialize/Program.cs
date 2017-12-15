using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
// ReSharper disable UnusedMember.Local

namespace SimpleSerialize
{

   #region Типы для сериализации

   [Serializable]
   public class Radio
   {
      public bool HasSubWoofers;
      public bool HasTweeters;

      [NonSerialized]
      public string RadioId = "XF-552RR6"; // Для этого элемента отменим сериализацию

      public double[] StationPresets;
   }

   [Serializable]
   public class Car
   {
      public bool IsHatchBack;
      public readonly Radio TheRadio = new Radio();
   }

   [Serializable]
   [XmlRoot(Namespace = "http://www.MyCompany.com")]
   public class JamesBondCar : Car
   {
      [XmlAttribute]
      public bool CanFly;

      [XmlAttribute]
      public bool CanSubmerge;

      public JamesBondCar(bool skyWorthy, bool seaWorthy)
      {
         CanFly = skyWorthy;
         CanSubmerge = seaWorthy;
      }

      public JamesBondCar()
      {
      } // XML-сериализатору нужен def-конструктор и открытые свойства.
   }

   #endregion

   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with Object Serialization *****\n");

         // Создадим объект JamesBondCar и установим его состояние.
         var jbc = new JamesBondCar
         {
            CanFly = true,
            CanSubmerge = false
         };
         jbc.TheRadio.StationPresets = new[] { 89.3, 105.1, 97.1 };
         jbc.TheRadio.HasTweeters = true;

         // Теперь сохраним его в файл в двоичном формате сериализации
         SaveAsBinaryFormat(jbc, "CarData.dat");
         LoadFromBinaryFile("CarData.dat");

         // Сохраним в формате SOAP.
         SaveAsSoapFormat(jbc, "CarData.soap");

         // Сохраним в формате XML
         SaveAsXmlFormat(jbc, "CarData.xml");

         SaveListOfCars();

         Console.ReadLine();
      }

      #region Load from binary

      private static void LoadFromBinaryFile(string fileName)
      {
         var binFormat = new BinaryFormatter();

         // Читаем JamesBondCar из двоичного файла.
         using (Stream fStream = File.OpenRead(fileName))
         {
            var carFromDisk = (JamesBondCar)binFormat.Deserialize(fStream);
            Console.WriteLine("Can this car fly? : {0}", carFromDisk.CanFly);
         }
      }

      #endregion

      #region Сохраним как двоичный поток

      private static void SaveAsBinaryFormat(object objGraph, string fileName)
      {
         var binFormat = new BinaryFormatter();

         using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
         {
            binFormat.Serialize(fStream, objGraph);
         }
         Console.WriteLine("=> Saved car in binary format!");
      }

      #endregion

      #region Сохраним как SOAP

      private static void SaveAsSoapFormat(object objGraph, string fileName)
      {
         var soapFormat = new SoapFormatter();

         using (Stream fStream = new FileStream(fileName,
            FileMode.Create, FileAccess.Write, FileShare.None))
         {
            soapFormat.Serialize(fStream, objGraph);
         }
         Console.WriteLine("=> Saved car in SOAP format!");
      }

      #endregion

      #region Сохраним как XML

      private static void SaveAsXmlFormat(object objGraph, string fileName)
      {
         var xmlFormat = new XmlSerializer(typeof(JamesBondCar));

         using (Stream fStream = new FileStream(fileName,
            FileMode.Create, FileAccess.Write, FileShare.None))
         {
            xmlFormat.Serialize(fStream, objGraph);
         }
         Console.WriteLine("=> Saved car in XML format!");
      }

      #endregion

      #region Сохранение списка объектов

      private static void SaveListOfCars()
      {
         var myCars = new List<JamesBondCar>
         {
            new JamesBondCar(true, true),
            new JamesBondCar(true, false),
            new JamesBondCar(false, true),
            new JamesBondCar(false, false)
         };

         using (Stream fStream = new FileStream("CarCollection.xml",
            FileMode.Create, FileAccess.Write, FileShare.None))
         {
            var xmlFormat = new XmlSerializer(typeof(List<JamesBondCar>));
            xmlFormat.Serialize(fStream, myCars);
         }
         Console.WriteLine("=> Saved list of cars!");
      }

      private static void SaveListOfCarsAsBinary()
      {
         var myCars = new List<JamesBondCar>();

         var binFormat = new BinaryFormatter();
         using (Stream fStream = new FileStream("AllMyCars.dat",
            FileMode.Create, FileAccess.Write, FileShare.None))
         {
            binFormat.Serialize(fStream, myCars);
         }
         Console.WriteLine("=> Saved list of cars in binary!");
      }

      #endregion
   }
}