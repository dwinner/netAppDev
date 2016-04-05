using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace LinqToXmlWinApp.Model
{
   /// <summary>
   /// Некоторая утильная логика обработки XML-документов через LINQ
   /// </summary>
   public static class LinqToXmlObjectModel
   {
      public const string DefaultXmlFile = "Inventory.xml";
      
      /// <summary>
      /// Возвращает построенный XDocument
      /// </summary>
      /// <param name="fileOrUri">Файл или URI</param>
      /// <returns>Построенный XDocument</returns>
      public static XDocument GetXmlInventory(string fileOrUri = DefaultXmlFile)
      {
         return File.Exists(fileOrUri) ? XDocument.Load(fileOrUri) : new XDocument("");
      }      

      /// <summary>
      /// Добавление нового узла внутрь элемента Inventory
      /// </summary>
      /// <param name="carEntity">Некий сущностный объект</param>
      /// <param name="fileOrUri">XML-файл с корневым элементом Inventory</param>
      public static void InsertNewElement(CarEntity carEntity, string fileOrUri = DefaultXmlFile)
      {
         XDocument inventoryDocument = XDocument.Load(fileOrUri);
         Random randomNumber = new Random(); // Сгенерировать случайное число для идентификатора
         
         // Создать новый XElement из входных параметров
         XElement newElement = new XElement("Car", new XAttribute("Id", randomNumber.Next(50000)),
            new XElement("Color", carEntity.Color),
            new XElement("Make", carEntity.Make),
            new XElement("PetName", carEntity.PetName)
         );

         inventoryDocument.Descendants("Inventory").First().Add(newElement);  // Добавить в объект в памяти.
         inventoryDocument.Save(fileOrUri);  // Сохранить на диске
      }
      
      /// <summary>
      /// Цвета, используемые определенным изготовителем
      /// </summary>
      /// <param name="make">Строка изготовителя</param>
      /// <param name="fileOrUri">Файл или URL с XML-файлом</param>
      /// <returns>Список цветов</returns>
      public static List<string> LookUpColorsForMake(string make, string fileOrUri = DefaultXmlFile)
      {
         XDocument inventoryDocument = XDocument.Load(fileOrUri); // Загрузить текущий документ

         // Найти цвета заданного изготовителя
         var makeInfo = from car in inventoryDocument.Descendants("Car")
                        let makeElement = car.Element("Make")
                        where makeElement != null && makeElement.Value == make
                        let colorElement = car.Element("Color")
                        where colorElement != null
                        select colorElement.Value;

         // Найти полученные цвета
         return makeInfo.Distinct().Select(color => string.Format("- {0}\n", color)).ToList();
      }
   }

   public class CarEntity
   {
      public int Id { get; set; }
      public string Make { get; set; }
      public string Color { get; set; }
      public string PetName { get; set; }
   }
}
