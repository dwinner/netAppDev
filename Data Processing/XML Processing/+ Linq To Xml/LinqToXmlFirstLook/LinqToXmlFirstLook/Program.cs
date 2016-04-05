using System;
using System.Xml;
using System.Xml.Linq;

namespace LinqToXmlFirstLook
{
   public static class Program
   {
      private const string XmlFile = "Inventory.xml";
      private const string LinqXmlFile = "LinqInventory.xml";

      static void Main()
      {
         BuildXmlDocWithDom();
         BuildXmlDocWithLinqToXml();
         DeleteNodeFromDoc();
         Console.ReadKey();
      }

      private static void BuildXmlDocWithDom()  // Построение XML-документа с помощью DOM
      {
         XmlDocument document = new XmlDocument(); // Создать новый документ XML в памяти.
         XmlElement inventoryRootElement = document.CreateElement("Inventory");  // Заполнить документ корневым элементом по имени Inventory
         XmlElement carElement = document.CreateElement("Car");   // Создать подэлемент по имени Car с атрибутом Id
         carElement.SetAttribute("Id", "1000");
         XmlElement nameElement = document.CreateElement("PetName"); // Построить данные внутри элемента Car
         nameElement.InnerText = "Jimbo";
         XmlElement colorElement = document.CreateElement("Color");
         colorElement.InnerText = "Red";
         XmlElement makeElement = document.CreateElement("Make");
         makeElement.InnerText = "Ford";
         carElement.AppendChild(nameElement);   // Добавить PetName, Color, Make в элемент Car
         carElement.AppendChild(colorElement);
         carElement.AppendChild(makeElement);
         inventoryRootElement.AppendChild(carElement);   // Добавить элемент Car к элементу Inventory
         document.AppendChild(inventoryRootElement);
         document.Save(XmlFile);
      }

      private static void BuildXmlDocWithLinqToXml()  // Построение XML-документа с помощью LINQ to XML
      {
         XElement document =  // Создание XML-документа в более 'функциональной' манере
            new XElement("Inventory",
               new XElement("Car", new XAttribute("Id", "1000"),
                  new XElement("PetName", "Jimbo"),
                  new XElement("Color", "Red"),
                  new XElement("Make", "Ford")
               )
            );
         document.Save(LinqXmlFile);
      }

      private static void DeleteNodeFromDoc()   // Осевые методы
      {
         XElement document =
            new XElement("Inventory",
               new XElement("Car", new XAttribute("Id", "1000"),
                  new XElement("PetName", "Jimbo"),
                  new XElement("Color", "Red"),
                  new XElement("Make", "Ford")
               )
            );
         document.Descendants("PetName").Remove(); // Удалить элемент PetName из дерева.
         Console.WriteLine(document);
      }
   }
}
