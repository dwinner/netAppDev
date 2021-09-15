using System.IO;

namespace CsCsLang
{
   internal class PwdFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var path = Directory.GetCurrentDirectory();
         Interpreter.Instance.AppendOutput(path, true);

         return new Variable(path);
      }
   }
}