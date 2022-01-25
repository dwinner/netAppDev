using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private const string CaplApiXml = "intellisense.xml";
        private const string CaplApiXsd = "intellisense.xsd";
        private static readonly XElement XRoot;

        static Program() => XRoot = XElement.Load(CaplApiXml);

        private static void Main()
        {
            using (var validator = XmlSchemaValidator.CreateInstance(CaplApiXml, CaplApiXsd))
            {
                var validated = validator.Validate(out var errorMessage);
                if (!validated)
                {
                    Console.Error.WriteLine(errorMessage);
                    Environment.Exit(-1);
                }
            }

            var classes = XRoot.XPathSelectElements("/class");
            foreach (var xEl in classes)
            {
                var className = xEl.Attribute("name")?.Value.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(className))
                {
                    Console.WriteLine(className);
                }
            }

            /*var methods = XRoot.XPathSelectElements("/method");
            foreach (var xEl in methods)
            {
                var funcName = xEl.Attribute("name")?.Value.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(funcName))
                {
                    Console.WriteLine(funcName);
                }
            }*/
        }
    }
}