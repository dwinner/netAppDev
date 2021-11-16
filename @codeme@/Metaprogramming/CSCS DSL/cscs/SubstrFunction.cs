using System;

namespace CsCsLang
{
   internal class SubstrFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         string substring;

         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.NextArgArray);
         Utils.CheckNotEmpty(script, varName, m_name);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         var currentValue = func.GetValue(script);

         // 3. Take either the string part if it is defined,
         // or the numerical part converted to a string otherwise.
         var arg = currentValue.AsString();
         // 4. Get the initial index of the substring.
         var init = Utils.GetItem(script);
         Utils.CheckNonNegativeInt(init);

         // 5. Get the length of the substring if available.
         var lengthAvailable = Utils.SeparatorExists(script);
         if (lengthAvailable)
         {
            var length = Utils.GetItem(script);
            Utils.CheckPosInt(length);
            if (init.Value + length.Value > arg.Length)
               throw new ArgumentException("The total substring length is larger than [" +
                                           arg + "]");
            substring = arg.Substring((int) init.Value, (int) length.Value);
         }
         else substring = arg.Substring((int) init.Value);
         var newValue = new Variable(substring);

         return newValue;
      }
   }
}