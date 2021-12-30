using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public sealed class CaplEnumDoxygen
    {
        public CaplEnumDoxygen(string enumName, Lazy<IList<string>> enumValues)
        {
            Name = enumName;
            Values = enumValues;
        }

        public string Name { get; }

        public Lazy<IList<string>> Values { get; }

        public override string ToString() => Name;
    }
}