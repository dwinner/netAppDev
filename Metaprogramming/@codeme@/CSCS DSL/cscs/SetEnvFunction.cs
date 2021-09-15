using System;

namespace CsCsLang
{
   internal class SetEnvFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var varName = Utils.GetToken(script, Constants.NextArgArray);
         Utils.CheckNotEmpty(script, varName, m_name);

         var varValue = Utils.GetItem(script);
         var strValue = varValue.AsString();
         Environment.SetEnvironmentVariable(varName, strValue);

         return new Variable(varName);
      }
   }
}