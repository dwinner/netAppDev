using System;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private static void Main()
        {
            bool validated;
            string errorMessage;
            using (var validator = new XmlValidator("capl_api.xml", "capl_api.xsd"))
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
        }
    }
}