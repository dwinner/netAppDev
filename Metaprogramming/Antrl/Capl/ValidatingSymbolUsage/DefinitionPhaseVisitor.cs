using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public class DefinitionPhaseVisitor : CaplBaseListener
   {
      private IScope _currentScope; // define symbols in this scope

      // NOTE maybe you need to extend it for more flexible using
      public ParseTreeProperty<IScope> Scopes { get; } = new();

      public GlobalSpaceScope GlobalScope { get; private set; }

      private bool _isVariableBlock;

      public override void EnterPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         // primary expr is left recursive, so the root is being walking only once: when there is no parent context
         var parentCtx = context.Parent;
         if (parentCtx != null)
         {
            // dont't reenter if it's not the root
            return;
         }

         GlobalScope = new GlobalSpaceScope(null); // TODO: Use null-object there
         _currentScope = GlobalScope;
      }

      public override void ExitPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         Debug.WriteLine(GlobalScope);
      }

      public override void EnterVariableBlock(CaplParser.VariableBlockContext context)
      {
         _currentScope = GlobalScope;
         _isVariableBlock = true;
      }

      public override void ExitVariableBlock(CaplParser.VariableBlockContext context)
      {
         Debug.WriteLine(_currentScope);
         _currentScope = GlobalScope;
         _isVariableBlock = false;
      }

      public override void EnterFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         var funcName = context.declarator()?.directDeclarator()?.directDeclarator()?.Identifier()?.GetText()
                        ?? throw new ArgumentNullException(nameof(EnterFunctionDefinition),
                           "Unable to select function name");

         var typeSpecifier = context.declarationSpecifiers()?.declarationSpecifier()
            .Select(declSpec => declSpec.typeSpecifier())
            .FirstOrDefault(ctx => ctx != null);

         var returnType = typeSpecifier?.Start.Type.GetCaplType() ?? BuiltInType.Void;
         var userTypeToken = GetUserTypeToken(returnType, typeSpecifier);
         var userDefType = userTypeToken?.Text ?? string.Empty;

         // push new scope by making new one that points to enclosing scope
         var function = new FunctionSymbol(funcName, returnType, _currentScope, userDefType);
         _currentScope.Define(function); // define function in current scope
         SaveScope(context, function); // push: set function's parent to current
         _currentScope = function;
      }

      private static IToken GetUserTypeToken(BuiltInType builtInType, CaplParser.TypeSpecifierContext typeSpecCtx) =>
         builtInType switch
         {
            BuiltInType.Struct => typeSpecCtx.structSpecifier()?.Identifier()?.Symbol,
            BuiltInType.Enum => typeSpecCtx.enumSpecifier()?.Identifier()?.Symbol,
            _ => null // TODO: avoid using null, prefer null object with enum's
         };

      public override void ExitFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         Debug.WriteLine(_currentScope);
         _currentScope = _currentScope?.EnclosingScope;
      }

      public override void EnterBlockItemList(CaplParser.BlockItemListContext context)
      {
         // Variables-block is like a global scope in CAPL
         if (_isVariableBlock)
         {
            return;
         }

         // push new local scope
         var localScope = new LocalScope(_currentScope);
         _currentScope = localScope;
         SaveScope(context, _currentScope);
      }

      public override void ExitBlockItemList(CaplParser.BlockItemListContext context)
      {
         // Keep in mind that variables-scope has no nested scopes due to our handling anymore
         if (_isVariableBlock)
         {
            return;
         }

         Debug.WriteLine(_currentScope);
         _currentScope = _currentScope?.EnclosingScope; // pop scope
      }

      public override void ExitParameterDeclaration(CaplParser.ParameterDeclarationContext context)
      {
         var declSpecCtx = context.declarationSpecifiers().declarationSpecifier();
         var typeSpecCtx = GetTypeSpecContext(declSpecCtx);
         if (typeSpecCtx == null)
         {
            return;
         }

         var declType = typeSpecCtx.Start.Type.GetCaplType();
         var userTypeToken = GetUserTypeToken(declType, typeSpecCtx);

         var directDecl = context.declarator()?.directDeclarator();
         var declToken = GetTypeToken(directDecl);

         DefineVar(declType, declToken, userTypeToken);
      }

      public override void ExitDeclaration(CaplParser.DeclarationContext context)
      {
         // Get the type for variable declaration
         var declSpecCtx = context.declarationSpecifiers().declarationSpecifier();
         var typeSpecCtx = GetTypeSpecContext(declSpecCtx);
         if (typeSpecCtx == null)
         {
            return;
         }

         var declType = typeSpecCtx.Start.Type.GetCaplType();
         var userTypeToken = GetUserTypeToken(declType, typeSpecCtx);

         // Get variable(s) name(s)
         // PERFORMANCE-NOTE: ToList() is waste, use morelinq lib instead
         var declTokenList = context.initDeclaratorList().initDeclarator()
            .Select(ctx => GetTypeToken(ctx.declarator()?.directDeclarator()))
            .ToList();

         declTokenList.ForEach(idToken => DefineVar(declType, idToken, userTypeToken));
      }

      private void DefineVar(BuiltInType aType, IToken aNameToken, IToken userType)
      {
         var userDefType = userType?.Text ?? string.Empty;
         var variable = new VariableSymbol(aNameToken.Text, aType, userDefType);
         _currentScope.Define(variable);
      }

      private void SaveScope(IParseTree aContext, IScope aScope)
      {
         Scopes.Put(aContext, aScope);
      }

      private static IToken GetTypeToken(CaplParser.DirectDeclaratorContext directDeclCtx)
      {
         var declToken = directDeclCtx?.Identifier()?.Symbol;
         while (declToken == null)
         {
            directDeclCtx = directDeclCtx?.directDeclarator();
            declToken = directDeclCtx?.Identifier()?.Symbol;
         }

         return declToken;
      }

      private static CaplParser.TypeSpecifierContext
         GetTypeSpecContext(IEnumerable<CaplParser.DeclarationSpecifierContext> declSpecCtx) =>
         declSpecCtx
            .Select(ctx => ctx.typeSpecifier())
            .FirstOrDefault(ctx => ctx != null);
   }
}