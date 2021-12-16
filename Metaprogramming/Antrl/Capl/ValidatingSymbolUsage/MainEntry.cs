/*
 * Symbol usage validation
 */

using System;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Impl.SymbolUsageValidation;

namespace ValidatingSymbolUsage
{
   internal static class MainEntry
   {
      private const string CanFile = "usages_full.can";

      private static void Main()
      {
         ICaplValidation validation = new CaplSymbolUsageValidationImpl();
         var issues = validation.GetIssues(CanFile);
         foreach (var issue in issues)
         {
            Console.WriteLine(issue);
         }
      }
   }
}