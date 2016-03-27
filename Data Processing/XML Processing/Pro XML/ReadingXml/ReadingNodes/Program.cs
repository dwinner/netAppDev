/**
 * Чтение узлов через XMLReader
 */

using System;
using System.Text;
using System.Xml;

namespace ReadingNodes
{
   internal class Program
   {
      private static void Main()
      {
         var stringBuilder = new StringBuilder();
         var xmlReader = XmlReader.Create("books.xml");
         while (xmlReader.Read())
         {
            if (xmlReader.NodeType == XmlNodeType.Text)
            {
               stringBuilder.AppendFormat("{0}{1}", xmlReader.Value, Environment.NewLine);
            }
         }

         Console.WriteLine(stringBuilder.ToString());
      }
   }
}