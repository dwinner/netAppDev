﻿/*
 * Запрос к XML-документу
 */

using System;
using System.Linq;
using System.Xml.Linq;

namespace LinqToXml
{
   internal class Program
   {
      private static void Main()
      {
         // Note: простой запрос к XML-документу
         var doc = XDocument.Load("LesMis.xml");
         var chaptersWithHe =
            from chapter in doc.Descendants("Chapter")
            where chapter.Value.Contains(" he ")
            select chapter.Value;

         Console.WriteLine("Chapters with 'he':");
         foreach (var title in chaptersWithHe)
         {
            Console.WriteLine(title);
         }

         Console.WriteLine();

         // Note: Создание XML-фрагмента
         var xml = new XElement("Books",
            new XElement("Book",
               new XAttribute("year", 1856),
               new XElement("Title", "Les Contemplations")),
            new XElement("Book",
               new XAttribute("year", 1843),
               new XElement("Title", "Les Burgraves"))
         );

         Console.WriteLine(xml);
         Console.WriteLine();

         Console.ReadKey();
      }
   }
}