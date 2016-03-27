/**
 * Вставка новых узлов через XPath
 */

using System.Xml;
using System.Xml.XPath;

namespace InsertingNodes
{
   internal static class Program
   {
      private const string XmlFile = "books.xml";

      private static void Main()
      {
         var document = new XmlDocument();
         document.Load(XmlFile);
         XPathNavigator navigator = document.CreateNavigator();
         if (navigator.CanEdit)
         {
            XPathNodeIterator iterator = navigator.Select("/bookstore/book/price");
            while (iterator.MoveNext())
            {
               iterator.Current.InsertAfter("<disc>5</disc>");
            }
         }

         document.Save("newbook.xml");
      }
   }
}