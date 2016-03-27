/**
 * Потоковое создание XML-документа
 */

using System;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlTextWriterDemo
{
   class Program
   {
      static void Main()
      {
         var stringBuilder = new StringBuilder();
         using (var stringWriter = new StringWriter(stringBuilder))
         using (var xmlTextWriter = new XmlTextWriter(stringWriter))
         {
            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteStartElement("Book");
            xmlTextWriter.WriteAttributeString("PublishYear", "2009");

            xmlTextWriter.WriteStartElement("Title");
            xmlTextWriter.WriteString("Programming, art or engineering?");
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteStartElement("Author");
            xmlTextWriter.WriteString("Billy Bob");
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteEndElement();
         }

         Console.WriteLine(stringBuilder.ToString());

         Console.ReadKey();
      }
   }
}
