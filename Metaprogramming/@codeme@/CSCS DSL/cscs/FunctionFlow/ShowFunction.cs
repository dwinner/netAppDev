namespace CsCsLang.FunctionFlow
{
   internal class ShowFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var funcName = Utils.GetToken(script, Constants.TokenSeparation);
         var function = GetFunction(funcName);
         var custFunc = function as CustomFunction;
         Utils.CheckNotNull(funcName, custFunc);

         var body = Utils.BeautifyScript(custFunc.Body, custFunc.Header);
         Translation.PrintScript(body);

         return new Variable(body);
      }
   }

   // Get a value of a variable or of an array element
}