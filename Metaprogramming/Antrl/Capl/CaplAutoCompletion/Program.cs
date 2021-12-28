using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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

            var xRoot = XElement.Load(CaplApiXml);

            // Find all CAPL's enums and there values
            var enumElements = xRoot.XPathSelectElements("/compounddef/sectiondef[@kind='enum']/memberdef");
            var caplEnums = new Dictionary<string, List<(string enumVal, string enumDesc)>>();
            foreach (var enumElement in enumElements)
            {
                var enumName = enumElement.XPathSelectElement("./name")?.Value;
                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    var enumItems = new List<(string enumVal, string enumDesc)>();
                    var xElements =
                        from enumValue in enumElement.Elements("enumvalue")
                        let currentValue = enumValue.Element("name")?.Value ?? string.Empty
                        let enumDecs = enumValue.Element("briefdescription")?.Element("param")?.Value ?? "No description"
                        where !string.IsNullOrWhiteSpace(enumName)
                        select (currentValue, enumDecs);
                    enumItems.AddRange(xElements);
                    caplEnums.Add(enumName, enumItems);
                }
            }

            Console.WriteLine();
        }

        private static IEnumerable<string> GetCaplClassNames(XContainer xRoot) =>
            from classContent in from element in xRoot.Descendants("innerclass")
                select element.Value
            where !string.IsNullOrWhiteSpace(classContent)
            select classContent.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)
            into classified
            where classified.Length == 2
            select classified[1];
    }
}