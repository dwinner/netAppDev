/**
 * Выдача запроса к XML-документу с помощью XPath
 */

using System;
using System.Xml.XPath;

namespace XPathDemo
{
   class Program
   {
      static void Main()
      {
         var doc = new XPathDocument("LesMis.xml");
         XPathNavigator navigator = doc.CreateNavigator();
         XPathNodeIterator iter = navigator.Select("/Book/Chapters/Chapter");
         while (iter.MoveNext())
         {
            Console.WriteLine("Chapter: {0}", iter.Current.Value);
         }
         Console.WriteLine("Found {0} chapters", navigator.Evaluate("count(/Book/Chapters/Chapter)"));
         Console.ReadKey();
      }
   }
}
