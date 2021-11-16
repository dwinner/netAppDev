using System;

namespace CsCsLang.FunctionFlow
{
   internal class CustomFunction : ParserFunction
   {
      private readonly string[] m_args;

      private int m_parentOffset;
      private ParsingScript m_parentScript;

      internal CustomFunction(string funcName,
         string body, string[] args)
      {
         m_name = funcName;
         Body = body;
         m_args = args;
      }

      public ParsingScript ParentScript
      {
         set => m_parentScript = value;
      }

      public int ParentOffset
      {
         set => m_parentOffset = value;
      }

      public string Body { get; }

      public string Header => Constants.Function + " " + Name + " " +
                              Constants.StartArg + string.Join(", ", m_args) +
                              Constants.EndArg + " " + Constants.StartGroup;

      protected override Variable Evaluate(ParsingScript script)
      {
         bool isList;
         var functionArgs = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         //script.MoveForwardIf(Constants.END_ARG);
         script.MoveBackIf(Constants.StartGroup);

         if (functionArgs.Count != m_args.Length)
            throw new ArgumentException("Function [" + m_name + "] arguments mismatch: " +
                                        m_args.Length + " declared, " + functionArgs.Count + " supplied");

         // 1. Add passed arguments as local variables to the Parser.
         var stackLevel = new StackLevel(m_name);
         for (var i = 0; i < m_args.Length; i++) stackLevel.Variables[m_args[i]] = new GetVarFunction(functionArgs[i]);

         AddLocalVariables(stackLevel);

         // 2. Execute the body of the function.
         Variable result = null;
         var tempScript = new ParsingScript(Body);
         tempScript.ScriptOffset = m_parentOffset;
         if (m_parentScript != null)
         {
            tempScript.Char2Line = m_parentScript.Char2Line;
            tempScript.Filename = m_parentScript.Filename;
            tempScript.OriginalScript = m_parentScript.OriginalScript;
         }

         while (tempScript.Pointer < Body.Length - 1 &&
                (result == null || !result.IsReturn))
         {
            result = tempScript.ExecuteTo();
            tempScript.GoToNextStatement();
         }

         PopLocalVariables();
         //script.MoveForwardIf(Constants.END_ARG);
         //script.MoveForwardIf(Constants.END_STATEMENT);

         if (result == null) result = Variable.EmptyInstance;
         else result.IsReturn = false;

         return result;
      }
   }
}