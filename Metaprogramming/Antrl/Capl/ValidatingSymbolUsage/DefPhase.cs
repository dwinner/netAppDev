using System.Diagnostics;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public class DefPhase : CaplBaseListener
   {
      private IScope _currentScope; // define symbols in this scope
      public ParseTreeProperty<IScope> Scopes { get; } = new();
      public GlobalScope Globals { get; private set; }

      public override void EnterPrimaryExpression(CaplParser.PrimaryExpressionContext context)
      {
         var parentCtx = context.Parent;

         // primary expr is left recursive, so the root is being walking only once - than there is no parent context
         if (parentCtx == null)
         {
            Globals = new GlobalScope(null); // TODO: Use null-object there
            _currentScope = Globals;
         }
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
         var declCtx = context.declarationSpecifiers().declarationSpecifier().FirstOrDefault();
         if (declCtx == null)
         {
            return;
         }

         var tokenType = declCtx.typeSpecifier().Start.Type;
         var declType = CheckSymbolsUtils.GetCaplType(tokenType);
         var initDeclarators = context.initDeclaratorList().initDeclarator();

         // TODO: ofc, there can be more than one, i.e. float x, y, z...
         var initDeclCtx = initDeclarators.FirstOrDefault();
         var declName = initDeclCtx.declarator().directDeclarator().Identifier().Symbol;

         DefineVar(declType, declName);
      }

      private void DefineVar(BuiltInType aType, IToken aName)
      {
         var variable = new VariableSymbol(aName.Text, aType);
         _currentScope.Define(variable);
      }

      private void SaveScope(ParserRuleContext aContext, IScope aScope)
      {
         Scopes.Put(aContext, aScope);
      }
   }
}