/**
 * Простой пример использования XmlDocument
 */

using System;
using System.Text;
using System.Xml;

namespace XmlDocumentSample
{
   internal class Program
   {
      private static readonly XmlDocument Document = new XmlDocument();

      private static void Main()
      {
         Document.Load("books.xml");

         // Извлечение только необходимых узлов
         XmlNodeList titleNodeList = Document.GetElementsByTagName("title");

         // Проход по XmlNodeList
         var buffer = new StringBuilder();
         foreach (XmlNode node in titleNodeList)
         {
            buffer.AppendLine(node.OuterXml);
         }

         Console.WriteLine(buffer.ToString());
      }
   }
}