namespace CsCsLang.FunctionFlow
{
   internal class ContinueStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => new Variable(Variable.VarType.Continue);
   }
}