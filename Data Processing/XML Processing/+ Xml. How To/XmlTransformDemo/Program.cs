/**
 * Преобразование XML-документа в HTML-документ
 */

using System.Xml.Xsl;
using System.Diagnostics;

namespace XmlTransformDemo
{
   class Program
   {
      static void Main()
      {
         var transform = new XslCompiledTransform();
         transform.Load("BookTransform.xslt");
         transform.Transform("LesMis.xml", "LesMis.html");

         Process.Start("LesMis.html");
      }
   }
}
