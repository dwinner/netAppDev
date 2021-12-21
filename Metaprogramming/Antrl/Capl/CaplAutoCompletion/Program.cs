using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private const string CaplApi = "namespacecapl.xml";
        private const string CaplApiSchema = "namespacecapl.xsd";
        private static bool _isXmlValid = true;

        private static void Main()
        {
            // Check Capl doc file validation first
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(null, CaplApiSchema);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += OnValidation;
            var reader = XmlReader.Create(CaplApi, settings);
            while (reader.Read() && _isXmlValid)
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    Debug.WriteLine("In validation");
                }
            }

            Console.WriteLine(_isXmlValid ? "XML file is valid" : "XML file is invalid");
            Console.ReadKey();
        }

        private static void OnValidation(object sender, ValidationEventArgs e)
        {
            Debug.WriteLine(e.Message);
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Debug.WriteLine("Error occured: {0}", e.Exception.Message);
                    _isXmlValid = false;
                    break;
                case XmlSeverityType.Warning:
                    Debug.WriteLine("Warning occured: {0}", e.Message);
                    _isXmlValid = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}