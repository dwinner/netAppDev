using System;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public sealed class ReferencePhaseVisitor : CaplBaseListener
   {
      private readonly GlobalSpaceScope _globalScope;
      private readonly ParseTreeProperty<IScope> _scopes;
      private IScope _currentScope;
      private bool _isVariableBlock;

      public ReferencePhaseVisitor(GlobalSpaceScope globalScope, ParseTreeProperty<IScope> scopes)
      {
         _scopes = scopes;
         _globalScope = globalScope;
      }

      public override void EnterPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         var parentCtx = context.Parent;
         if (parentCtx != null)
         {
            return;
         }

         _currentScope = _globalScope;
      }

      public override void ExitPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         var parentCtx = context.Parent;
         if (parentCtx == null)
         {
            return;
         }

         var identifier = context.Identifier();
         if (identifier == null)
         {
            return;
         }

         var lastChild = parentCtx.GetChild(parentCtx.ChildCount - 1);
         if (lastChild.GetText().Equals(")"))
         {
            // This is a function
            var funcName = identifier.GetText();
            var funcSymbol = _currentScope.Resolve(funcName);

            // TODO: notify or collect errors
            if (funcSymbol == Symbol.Null)
            {
               Console.WriteLine($"Error! No such function: {funcName}");
            }

            if (funcSymbol is VariableSymbol)
            {
               Console.WriteLine($"Error! {funcName} is not a function!");
            }

            return;
         }

         if (context.ChildCount == 1)
         {
            // This is a variable usage
            var varName = identifier.GetText();
            var varSymbol = _currentScope.Resolve(varName);

            // TODO: notify or collect errors
            if (varSymbol == Symbol.Null)
            {
               Console.WriteLine($"No such variable: {varName}");
            }

            if (varSymbol is FunctionSymbol)
            {
               Console.WriteLine($"{varName} is not a variable");
            }
         }
      }

      public override void EnterFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         _currentScope = _scopes.Get(context);
      }

      public override void ExitFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         _currentScope = _currentScope?.EnclosingScope;
      }

      public override void EnterVariableBlock(CaplParser.VariableBlockContext context)
      {
         _currentScope = _globalScope;
         _isVariableBlock = true;
      }

      public override void ExitVariableBlock(CaplParser.VariableBlockContext context)
      {
         _currentScope = _globalScope;
         _isVariableBlock = false;
      }

      public override void EnterBlockItemList(CaplParser.BlockItemListContext context)
      {
         if (_isVariableBlock)
         {
            return;
         }

         _currentScope = _scopes.Get(context);
      }

      public override void ExitBlockItemList(CaplParser.BlockItemListContext context)
      {
         if (_isVariableBlock)
         {
            return;
         }

         _currentScope = _currentScope?.EnclosingScope;
      }
   }
}