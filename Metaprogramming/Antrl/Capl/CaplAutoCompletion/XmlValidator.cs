using System;
using System.Xml;
using System.Xml.Schema;

namespace CaplAutoCompletion
{
    public sealed class XmlValidator : DisposableBase
    {
        private readonly string _xmlFile;
        private readonly XmlReaderSettings _xmlReaderSettings;
        private string _errorMessage;
        private bool _isXmlValid;

        public XmlValidator(string xmlFile, string xsdFile)
        {
            _xmlFile = xmlFile;
            _xmlReaderSettings = new XmlReaderSettings();
            _xmlReaderSettings.Schemas.Add(null, xsdFile);
            _xmlReaderSettings.ValidationType = ValidationType.Schema;
            _xmlReaderSettings.ValidationEventHandler += OnValidation;
        }

        public bool Validate(out string errorMessage)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XmlValidator),
                    $"One time using instance for class {nameof(XmlValidator)}");
            }

            var reader = XmlReader.Create(_xmlFile, _xmlReaderSettings);
            _isXmlValid = true;
            _errorMessage = string.Empty;
            while (reader.Read() && _isXmlValid)
            {
            }

            errorMessage = _errorMessage;
            return _isXmlValid;
        }

        private void OnValidation(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    _isXmlValid = false;
                    var xmlEx = e.Exception;
                    var message = xmlEx.Message;
                    var lineNumber = xmlEx.LineNumber;
                    var columnNumber = xmlEx.LinePosition;
                    var sourceUri = xmlEx.SourceUri;
                    _errorMessage =
                        $"Validation error occured: '{message}'. At line: '{lineNumber}'. At column: '{columnNumber}'. Source: '{sourceUri}'.";
                    break;
                case XmlSeverityType.Warning:
                    _isXmlValid = false;
                    _errorMessage = e.Message;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void UnmanagedCleaning()
        {
        }

        protected override void ManagedCleaning()
        {
            _xmlReaderSettings.ValidationEventHandler -= OnValidation;
        }
    }
}