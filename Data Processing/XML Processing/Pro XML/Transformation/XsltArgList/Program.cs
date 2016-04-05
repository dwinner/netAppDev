/**
 * Вызов методов объекта во время трансформации
 */

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XsltArgList
{
   internal static class Program
   {
      private static void Main()
      {
         // Новый объект XPathDocument
         var document = new XPathDocument("books.xml");

         // Новый объект XslTransform
         var transform = new XslCompiledTransform();
         transform.Load("booksarg.xsl");

         // Новый метод XmlTextWriter, поскольку мы создаем новый XML-документ
         using (XmlWriter writer = new XmlTextWriter("argSample.xml", null))
         {
            var argBook = new XsltArgumentList();
            var bookUtils = new BookUtils();

            // Уведомление argumentList об объекте BookUtils
            argBook.AddExtensionObject("urn:XslSample", bookUtils);

            // Новый XPathNavigator
            XPathNavigator navigator = document.CreateNavigator();

            // Выполнение трансформации
            transform.Transform(navigator, argBook, writer);
         }
      }
   }
}