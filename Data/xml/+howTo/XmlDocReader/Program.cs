/*
 * Чтение XML-файла с помощью DOM
 */

using System;
using System.Xml;

namespace XmlDocReader
{
   internal static class Program
   {
      private const string SourceXml =
         "<Book PublishYear=\"2009\">" +
         "<Title>Programming, art or engineering?</Title>" +
         "<Author>Billy Bob</Author>" +
         "</Book>";

      private static void Main()
      {
         var xmlDocument = new XmlDocument();
         xmlDocument.LoadXml(SourceXml);

         var bookElements = xmlDocument.GetElementsByTagName("Book");
         if (bookElements.Count > 0)
         {
            var bookAttributes = bookElements[0].Attributes;
            if (bookAttributes != null)
               Console.WriteLine("Publish Year: {0}",
                  bookAttributes["PublishYear"].Value);
         }

         var authorElements = xmlDocument.GetElementsByTagName("Author");
         if (authorElements.Count > 0)
         {
            var author = authorElements[0].InnerText;
            Console.WriteLine("Author: {0}", author);
         }

         Console.ReadKey();
      }
   }
}