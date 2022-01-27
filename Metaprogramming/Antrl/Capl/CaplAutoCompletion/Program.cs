using System;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private const string CaplApiXml = "intellisense.xml";
        private const string CaplApiXsd = "intellisense.xsd";

        private static void Main()
        {
            // Check xml for validation
            using (var validator = XmlSchemaValidator.CreateInstance(CaplApiXml, CaplApiXsd))
            {
                var validated = validator.Validate(out var errorMessage);
                if (!validated)
                {
                    Console.Error.WriteLine(errorMessage);
                    Environment.Exit(-1);
                }
            }

            var intellisense = CaplIntellisense.GetInstance(CaplApiXml);
            var caplApi = intellisense.Api;
            Console.WriteLine(caplApi);
        }
    }
}