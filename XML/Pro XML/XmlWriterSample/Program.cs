/**
 * Потоковая запись XML
 */

using System.Xml;

namespace XmlWriterSample
{
   internal static class Program
   {
      private static void Main()
      {
         var settings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true };
         using (var writer = XmlWriter.Create("newbook.xml", settings))
         {
            writer.WriteStartDocument();

            // Начало создания элементов и атрибутов.
            writer.WriteStartElement("book");
            writer.WriteAttributeString("genre", "Mystery");
            writer.WriteAttributeString("publicationdate", "2001");
            writer.WriteAttributeString("ISBN", "123456789");
            writer.WriteElementString("title", "Case of the Missing Cookie");

            writer.WriteStartElement("author");
            writer.WriteElementString("name", "Cookie Monster");
            writer.WriteEndElement();

            writer.WriteElementString("price", "9.99");

            writer.WriteEndElement();
            writer.WriteEndDocument();

            // Выполнение очистки
            writer.Flush();
            writer.Close();
         }
      }
   }
}