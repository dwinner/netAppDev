using System.Collections.Generic;
using System.IO;
using System.Linq;
using CaplGrammar.Application;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Impl.GrammarValidation;
using CaplGrammar.Application.Poco;
using NUnit.Framework;

namespace GrammarValidation.UnitTests
{
    [TestFixture]
    public class CaplSyntaxCheckTests
    {
        private const string RootTestPath = "src";
        private const string SearchPattern = "*.can";
        private const string FmtErrorMsg = "There are unresolved syntax issues in files: '{0}'";

        private static readonly object[] TestSources =
        {
            new object[] { "ags" },
            new object[] { "au" },
            new object[] { "bap" },
            new object[] { "fgs" },
            new object[] { "hud" },
            new object[] { "keywords" },
            new object[] { "kombi" },
            new object[] { "misc" },
            new object[] { "network" },
            new object[] { "operators" },
            new object[] { "rbs" },
            new object[] { "realSimulation" },
            new object[] { "simplest" },
            new object[] { "syntax" },
            new object[] { "unit" },
            new object[] { "vdo" }
        };

        [TestCaseSource(nameof(TestSources))]
        public void TestCaplSyntax(string categoryDir)
        {
            var (issues, files) = GetIssues(categoryDir);
            var allFiles = files.Aggregate(string.Empty,
                (current, canFile) => $"{current}{Path.GetFileName(canFile)}, ");
            Assert.IsEmpty(issues, FmtErrorMsg, allFiles);
        }

        private static (IEnumerable<CaplIssue> issues, IEnumerable<string> files) GetIssues(string categoryDir)
        {
            var canFiles = new List<string>(0x80);
            canFiles.AddRange(Directory
                .EnumerateFiles(Path.Combine(RootTestPath, categoryDir), SearchPattern,
                    SearchOption.AllDirectories).Select(Path.GetFullPath));

            ICaplValidation sntxValidation = new CaplSyntaxValidationImpl();
            var issues = new List<CaplIssue>();
            var issueFiles = new List<string>();
            canFiles.ForEach(canFile =>
            {
                var fileIssues = sntxValidation.GetIssues(canFile, AntlrInput.FromFile);
                var caplIssues = fileIssues as CaplIssue[] ?? fileIssues.ToArray();
                if (caplIssues.Length > 0)
                {
                    issues.AddRange(caplIssues);
                    issueFiles.Add(canFile);
                }
            });

            return (issues, issueFiles);
        }
    }
}