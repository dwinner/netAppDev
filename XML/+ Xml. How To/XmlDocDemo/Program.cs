/**
 * Построение XML-документа с помощью DOM
 */

using System;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlDocDemo
{
   class Program
   {
      static void Main()
      {
         var xmlDocument = new XmlDocument();
         XmlElement bookElement = xmlDocument.CreateElement("Book");
         bookElement.SetAttribute("PublishYear", "2009");
         XmlElement titleElement = xmlDocument.CreateElement("Title");
         titleElement.InnerText = "Programming, art or engineering?";
         XmlElement authorElement = xmlDocument.CreateElement("Author");
         authorElement.InnerText = "Billy Bob";
         bookElement.AppendChild(titleElement);
         bookElement.AppendChild(authorElement);
         xmlDocument.AppendChild(bookElement);

         var stringBuilder = new StringBuilder();

         // Можно с тем же успехом выводить в файл или другой поток данных
         using (var stringWriter = new StringWriter(stringBuilder))
         using (var xmlTextWriter = new XmlTextWriter(stringWriter))
         {
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDocument.WriteContentTo(xmlTextWriter);
            Console.WriteLine(stringBuilder.ToString());
         }

         Console.ReadKey();
      }
   }
}
