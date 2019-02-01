/**
 * Извлечение набора узлов с применением синтаксиса XPath
 */

using System;
using System.Text;
using System.Xml;

namespace SelectNodesByXPathSample
{
   internal class Program
   {
      private static readonly XmlDocument Document;

      static Program()
      {
         Document = new XmlDocument();
         Document.Load("books.xml");
      }

      private static void Main()
      {
         // Извлечение только необходимых узлов
         XmlNodeList nodeList = Document.SelectNodes("/bookstore/book/title");

         var buffer = new StringBuilder();

         // Проход по XmlNodeList.
         if (nodeList != null)
         {
            foreach (XmlNode node in nodeList)
            {
               buffer.AppendLine(node.OuterXml);
            }
         }

         Console.WriteLine(buffer.ToString());
      }
   }
}