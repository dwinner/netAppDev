/**
 * Извлечение узлов
 */

using System;
using System.Text;
using System.Xml.XPath;

namespace SelectingNodes
{
   internal static class Program
   {
      private const string Books = "books.xml";

      private static void Main()
      {
         var document = new XPathDocument(Books);

         // Создание объекта XPathNavigator
         XPathNavigator navigator = ((IXPathNavigable)document).CreateNavigator();

         // Создание объекта XPathNodeIterator для узлов книг, в атрибуте genre которых содержится значение novel
         var builder = new StringBuilder();
         if (navigator != null)
         {
            XPathNodeIterator nodeIterator = navigator.Select("/bookstore/book[@genre='novel']");
            while (nodeIterator.MoveNext())
            {
               XPathNodeIterator descendants = nodeIterator.Current.SelectDescendants(XPathNodeType.Element, false);
               while (descendants.MoveNext())
               {
                  builder.AppendFormat("{0}: {1}{2}", descendants.Current.Name, descendants.Current.Value,
                     Environment.NewLine);
               }
            }
         }

         Console.WriteLine(builder.ToString());
      }
   }
}