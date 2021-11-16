/**
 * Запрашивание статических XML-документов
 */

using System;
using System.Linq;
using System.Xml.Linq;

namespace QueryingStaticXmlDocs
{
   internal static class Program
   {
      private static void Main()
      {
         var hamletXml = XDocument.Load("hamlet.xml");
         var personaQuery = from people in hamletXml.Descendants("PERSONA")
                            select people.Value;

         var persons = personaQuery as string[] ?? personaQuery.ToArray();
         Console.WriteLine("{0} players found", persons.Count());
         Console.WriteLine();
         foreach (var item in persons)
         {
            Console.WriteLine(item);
         }
      }
   }
}