/**
 * Запись в XML объектом XDocument
 */

using System;
using System.Linq;
using System.Xml.Linq;

namespace WritingByXDoc
{
   internal static class Program
   {
      private static void Main()
      {
         var hamletXml = XDocument.Load("hamlet.xml");

         #region Изменение значения отдельного элемента

         var playElement = hamletXml.Element("PLAY");
         if (playElement != null)
         {
            var personsEl = playElement.Element("PERSONAE");
            if (personsEl != null)
            {
               var personEl = personsEl.Element("PERSONA");
               if (personEl != null)
               {
                  personEl.SetValue("Bill Evjen, king of Denmark");
                  Console.WriteLine(personsEl.Value);
               }
            }
         }

         #endregion

         #region Вставка в документ целых узлов

         var xe = new XElement("PERSONA", "Bill Evjen");
         var playEl = hamletXml.Element("PLAY");
         if (playEl != null)
         {
            var perEl = playEl.Element("PERSONAE");
            if (perEl != null)
               perEl.Add(xe);

            var q = from people in hamletXml.Descendants("PERSONA")
                    select people.Value;

            var notDefferedQuery = q as string[] ?? q.ToArray();
            Console.WriteLine("{0} players found", notDefferedQuery.Count());
            Console.WriteLine();
            foreach (var item in notDefferedQuery)
            {
               Console.WriteLine(item);
            }
         }

         #endregion
      }
   }
}