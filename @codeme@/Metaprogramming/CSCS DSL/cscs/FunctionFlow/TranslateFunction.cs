namespace CsCsLang.FunctionFlow
{
   internal class TranslateFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var language = Utils.GetToken(script, Constants.TokenSeparation);
         var funcName = Utils.GetToken(script, Constants.TokenSeparation);

         var function = GetFunction(funcName);
         var custFunc = function as CustomFunction;
         Utils.CheckNotNull(funcName, custFunc);

         var body = Utils.BeautifyScript(custFunc.Body, custFunc.Header);
         var translated = Translation.TranslateScript(body, language);
         Translation.PrintScript(translated);

         return new Variable(translated);
      }
   }
}