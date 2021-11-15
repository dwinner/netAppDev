namespace CsCsLang.FunctionFlow
{
   internal class FunctionCreator : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var funcName = Utils.GetToken(script, Constants.TokenSeparation);
         //Interpreter.Instance.AppendOutput("Registering function [" + funcName + "] ...");

         var args = Utils.GetFunctionSignature(script);
         if (args.Length == 1 && string.IsNullOrWhiteSpace(args[0])) args = new string[0];

         script.MoveForwardIf(Constants.StartGroup, Constants.Space);
         var parentOffset = script.Pointer;

         var body = Utils.GetBodyBetween(script, Constants.StartGroup, Constants.EndGroup);

         var customFunc = new CustomFunction(funcName, body, args);
         customFunc.ParentScript = script;
         customFunc.ParentOffset = parentOffset;

         RegisterFunction(funcName, customFunc, false /* not native */);

         return new Variable(funcName);
      }
   }
}