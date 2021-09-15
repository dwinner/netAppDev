using System;
using System.Diagnostics;

namespace CsCsLang
{
   internal class KillFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var id = Utils.GetItem(script);
         Utils.CheckPosInt(id);

         var processId = (int) id.Value;
         try
         {
            var process = Process.GetProcessById(processId);
            process.Kill();
            Interpreter.Instance.AppendOutput("Process " + processId + " killed", true);
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't kill process " + processId +
                                        " (" + exc.Message + ")");
         }

         return Variable.EmptyInstance;
      }
   }
}