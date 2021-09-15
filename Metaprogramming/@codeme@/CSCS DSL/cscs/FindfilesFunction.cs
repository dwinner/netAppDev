using System;
using System.Collections.Generic;
using System.IO;

namespace CsCsLang
{
   internal class FindfilesFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var patterns = Utils.GetFunctionArgs(script);
         if (patterns.Count == 0) patterns.Add("*.*");

         List<Variable> results = null;
         try
         {
            var pwd = Directory.GetCurrentDirectory();
            var files = Utils.GetFiles(pwd, patterns.ToArray());

            results = Utils.ConvertToResults(files.ToArray(), true);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't list directory: " + exc.Message);
         }

         return new Variable(results);
      }
   }
}