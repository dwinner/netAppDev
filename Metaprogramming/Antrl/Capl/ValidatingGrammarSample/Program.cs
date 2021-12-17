/*
 * Simple parsing for CAPL source code
 */

using System;
using CaplGrammar.Application;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Impl.GrammarValidation;

namespace ValidatingGrammarSample
{
    internal static class Program
    {
        private const string CaplSourceCode = "arithmetic_operators.can";

        private static void Main()
        {
            ICaplValidation sntxValidation = new CaplSyntaxValidationImpl();
            var issues = sntxValidation.GetIssues(AntlrInput.FromFile, CaplSourceCode);
            foreach (var issue in issues)
            {
                Console.WriteLine(issue);
            }
        }
    }
}