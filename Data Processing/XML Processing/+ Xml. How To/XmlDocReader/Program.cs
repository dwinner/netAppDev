/**
 * Чтение XML-файла с помощью DOM
 */

using System;
using System.Xml;

namespace XmlDocReader
{
   class Program
   {
      const string SOURCE_XML =
          "<Book PublishYear=\"2009\">" +
          "<Title>Programming, art or engineering?</Title>" +
          "<Author>Billy Bob</Author>" +
          "</Book>";

      static void Main()
      {
         var xmlDocument = new XmlDocument();
         xmlDocument.LoadXml(SOURCE_XML);

         XmlNodeList bookElements = xmlDocument.GetElementsByTagName("Book");
         if (bookElements.Count > 0)
         {
            var bookAttributes = bookElements[0].Attributes;
            if (bookAttributes != null)
            {
               Console.WriteLine("Publish Year: {0}",
                  bookAttributes["PublishYear"].Value);
            }
         }

         XmlNodeList authorElements = xmlDocument.GetElementsByTagName("Author");
         if (authorElements.Count > 0)
         {
            string author = authorElements[0].InnerText;
            Console.WriteLine("Author: {0}", author);
         }

         Console.ReadKey();
      }
   }
}
