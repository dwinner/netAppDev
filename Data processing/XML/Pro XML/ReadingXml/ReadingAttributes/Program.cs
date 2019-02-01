/**
 * Извлечение данных атрибутов
 */

using System;
using System.Text;
using System.Xml;

namespace ReadingAttributes
{
   internal class Program
   {
      private static void Main()
      {
         var builder = new StringBuilder();
         XmlReader reader = XmlReader.Create("books.xml");

         // Чтение узлов по одному за раз
         while (reader.Read())
         {
            // Выполнение проверки, является ли узел элементом
            switch (reader.NodeType)
            {
               case XmlNodeType.Element:
                  for (int i = 0; i < reader.AttributeCount; i++)
                  {
                     builder.AppendLine(reader.GetAttribute(i));
                  }
                  break;
            }
         }

         Console.WriteLine(builder.ToString());
      }
   }
}