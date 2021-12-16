using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Poco;
using CaplGrammar.Core;

namespace CaplGrammar.Application.Impl.SymbolUsageValidation
{
   internal sealed class ReferencePhaseVisitor : CaplBaseListener
   {
      private const int DefaultCapacity = 0x400;
      private readonly GlobalSpaceScope _globalScope;
      private readonly ParseTreeProperty<IScope> _scopes;
      private IScope _currentScope;
      private bool _isVariableBlock;

      internal ReferencePhaseVisitor(GlobalSpaceScope globalScope, ParseTreeProperty<IScope> scopes)
      {
         _scopes = scopes;
         _globalScope = globalScope;
         Issues = new List<CaplIssue>(DefaultCapacity);
      }

      public IList<CaplIssue> Issues { get; }

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

         var issueToken = identifier.Symbol;
         var lastChild = parentCtx.GetChild(parentCtx.ChildCount - 1);
         if (lastChild.GetText().Equals(")"))
         {
            // This is a function
            var funcName = identifier.GetText();
            var funcSymbol = _currentScope.Resolve(funcName);
            if (funcSymbol == Symbol.Null)
            {
               var issue = new CaplIssue($"No such function: '{funcName}'", issueToken);
               Issues.Add(issue);
            }

            if (funcSymbol is VariableSymbol)
            {
               var issue = new CaplIssue($"The '{funcName}' is not a function!", issueToken);
               Issues.Add(issue);
            }

            return;
         }

         if (context.ChildCount == 1)
         {
            // This is a variable usage
            var varName = identifier.GetText();
            var varSymbol = _currentScope.Resolve(varName);
            if (varSymbol == Symbol.Null)
            {
               var issue = new CaplIssue($"No such variable: '{varName}'", issueToken);
               Issues.Add(issue);
            }

            if (varSymbol is FunctionSymbol)
            {
               var issue = new CaplIssue($"{varName} is not a variable", issueToken);
               Issues.Add(issue);
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