namespace CsCsLang.FunctionFlow
{
   internal class ReturnStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         script.MoveForwardIf(Constants.Space);

         var result = Utils.GetItem(script);

         // If we are in Return, we are done:
         script.SetDone();
         result.IsReturn = true;

         return result;
      }
   }
}