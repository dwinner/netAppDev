using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CaplAutoCompletion
{
    public sealed class CaplIntellisense
    {
        private static CaplIntellisense _instance;
        private readonly XElement _root;
        private CaplApi _caplApi;

        private CaplIntellisense(string caplXml) => _root = XElement.Load(caplXml);

        public CaplApi Api
        {
            get
            {
                if (_caplApi == null)
                {
                    // Get CAPL' classes
                    var caplClasses =
                        from classElement in _root.XPathSelectElements("/class")
                        let className = classElement.Attribute("name").GetValue()
                        let baseClassName = classElement.Attribute("base").GetValue()
                        let fieldSet =
                            new HashSet<FieldType>(
                                classElement.Elements("field").Select(fieldElement => fieldElement.GetFieldType()),
                                FieldType.DefaultComparer)
                        let indexerSet =
                            new HashSet<IndexerType>(
                                classElement.Elements("indexer")
                                    .Select(indexerElement => indexerElement.GetIndexerType()),
                                IndexerType.DefaultComparer)
                        let methodSet =
                            new HashSet<FunctionType>(
                                classElement.Elements("method")
                                    .Select(methodElement => methodElement.GetFunctionType()),
                                FunctionType.DefaultComparer)
                        select new CaplClass(className, baseClassName, fieldSet, indexerSet, methodSet);

                    // Get CAPL's functions
                    var caplFunctions = _root.XPathSelectElements("/method")
                        .Select(funcElement => funcElement.GetFunctionType());

                    // Wrap in CAPL's API
                    _caplApi = new CaplApi(new HashSet<FunctionType>(caplFunctions, FunctionType.DefaultComparer),
                        new HashSet<CaplClass>(caplClasses, CaplClass.DefaultComparer));
                }

                return _caplApi;
            }
        }

        public static CaplIntellisense GetInstance(string caplXml) =>
            _instance ?? (_instance = new CaplIntellisense(caplXml));
    }
}