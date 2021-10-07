using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public class DefinitionPhase : CaplBaseListener
   {
      private IScope _currentScope; // define symbols in this scope
      public ParseTreeProperty<IScope> Scopes { get; } = new();
      public GlobalScope Globals { get; private set; }

      public override void EnterPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         // primary expr is left recursive, so the root is being walking only onc: when there is no parent context
         var parentCtx = context.Parent;
         if (parentCtx != null)
         {
            // dont't reenter if it's not the root
            return;
         }

         Globals = new GlobalScope(null); // TODO: Use null-object there
         _currentScope = Globals;
      }

      public override void ExitPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         Debug.WriteLine(Globals);
      }

      /*
      public override void EnterFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         var funcName = context.declarator().directDeclarator().Identifier().GetText();
         var declarationSpecifiers = context.declarationSpecifiers().declarationSpecifier();
         var specifierCtx = declarationSpecifiers.FirstOrDefault();
         if (specifierCtx == null)
         {
            return;
         }

         var tokenType = specifierCtx.typeSpecifier().Start.Type;
         var funcRetType = CheckSymbolsUtils.GetCaplType(tokenType);

         // push new scope by making new one that points to enclosing scope
         var function = new FunctionSymbol(funcName, funcRetType, _currentScope);
         _currentScope.Define(function); // define function in current scope
         SaveScope(context, function); // push: set function's parent to current
         _currentScope = function;
      }

      public override void ExitFunctionDefinition(CaplParser.FunctionDefinitionContext context)
      {
         Debug.WriteLine(_currentScope);
         _currentScope = _currentScope.EnclosingScope;
      }

      public override void EnterBlockItemList(CaplParser.BlockItemListContext context)
      {
         // push new local scope
         _currentScope = new LocalScope(_currentScope);
         SaveScope(context, _currentScope);
      }

      public override void ExitBlockItemList(CaplParser.BlockItemListContext context)
      {
         Debug.WriteLine(_currentScope);
         _currentScope = _currentScope?.EnclosingScope;// pop scope
      }      

      public override void ExitParameterDeclaration(CaplParser.ParameterDeclarationContext context)
      {
         var declCtx = context.declarationSpecifiers().declarationSpecifier().FirstOrDefault();
         if (declCtx == null)
         {
            return;
         }

         var tokenType = declCtx.typeSpecifier().Start.Type;
         var parameterType = CheckSymbolsUtils.GetCaplType(tokenType);
         var parameterName = context.declarator().directDeclarator().Identifier().Symbol;

         DefineVar(parameterType, parameterName);
      }
      */

      public override void ExitDeclaration(CaplParser.DeclarationContext context)
      {
         // Get the type for variable declaration
         var declSpecCtx = context.declarationSpecifiers().declarationSpecifier();
         var typeSpecCtx = declSpecCtx
            .Select(ctx => ctx.typeSpecifier())
            .FirstOrDefault(ctx => ctx != null);
         if (typeSpecCtx == null)
         {
            return;
         }

         var declType = typeSpecCtx.Start.Type.GetCaplType();
         var userTypeToken = declType switch
         {
            BuiltInType.Struct => typeSpecCtx.structSpecifier()?.Identifier()?.Symbol,
            BuiltInType.Enum => typeSpecCtx.enumSpecifier()?.Identifier()?.Symbol,
            _ => null
         };

         // Get variable(s) name(s)
         // PERFORMANCE-NOTE: ToList() is waste, use morelinq lib
         var list = new List<IToken>();
         foreach (var declCtx in context.initDeclaratorList().initDeclarator())
         {
            var topDeclarator = declCtx.declarator()?.directDeclarator();
            var declToken = topDeclarator?.Identifier()?.Symbol // Primitive type
                            ?? topDeclarator?.directDeclarator()?.Identifier()?.Symbol;   // Array
            if (declToken != null)
            {
               list.Add(declToken);
            }
         }

         list.ForEach(idToken => DefineVar(declType, idToken, userTypeToken));
      }

      private void DefineVar(BuiltInType aType, IToken aNameToken, IToken userType)
      {
         var userDefType = userType == null ? string.Empty : userType.Text;
         var variable = new VariableSymbol(aNameToken.Text, aType, userDefType);
         _currentScope.Define(variable);
      }

      private void SaveScope(ParserRuleContext aContext, IScope aScope)
      {
         Scopes.Put(aContext, aScope);
      }
   }
}