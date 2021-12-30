using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CaplAutoCompletion
{
    internal static class Program
    {
        private const string CaplApiXml = "capl_api.xml";
        private const string CaplApiXsd = "capl_api.xsd";
        private const string InnerClsEl = "innerclass";
        private const string NameEl = "name";
        private const string EnumValueEl = "enumvalue";
        private const string DefaultDefinition = "No definition";
        private const string DoxyTypeEl = "type";
        private const string DefEl = "definition";
        private const string InitEl = "initializer";
        private const string DefaultInitializer = "No initializer";
        private const string BriefDescEl = "briefdescription";
        private const string DefaultDesc = "No brief description";
        private const string DetailedDescEl = "detaileddescription";
        private const string DefaultDetailedDesc = "No detailed description";
        private const string ArgStrEl = "argsstring";
        private const string DeclNameEl = "declname";
        private static readonly XElement XRoot;

        static Program() => XRoot = XElement.Load(CaplApiXml);

        private static void Main()
        {
            using (var validator = new DoxygenValidator(CaplApiXml, CaplApiXsd))
            {
                var validated = validator.Validate(out var errorMessage);
                if (!validated)
                {
                    Console.Error.WriteLine(errorMessage);
                    Environment.Exit(-1);
                }
            }

            var caplFuncDoxygens = GetCaplFunctions(XRoot);

            // Get all classes API
            var doxygenFiles = Directory.EnumerateFiles("capl_classes", "*.xml").ToArray();
            foreach (var clsDoxygen in doxygenFiles)
            {
                var clsXroot = XElement.Load(clsDoxygen);
                var qClassName = clsXroot.XPathSelectElement("/compounddef/compoundname")?.Value;
                Console.WriteLine(qClassName);
            }
        }

        private static IEnumerable<CaplFuncDoxygen> GetCaplFunctions(XNode xRoot) =>
            from funcEl in xRoot.XPathSelectElements("/compounddef/sectiondef[@kind='func']/memberdef")
            let returnType = funcEl.Element(DoxyTypeEl)?.Value ?? string.Empty
            let definition = funcEl.Element(DefEl)?.Value ?? string.Empty
            let argsSummary = funcEl.Element(ArgStrEl)?.Value ?? string.Empty
            let funcName = funcEl.Element(NameEl)?.Value ?? string.Empty
            let brief = funcEl.Element(BriefDescEl)?.Value.StripTags().Trim() ?? DefaultDesc
            let detail = funcEl.Element(DetailedDescEl)?.Value.StripTags().Trim() ?? DefaultDetailedDesc
            where !string.IsNullOrWhiteSpace(funcName)
            let paramElements = funcEl.XPathSelectElements("./param").ToList()
            let funcParameters = GetCaplFuncParameters(paramElements)
            select new CaplFuncDoxygen(
                returnType, definition, argsSummary, funcName, brief, detail, funcParameters);

        private static List<(string paramType, string paramName)> GetCaplFuncParameters(
            IReadOnlyCollection<XElement> paramElements)
        {
            var funcParameters = paramElements.Count > 0
                ? new List<(string paramType, string paramName)>(0x10)
                : new List<(string paramType, string paramName)>(0);
            funcParameters.AddRange(
                from paramEl in paramElements
                let paramType = paramEl.Element(DoxyTypeEl)?.Value ?? string.Empty
                let paramName = paramEl.Element(DeclNameEl)?.Value ?? string.Empty
                where !string.IsNullOrEmpty(paramType) && !string.IsNullOrEmpty(paramName)
                select (paramType, paramName));

            return funcParameters;
        }

        private static IEnumerable<CaplVarDoxygen> GetCaplVars(XNode xRoot) =>
            from varEl in xRoot.XPathSelectElements("/compounddef/sectiondef[@kind='var']/memberdef")
            let varType = varEl.Element(DoxyTypeEl)?.Value ?? string.Empty
            let varDef = varEl.Element(DefEl)?.Value ?? DefaultDefinition
            let varName = varEl.Element(NameEl)?.Value ?? string.Empty
            let varInit = varEl.Element(InitEl)?.Value ?? DefaultInitializer
            let varBriefDesc = varEl.Element(BriefDescEl)?.Value.StripTags() ?? DefaultDesc
            let varDetailedDesc = varEl.Element(DetailedDescEl)?.Value.StripTags() ?? DefaultDetailedDesc
            where !string.IsNullOrWhiteSpace(varName)
            select new CaplVarDoxygen(varType, varDef, varName, varInit, varBriefDesc, varDetailedDesc);

        private static IEnumerable<CaplTypedefDoxygen> GetCaplTypedefs(XNode xRoot) =>
            from typedefEl in xRoot.XPathSelectElements("/compounddef/sectiondef[@kind='typedef']/memberdef")
            let prototype =
                $"{typedefEl.Element(DoxyTypeEl)?.Value ?? string.Empty}{typedefEl.Element(ArgStrEl)?.Value ?? string.Empty}"
            let typedefName = typedefEl.Element(NameEl)?.Value ?? string.Empty
            let briefDesc = typedefEl.Element(BriefDescEl)?.Value.StripTags() ?? DefaultDesc
            let detailedDesc = typedefEl.Element(DetailedDescEl)?.Value.StripTags() ?? DefaultDetailedDesc
            let definition = typedefEl.Element(DefEl)?.Value ?? DefaultDefinition
            where !string.IsNullOrWhiteSpace(typedefName)
            select new CaplTypedefDoxygen(typedefName, prototype, definition, briefDesc, detailedDesc);

        private static IEnumerable<CaplEnumDoxygen> GetCaplEnums(XNode xRoot) =>
            from enumElement in xRoot.XPathSelectElements("/compounddef/sectiondef[@kind='enum']/memberdef")
            let enumName = enumElement.XPathSelectElement("./name")?.Value
            where !string.IsNullOrWhiteSpace(enumName)
            let deferredEnValues = new Lazy<IList<string>>(
                () => new List<string>(
                    from enumValue in enumElement.Elements(EnumValueEl)
                    let currentValue = enumValue.Element(NameEl)?.Value ?? string.Empty
                    where !string.IsNullOrWhiteSpace(currentValue)
                    select currentValue),
                LazyThreadSafetyMode.ExecutionAndPublication)
            select new CaplEnumDoxygen(enumName, deferredEnValues);

        private static IEnumerable<string> GetCaplClassNames(XContainer xRoot) =>
            from classContent in from element in xRoot.Descendants(InnerClsEl)
                select element.Value
            where !string.IsNullOrWhiteSpace(classContent)
            select classContent.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)
            into classified
            where classified.Length == 2
            select classified[1];
    }
}