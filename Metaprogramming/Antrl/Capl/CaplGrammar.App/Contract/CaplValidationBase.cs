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

      public virtual IEnumerable<CaplIssue> GetIssues(params string[] sourceFiles)
      {
         var caplIssues = new List<CaplIssue>(DefaultListCapacity);
         foreach (var sourceFile in sourceFiles)
         {
            var issues = GetIssues(sourceFile);
            caplIssues.AddRange(issues);
         }

         return caplIssues;
      }

      public abstract IEnumerable<CaplIssue> GetIssues(string sourceFile);
   }
}