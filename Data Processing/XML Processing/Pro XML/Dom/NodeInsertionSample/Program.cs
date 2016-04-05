/**
 * Вставка узлов
 */

using System;
using System.Text;
using System.Xml;

namespace NodeInsertionSample
{
   internal class Program
   {
      private static readonly XmlDocument XmlDocument;

      static Program()
      {
         XmlDocument = new XmlDocument();
         XmlDocument.Load("books.xml");
      }

      private static void Main()
      {
         // Создание нового элемента 'book'
         XmlElement newBookElement = XmlDocument.CreateElement("book");

         // Установка некоторых атрибутов
         newBookElement.SetAttribute("genre", "Mystery");
         newBookElement.SetAttribute("publicationdate", "2001");
         newBookElement.SetAttribute("ISBN", "123456789");

         // Создание нового элемента title
         XmlElement titleElement = XmlDocument.CreateElement("title");
         titleElement.InnerText = "Case of the Missing Cookie";
         newBookElement.AppendChild(titleElement);

         // Создание нового элемента author
         XmlElement authorElement = XmlDocument.CreateElement("author");
         newBookElement.AppendChild(authorElement);

         // Создание нового элемента name
         XmlElement nameElement = XmlDocument.CreateElement("name");
         nameElement.InnerText = "Cookie Monster";
         authorElement.AppendChild(nameElement);

         // Создание нового элемента price
         XmlElement priceElement = XmlDocument.CreateElement("price");
         priceElement.InnerText = "9.95";
         newBookElement.AppendChild(priceElement);

         if (XmlDocument.DocumentElement != null)
         {
            // Добавление в текущий документ
            XmlDocument.DocumentElement.AppendChild(newBookElement);

            // Запись документа на диск
            using (var xmlTextWriter = new XmlTextWriter("booksEdit.xml", null) { Formatting = Formatting.Indented })
            {
               XmlDocument.WriteContentTo(xmlTextWriter);
            }
         }

         // Загрузка названий всех книг, в том числе и новой
         var buffer = new StringBuilder();
         XmlNodeList titleNodeList = XmlDocument.GetElementsByTagName("title");

         foreach (XmlNode xmlNode in titleNodeList)
         {
            buffer.AppendLine(xmlNode.InnerText);
         }

         Console.WriteLine(buffer.ToString());
      }
   }
}