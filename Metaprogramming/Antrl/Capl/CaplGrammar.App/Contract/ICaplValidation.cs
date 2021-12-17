using System.Collections.Generic;
using System.Text;
using CaplGrammar.Application.Poco;

namespace CaplGrammar.Application.Contract
{
   public interface ICaplValidation
   {
      Encoding CurrentEncoding { get; set; }

      IEnumerable<CaplIssue> GetIssues(AntlrInput antlrInput, params string[] sources);

      IEnumerable<CaplIssue> GetIssues(string source, AntlrInput antlrInput);
   }
}