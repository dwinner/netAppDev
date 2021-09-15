namespace CsCsLang.FunctionFlow
{
   internal class ForStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => Interpreter.Instance.ProcessFor(script);
   }
}