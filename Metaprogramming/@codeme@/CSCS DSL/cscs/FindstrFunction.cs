using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsCsLang
{
   internal class FindstrFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var search = Utils.GetStringOrVarValue(script);
         var patterns = Utils.GetFunctionArgs(script);

         var ignoreCase = true;
         if (patterns.Count > 0 && patterns.Last().Equals("case"))
         {
            ignoreCase = false;
            patterns.RemoveAt(patterns.Count - 1);
         }
         if (patterns.Count == 0) patterns.Add("*.*");

         List<Variable> results = null;
         try
         {
            var pwd = Directory.GetCurrentDirectory();
            var files = Utils.GetStringInFiles(pwd, search, patterns.ToArray(), ignoreCase);

            results = Utils.ConvertToResults(files.ToArray(), true);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't find pattern: " + exc.Message);
         }

         return new Variable(results);
      }
   }
}