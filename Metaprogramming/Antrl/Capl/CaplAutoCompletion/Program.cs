using System;
using System.Xml.XPath;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private const string CaplApiXml = "capl_api.xml";
        private const string CaplApiXsd = "capl_api.xsd";

        private static void Main()
        {
            bool validated;
            string errorMessage;
            using (var validator = new XmlValidator(CaplApiXml, CaplApiXsd))
            {
                validated = validator.Validate(out errorMessage);
            }

            if (validated)
            {
                Console.WriteLine("Validation is Ok");
            }
            else
            {
                Console.WriteLine("Validation error: {0}", errorMessage);
            }

            var document = new XPathDocument(CaplApiXml);
            var navigator = ((IXPathNavigable)document).CreateNavigator();
            if (navigator != null)
            {
                var iterator = navigator.Select("/doxygen/compounddef/innerclass");
                while (iterator.MoveNext())
                {
                    var caplClassName = iterator.Current.Value;
                    Console.WriteLine(caplClassName);
                }
            }
        }
    }
}