using System;
using System.Collections.Generic;

namespace CsCsLang.FunctionFlow
{
   internal class IncludeFile : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetItem(script).AsString();
         var lines = Utils.GetFileLines(filename);

         var includeFile = string.Join(Environment.NewLine, lines);
         Dictionary<int, int> char2Line;
         var includeScript = Utils.ConvertToScript(includeFile, out char2Line);
         var tempScript = new ParsingScript(includeScript, 0, char2Line);
         tempScript.Filename = filename;
         tempScript.OriginalScript = string.Join(Constants.EndLine.ToString(), lines);

         while (tempScript.Pointer < includeScript.Length)
         {
            tempScript.ExecuteTo();
            tempScript.GoToNextStatement();
         }
         return Variable.EmptyInstance;
      }
   }
}