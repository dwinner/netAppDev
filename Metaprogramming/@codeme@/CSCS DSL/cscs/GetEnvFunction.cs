using System;

namespace CsCsLang
{
   internal class GetEnvFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var varName = Utils.GetToken(script, Constants.EndArgArray);
         var res = Environment.GetEnvironmentVariable(varName);

         return new Variable(res);
      }
   }
}