/**
 * Проверка корректности XML-документа
 */

using System;
using System.Xml.Schema;
/**
 * Проверка корректности XML-документа
 */

using System.Xml;
using System.IO;

namespace XmlValidationDemo
{
   class Program
   {
      // Этот XML-документ не пройдет проверку на соответствие,
      // потому что элементы Author и Title идут в неправильном порядке        
      const string SOURCE_XML =
          "<?xml version='1.0'?>" +
          "<Book PublishYear=\"2009\">" +
          "<Author>Billy Bob</Author>" +
          "<Title>Programming, art or engineering?</Title>" +
          "</Book>";

      static void Main()
      {
         var schemaSet = new XmlSchemaSet();
         schemaSet.Add(null, "XmlBookSchema.xsd");
         var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = schemaSet };
         settings.ValidationEventHandler += (o, e) => Console.WriteLine("Validation failed: {0}", e.Message);

         using (var reader = new StringReader(SOURCE_XML))
         using (XmlReader xmlReader = XmlReader.Create(reader, settings))
         {
            while (xmlReader.Read())
            {
            }
         }

         Console.WriteLine("Validation complete");
         Console.ReadKey();
      }
   }
}
