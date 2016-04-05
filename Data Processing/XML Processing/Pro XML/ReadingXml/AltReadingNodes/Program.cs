/**
 * Другие методы чтения узлов
 */

using System;
using System.Text;
using System.Xml;

namespace AltReadingNodes
{
   internal class Program
   {
      private static void Main()
      {
         var stringBuilder = new StringBuilder();

         XmlReader xmlReader = XmlReader.Create("books.xml");
         while (!xmlReader.EOF)
         {
            try
            {
               // Если это тип элемента, попытаться загрузить его
               if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "title")
               {
                  stringBuilder.AppendLine(xmlReader.ReadElementString());
               }
               else
               {
                  // В противном случае двигаемся дальше
                  xmlReader.Read();
               }
            }
            catch (XmlException) { }
         }

         Console.WriteLine(stringBuilder.ToString());
      }
   }
}