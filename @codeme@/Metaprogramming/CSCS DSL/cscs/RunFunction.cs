using System;
using System.Diagnostics;

namespace CsCsLang
{
   internal class RunFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var processName = Utils.GetItem(script).String;
         Utils.CheckNotEmpty(processName, "processName");

         var args = Utils.GetFunctionArgs(script);
         var processId = -1;

         try
         {
            var pr = Process.Start(processName, string.Join("", args.ToArray()));
            processId = pr.Id;
         }
         catch (Exception exc)
         {
            throw new ArgumentException("Couldn't start [" + processName + "]: " + exc.Message);
         }

         Interpreter.Instance.AppendOutput("Process " + processName + " started, id: " + processId, true);
         return new Variable(processId);
      }
   }
}