namespace CaplAutoCompletion
{
    public sealed class CaplTypedefDoxygen
    {
        public CaplTypedefDoxygen(string name, string prototype, string definition, string briefDesc, string detailedDesc)
        {
            Name = name;
            Prototype = prototype;
            Definition = definition;
            BriefDesc = briefDesc;
            DetailedDesc = detailedDesc;
        }

        public string Name { get; }

        public string Prototype { get; }

        public string Definition { get; }

        public string BriefDesc { get; }

        public string DetailedDesc { get; }
    }
}