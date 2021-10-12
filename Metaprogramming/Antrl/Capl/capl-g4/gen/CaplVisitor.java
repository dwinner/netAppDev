// Generated from D:/Projects/dotNET/appDev-NET/Metaprogramming/Antrl/Capl_grammar/capl-g4/grammar\Capl.g4 by ANTLR 4.9.1
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link CaplParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface CaplVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link CaplParser#primaryExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrimaryExpression(CaplParser.PrimaryExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#startBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStartBlock(CaplParser.StartBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#variableBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVariableBlock(CaplParser.VariableBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#eventBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEventBlock(CaplParser.EventBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#timerBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTimerBlock(CaplParser.TimerBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#errorFrame}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitErrorFrame(CaplParser.ErrorFrameContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#messageBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMessageBlock(CaplParser.MessageBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#stopMeasurement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStopMeasurement(CaplParser.StopMeasurementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#envBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEnvBlock(CaplParser.EnvBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#postfixExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPostfixExpression(CaplParser.PostfixExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#argumentExpressionList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArgumentExpressionList(CaplParser.ArgumentExpressionListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#unaryExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitUnaryExpression(CaplParser.UnaryExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#unaryOperator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitUnaryOperator(CaplParser.UnaryOperatorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#castExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCastExpression(CaplParser.CastExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#multiplicativeExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMultiplicativeExpression(CaplParser.MultiplicativeExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#additiveExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAdditiveExpression(CaplParser.AdditiveExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#shiftExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitShiftExpression(CaplParser.ShiftExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#relationalExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRelationalExpression(CaplParser.RelationalExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#equalityExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEqualityExpression(CaplParser.EqualityExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#andExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAndExpression(CaplParser.AndExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#exclusiveOrExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExclusiveOrExpression(CaplParser.ExclusiveOrExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#inclusiveOrExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInclusiveOrExpression(CaplParser.InclusiveOrExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#logicalAndExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogicalAndExpression(CaplParser.LogicalAndExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#logicalOrExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLogicalOrExpression(CaplParser.LogicalOrExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#conditionalExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConditionalExpression(CaplParser.ConditionalExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#assignmentExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignmentExpression(CaplParser.AssignmentExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#assignmentOperator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignmentOperator(CaplParser.AssignmentOperatorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpression(CaplParser.ExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#constantExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitConstantExpression(CaplParser.ConstantExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclaration(CaplParser.DeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declarationSpecifiers}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarationSpecifiers(CaplParser.DeclarationSpecifiersContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declarationSpecifiers2}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarationSpecifiers2(CaplParser.DeclarationSpecifiers2Context ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declarationSpecifier}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarationSpecifier(CaplParser.DeclarationSpecifierContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#initDeclaratorList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitDeclaratorList(CaplParser.InitDeclaratorListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#initDeclarator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitDeclarator(CaplParser.InitDeclaratorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#typeSpecifier}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTypeSpecifier(CaplParser.TypeSpecifierContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#messageType}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMessageType(CaplParser.MessageTypeContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#messageIdentifier}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMessageIdentifier(CaplParser.MessageIdentifierContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#specifierQualifierList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSpecifierQualifierList(CaplParser.SpecifierQualifierListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declarator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarator(CaplParser.DeclaratorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#directDeclarator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDirectDeclarator(CaplParser.DirectDeclaratorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#nestedParenthesesBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNestedParenthesesBlock(CaplParser.NestedParenthesesBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#parameterTypeList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParameterTypeList(CaplParser.ParameterTypeListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#parameterList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParameterList(CaplParser.ParameterListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#parameterDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParameterDeclaration(CaplParser.ParameterDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#identifierList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdentifierList(CaplParser.IdentifierListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#typeName}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTypeName(CaplParser.TypeNameContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#abstractDeclarator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAbstractDeclarator(CaplParser.AbstractDeclaratorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#directAbstractDeclarator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDirectAbstractDeclarator(CaplParser.DirectAbstractDeclaratorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#initializer}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitializer(CaplParser.InitializerContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#initializerList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitInitializerList(CaplParser.InitializerListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#designation}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDesignation(CaplParser.DesignationContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#designatorList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDesignatorList(CaplParser.DesignatorListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#designator}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDesignator(CaplParser.DesignatorContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStatement(CaplParser.StatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#labeledStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLabeledStatement(CaplParser.LabeledStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#compoundStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCompoundStatement(CaplParser.CompoundStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#blockItemList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBlockItemList(CaplParser.BlockItemListContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#blockItem}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBlockItem(CaplParser.BlockItemContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#expressionStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpressionStatement(CaplParser.ExpressionStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#selectionStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSelectionStatement(CaplParser.SelectionStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#iterationStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIterationStatement(CaplParser.IterationStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#forCondition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForCondition(CaplParser.ForConditionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#forDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForDeclaration(CaplParser.ForDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#forExpression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForExpression(CaplParser.ForExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#jumpStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitJumpStatement(CaplParser.JumpStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#compilationUnit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCompilationUnit(CaplParser.CompilationUnitContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#translationUnit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTranslationUnit(CaplParser.TranslationUnitContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#externalDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExternalDeclaration(CaplParser.ExternalDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#functionDefinition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionDefinition(CaplParser.FunctionDefinitionContext ctx);
	/**
	 * Visit a parse tree produced by {@link CaplParser#declarationList}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarationList(CaplParser.DeclarationListContext ctx);
}