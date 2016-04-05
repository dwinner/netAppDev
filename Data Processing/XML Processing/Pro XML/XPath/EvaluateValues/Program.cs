/**
 * Вычисление значений через XPath-выражения
 */

using System;
using System.Xml.XPath;

namespace EvaluateValues
{
   internal static class Program
   {
      private const string XmlFile = "books.xml";

      private static void Main()
      {
         var document = new XPathDocument(XmlFile);
         XPathNavigator navigator = ((IXPathNavigable)document).CreateNavigator();

         if (navigator != null)
         {
            object costObj = navigator.Evaluate("sum(/bookstore/book/price)");
            double cost;
            if (costObj != null && double.TryParse(costObj.ToString(), out cost))
            {
               Console.WriteLine("Total cost: {0}", cost);
            }
         }
      }
   }
}