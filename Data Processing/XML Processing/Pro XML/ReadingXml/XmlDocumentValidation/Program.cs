/**
 * Проверка достоверности в XmlReader
 */

using System;
using System.Xml;
using System.Xml.Schema;

namespace XmlDocumentValidation
{
   internal class Program
   {
      private static void Main()
      {
         var settings = new XmlReaderSettings();
         settings.Schemas.Add(null, "books.xsd");
         settings.ValidationType = ValidationType.Schema;
         settings.ValidationEventHandler += OnValidate;
         XmlReader reader = XmlReader.Create("books.xml", settings);
         while (reader.Read())
         {
            if (reader.NodeType == XmlNodeType.Text)
            {
               Console.WriteLine(reader.Value);
            }
         }
      }

      private static void OnValidate(object sender, ValidationEventArgs e)
      {
         Console.WriteLine(e.Message);

         XmlSeverityType severityType = e.Severity;
         switch (severityType)
         {
            case XmlSeverityType.Error:
               Console.WriteLine("Error occured: {0}", e.Exception.Message);
               break;
            case XmlSeverityType.Warning:
               Console.WriteLine("Warning occured: ");
               break;
         }
      }
   }
}