using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public sealed class CaplClassDoxygen
    {
        public string ClassName { get; private set; }

        public ISet<CaplMemberDoxygen> Members { get; private set; }
    }

    public sealed class CaplMemberDoxygen
    {
        public string MemberName { get; private set; }

        public string Definition { get; private set; }

        public string Args { get; private set; }

        public IList<(string paramType, string paramNae)> Parameters { get; private set; }

        public MemberKindDoxygen Kind { get; private set; }
    }

    public enum MemberKindDoxygen
    {
        PublicFunc,  // kind="public-func"
        PublicEnum,  // kind="enum" under sectiondef kind="public-type" (classcapl_1_1_i_env_var.xml)
    }
}