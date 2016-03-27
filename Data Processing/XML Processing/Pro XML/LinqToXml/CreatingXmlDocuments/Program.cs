/**
 * Создание XML-документов объектом XElement
 */

using System;
using System.Xml.Linq;

namespace CreatingXmlDocuments
{
   internal static class Program
   {
      private static void Main()
      {
         var document = new XDocument();
         var comment = new XComment("Here is a comment");
         document.Add(comment);

         XNamespace ns1 = "http://www.lipperweb.com/ns/1";
         XNamespace ns2 = "http://www.lipperweb.com/ns/sub";

         var xElement = new XElement(ns1 + "Company", new XAttribute("MyAttribute", "MyAttributeValue"),
            new XElement(ns2 + "CompanyName", "Lipper"),
            new XElement(ns2 + "CompanyAddress",
               new XElement(ns2 + "Address", "123 Main Street"),
               new XElement(ns2 + "City", "St. Louis"),
               new XElement(ns2 + "State", "MO"),
               new XElement(ns2 + "Country", "USA")));

         document.Add(xElement);

         Console.WriteLine(document.ToString());
      }
   }
}