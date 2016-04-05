using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConstructingXmlDocs
{
   public static class Program
   {
      private const string XmlFile = "SimpleInventory.xml";

      static void Main()
      {
         CreateFullXDocument();
         MakeXElementFromArray();
         ParseAndLoadExistingXml();

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }

      #region Построение полного XML-документа без DTD

      private static void CreateFullXDocument()
      {
         XDocument inventoryDocument = // note: Можно использовать XElement
            new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XComment("Current inventory of cars!"),
               new XProcessingInstruction("xml-stylesheet", "href='MyStyles.css' title='Compact', type='text/css'"),
               new XElement("Inventory",
                            new XElement("Car", new XAttribute("Id", "1"),
                                         new XElement("Color", "Green"),
                                         new XElement("Make", "BMW"),
                                         new XElement("PetName", "Stan")
                               ),
                            new XElement("Car", new XAttribute("Id", 2),
                                         new XElement("Color", "Pink"),
                                         new XElement("Make", "Yugo"),
                                         new XElement("PetName", "Melvin")
                               )
                  )
               );
         inventoryDocument.Save(XmlFile); // Сохранить на диск
      }

      #endregion

      #region Генерация документов из массивов и контейнеров

      private static void MakeXElementFromArray()
      {
         var people = new[] // Анонимный массив анонимных типов
            {
               new {FirstName = "Mandy", Age = 32},
               new {FirstName = "Andrew", Age = 40},
               new {FirstName = "Dave", Age = 41},
               new {FirstName = "Sara", Age = 31}
            };
         XElement peopleElement =
            new XElement("People",
                         from c in people
                         select
                            new XElement("Person", new XAttribute("Age", c.Age),
                                         new XElement("FirstName", c.FirstName))
               );
         Console.WriteLine(peopleElement);
      }

      #endregion

      #region Загрузка и разбор XML-содержимого

      private static void ParseAndLoadExistingXml()
      {
         // Построить XElement из строки
         const string myElement = @"<Car Id='3'>
               <Color>Yellow</Color>
               <Make>Yugo</Make>
            </Car>";
         XElement newElement = XElement.Parse(myElement);
         Console.WriteLine(newElement);
         Console.WriteLine();
         if (File.Exists(XmlFile))
         {
            XDocument myDocument = XDocument.Load(XmlFile); // Загрузить XML-документ из файла
            Console.WriteLine(myDocument);
         }
      }

      #endregion

   }
}
