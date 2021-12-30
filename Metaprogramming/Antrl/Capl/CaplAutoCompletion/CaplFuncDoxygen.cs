using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public sealed class CaplFuncDoxygen
    {
        public CaplFuncDoxygen(string returnType, string definition, string argsSummary, string name, string briefDesc,
            string detailedDesc, IList<(string paramType, string paramName)> parameters)
        {
            ReturnType = returnType;
            Definition = definition;
            ArgsSummary = argsSummary;
            Name = name;
            BriefDesc = briefDesc;
            DetailedDesc = detailedDesc;
            Parameters = parameters;
        }

        public string ReturnType { get; }

        public string Definition { get; }

        public string ArgsSummary { get; }

        public string Name { get; }

        public string BriefDesc { get; }

        public string DetailedDesc { get; }

        public IList<(string paramType, string paramName)> Parameters { get; }
    }
}