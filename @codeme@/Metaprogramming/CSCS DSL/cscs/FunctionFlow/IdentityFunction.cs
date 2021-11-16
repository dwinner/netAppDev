namespace CsCsLang.FunctionFlow
{
   internal class IdentityFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => script.ExecuteTo(Constants.EndArg);
   }
}