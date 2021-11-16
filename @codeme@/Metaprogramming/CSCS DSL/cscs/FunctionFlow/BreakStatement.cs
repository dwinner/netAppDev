namespace CsCsLang.FunctionFlow
{
   internal class BreakStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => new Variable(Variable.VarType.Break);
   }
}