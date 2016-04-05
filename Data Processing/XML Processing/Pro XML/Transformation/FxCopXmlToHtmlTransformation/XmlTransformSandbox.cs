/**
 * Трансформация лога диагностики FxCop'а из XML в HTML-формат
 */

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace FxCopXmlToHtmlTransformation
{
   internal static class XmlTransformSandbox
   {
      private const string CodeAnalysisLogXml = "CodeAnalysisLog.xml";

      private static readonly Tuple<string, string>[] XsltTemplatesPair =
      {
         Tuple.Create("LinearTable.xslt", "LinearTable.html")
      };

      private static void Main()
      {
         Parallel.ForEach(XsltTemplatesPair, ExecuteTransform);
      }

      /// <summary>
      /// Выполняет преобразование, открывает преобразованный файл в браузере
      /// </summary>
      /// <param name="tplPair">Кортеж [xslt-файл, html-файл]</param>
      private static void ExecuteTransform(Tuple<string, string> tplPair)
      {
         var compiledTransform = new XslCompiledTransform(true);
         compiledTransform.Load(tplPair.Item1);
         compiledTransform.Transform(CodeAnalysisLogXml, tplPair.Item2);
         Process.Start(tplPair.Item2);
      }
   }
}