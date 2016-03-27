/**
 * Потоковое чтение XML-файла
 */

using System;
using System.Xml;
using System.IO;

namespace XmlTextReaderDemo
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
         string publishYear = null, author = null;

         try
         {
            using (var reader = new StringReader(SOURCE_XML))
            using (var xmlReader = new XmlTextReader(reader))
            {
               while (xmlReader.Read())
               {
                  if (xmlReader.NodeType != XmlNodeType.Element)
                     continue;
                  switch (xmlReader.Name)
                  {
                     case "Book":
                        if (xmlReader.MoveToAttribute("PublishYear"))
                           publishYear = xmlReader.Value;
                        break;

                     case "Author":
                        xmlReader.Read();
                        author = xmlReader.Value;
                        break;
                  }
               }
            }
         }
         catch (XmlException xmlEx)
         {
            Console.WriteLine(xmlEx);
         }

         Console.WriteLine("Publish Year: {0}", publishYear);
         Console.WriteLine("Author: {0}", author);

         Console.ReadKey();
      }
   }
}
