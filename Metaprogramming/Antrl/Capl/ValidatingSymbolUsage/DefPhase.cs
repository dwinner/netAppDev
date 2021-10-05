using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public class DefPhase : CaplBaseListener
   {
      private ParseTreeProperty<IScope> _scopes = new ParseTreeProperty<IScope>();
      private GlobalScope _globals;
      private IScope _currentScope; // define symbols in this scope

      public override void EnterExternalDeclaration(CaplParser.ExternalDeclarationContext context)
      {
         base.EnterExternalDeclaration(context);
      }

      public override void ExitExternalDeclaration(CaplParser.ExternalDeclarationContext context)
      {
         base.ExitExternalDeclaration(context);
      }

      public override void EnterVariableBlock(CaplParser.VariableBlockContext context)
      {
         base.EnterVariableBlock(context);
      }

      public override void ExitVariableBlock(CaplParser.VariableBlockContext context)
      {
         base.ExitVariableBlock(context);
      }

      public override void EnterFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         base.EnterFunctionDefinition(context);
      }

      public override void ExitFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         base.ExitFunctionDefinition(context);
      }
   }
}