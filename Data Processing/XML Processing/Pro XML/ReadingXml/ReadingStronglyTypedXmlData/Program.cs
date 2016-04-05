/**
 * Чтение строготипизированных данных
 */

using System;
using System.Text;
using System.Xml;

namespace ReadingStronglyTypedXmlData
{
   internal class Program
   {
      private static void Main()
      {
         var builder = new StringBuilder();

         decimal totalPrice = 0M;
         const decimal percent = .25M;

         XmlReader xmlReader = XmlReader.Create("books.xml");
         while (xmlReader.Read())
         {
            switch (xmlReader.NodeType)
            {
               case XmlNodeType.Element:
                  switch (xmlReader.Name)
                  {
                     case "price":
                        decimal price = xmlReader.ReadElementContentAsDecimal();
                        totalPrice += price * percent;
                        break;
                     case "title":
                        builder.AppendLine(xmlReader.ReadElementContentAsString());
                        break;
                  }
                  break;
            }
         }

         Console.WriteLine(builder.ToString());
         Console.WriteLine("Total price = {0}", totalPrice);
      }
   }
}