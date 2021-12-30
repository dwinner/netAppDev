namespace CaplAutoCompletion
{
    public sealed class CaplVarDoxygen
    {
        public CaplVarDoxygen(string type, string definition, string name, string initializer, string briefDesc,
            string detailedDesc)
        {
            Type = type;
            Definition = definition;
            Name = name;
            Initializer = initializer;
            BriefDesc = briefDesc;
            DetailedDesc = detailedDesc;
        }

        public string Type { get; }

        public string Definition { get; }

        public string Name { get; }

        public string Initializer { get; }

        public string BriefDesc { get; }

        public string DetailedDesc { get; }
    }
}