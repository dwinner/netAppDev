/**
 * Простой пример трансформации XML в HTML
 */

using System.Xml.Xsl;

namespace SimpleTransform
{
   internal static class Program
   {
      private static void Main()
      {
         var transform = new XslCompiledTransform();
         transform.Load("books.xsl");
         transform.Transform("books.xml", "out.html");
      }
   }
}