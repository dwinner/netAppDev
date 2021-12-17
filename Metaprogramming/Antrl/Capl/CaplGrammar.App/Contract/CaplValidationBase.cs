using System.Collections.Generic;
using System.Text;
using CaplGrammar.Application.Poco;
using static CaplGrammar.Application.InternalConstants;

namespace CaplGrammar.Application.Contract
{
    public abstract class CaplValidationBase : ICaplValidation
    {
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        protected CaplValidationBase(Encoding currentEncoding) => CurrentEncoding = currentEncoding;

        protected CaplValidationBase() : this(DefaultEncoding)
        {
        }

        public Encoding CurrentEncoding { get; set; }

        public virtual IEnumerable<CaplIssue> GetIssues(AntlrInput antlrInput, params string[] sources)
        {
            var caplIssues = new List<CaplIssue>(DefaultListCapacity);
            foreach (var src in sources)
            {
                var issues = GetIssues(src, antlrInput);
                caplIssues.AddRange(issues);
            }

            return caplIssues;
        }

        public abstract IEnumerable<CaplIssue> GetIssues(string source, AntlrInput antlrInput);
    }
}