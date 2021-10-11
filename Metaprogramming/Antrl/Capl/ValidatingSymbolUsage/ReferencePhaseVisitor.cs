using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public sealed class ReferencePhaseVisitor : CaplBaseListener
   {
      private ParseTreeProperty<IScope> _scopes;
      private GlobalSpaceScope _globals;
      private readonly VariableSectionScope _varScope;
      private IScope _currentScope;

      public ReferencePhaseVisitor(GlobalSpaceScope globals, VariableSectionScope varScope, ParseTreeProperty<IScope> scopes)
      {
         _scopes = scopes;
         _globals = globals;
         _varScope = varScope;
      }

      public override void EnterPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         _currentScope = _globals;
      }

      public override void EnterFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         _currentScope = _scopes.Get(context);
      }

      public override void EnterVariableBlock(CaplParser.VariableBlockContext context)
      {
         _currentScope = _varScope;
      }

      public override void ExitVariableBlock(CaplParser.VariableBlockContext context)
      {
         _currentScope = _globals;
      }

      public override void ExitFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         _currentScope = _currentScope.EnclosingScope;
      }

      public override void EnterBlockItemList(CaplParser.BlockItemListContext context)
      {
         _currentScope = _scopes.Get(context);
      }

      public override void ExitBlockItemList(CaplParser.BlockItemListContext context)
      {
         _currentScope = _currentScope.EnclosingScope;
      }

      public override void ExitIdentifierList(CaplParser.IdentifierListContext context)
      {
         base.ExitIdentifierList(context);
      }
   }
}