// Generated from d:\Projects\dotNET\appDev-NET\Metaprogramming\Antrl\Capl_grammar\capl-vscode\grammar\Capl.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class CaplParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		MessageIdHex=1, Variables=2, Break=3, Case=4, Char=5, Byte=6, Continue=7, 
		Default=8, Do=9, Double=10, Else=11, Float=12, For=13, If=14, Int=15, 
		Word=16, Dword=17, Message=18, Timer=19, MsTimer=20, Long=21, Int64=22, 
		Return=23, Switch=24, Void=25, While=26, LeftParen=27, RightParen=28, 
		LeftBracket=29, RightBracket=30, LeftBrace=31, RightBrace=32, Less=33, 
		LessEqual=34, Greater=35, GreaterEqual=36, LeftShift=37, RightShift=38, 
		Plus=39, PlusPlus=40, Minus=41, MinusMinus=42, Star=43, Div=44, Mod=45, 
		And=46, Or=47, AndAnd=48, OrOr=49, Caret=50, Not=51, Tilde=52, Question=53, 
		Colon=54, Semi=55, Comma=56, Assign=57, StarAssign=58, DivAssign=59, ModAssign=60, 
		PlusAssign=61, MinusAssign=62, LeftShiftAssign=63, RightShiftAssign=64, 
		AndAssign=65, XorAssign=66, OrAssign=67, Equal=68, NotEqual=69, Ellipsis=70, 
		Identifier=71, This=72, Dot=73, Constant=74, DigitSequence=75, StringLiteral=76, 
		Whitespace=77, Newline=78, BlockComment=79, LineComment=80;
	public static final int
		RULE_primaryExpression = 0, RULE_postfixExpression = 1, RULE_argumentExpressionList = 2, 
		RULE_unaryExpression = 3, RULE_unaryOperator = 4, RULE_castExpression = 5, 
		RULE_multiplicativeExpression = 6, RULE_additiveExpression = 7, RULE_shiftExpression = 8, 
		RULE_relationalExpression = 9, RULE_equalityExpression = 10, RULE_andExpression = 11, 
		RULE_exclusiveOrExpression = 12, RULE_inclusiveOrExpression = 13, RULE_logicalAndExpression = 14, 
		RULE_logicalOrExpression = 15, RULE_conditionalExpression = 16, RULE_assignmentExpression = 17, 
		RULE_assignmentOperator = 18, RULE_expression = 19, RULE_constantExpression = 20, 
		RULE_declaration = 21, RULE_declarationSpecifiers = 22, RULE_declarationSpecifiers2 = 23, 
		RULE_declarationSpecifier = 24, RULE_initDeclaratorList = 25, RULE_initDeclarator = 26, 
		RULE_typeSpecifier = 27, RULE_messageType = 28, RULE_hexConstMessage = 29, 
		RULE_specifierQualifierList = 30, RULE_declarator = 31, RULE_directDeclarator = 32, 
		RULE_nestedParenthesesBlock = 33, RULE_parameterTypeList = 34, RULE_parameterList = 35, 
		RULE_parameterDeclaration = 36, RULE_identifierList = 37, RULE_typeName = 38, 
		RULE_abstractDeclarator = 39, RULE_directAbstractDeclarator = 40, RULE_initializer = 41, 
		RULE_initializerList = 42, RULE_designation = 43, RULE_designatorList = 44, 
		RULE_designator = 45, RULE_statement = 46, RULE_labeledStatement = 47, 
		RULE_compoundStatement = 48, RULE_blockItemList = 49, RULE_blockItem = 50, 
		RULE_expressionStatement = 51, RULE_selectionStatement = 52, RULE_iterationStatement = 53, 
		RULE_forCondition = 54, RULE_forDeclaration = 55, RULE_forExpression = 56, 
		RULE_jumpStatement = 57, RULE_compilationUnit = 58, RULE_translationUnit = 59, 
		RULE_externalDeclaration = 60, RULE_functionDefinition = 61, RULE_declarationList = 62, 
		RULE_variableDeclarationBlock = 63;
	private static String[] makeRuleNames() {
		return new String[] {
			"primaryExpression", "postfixExpression", "argumentExpressionList", "unaryExpression", 
			"unaryOperator", "castExpression", "multiplicativeExpression", "additiveExpression", 
			"shiftExpression", "relationalExpression", "equalityExpression", "andExpression", 
			"exclusiveOrExpression", "inclusiveOrExpression", "logicalAndExpression", 
			"logicalOrExpression", "conditionalExpression", "assignmentExpression", 
			"assignmentOperator", "expression", "constantExpression", "declaration", 
			"declarationSpecifiers", "declarationSpecifiers2", "declarationSpecifier", 
			"initDeclaratorList", "initDeclarator", "typeSpecifier", "messageType", 
			"hexConstMessage", "specifierQualifierList", "declarator", "directDeclarator", 
			"nestedParenthesesBlock", "parameterTypeList", "parameterList", "parameterDeclaration", 
			"identifierList", "typeName", "abstractDeclarator", "directAbstractDeclarator", 
			"initializer", "initializerList", "designation", "designatorList", "designator", 
			"statement", "labeledStatement", "compoundStatement", "blockItemList", 
			"blockItem", "expressionStatement", "selectionStatement", "iterationStatement", 
			"forCondition", "forDeclaration", "forExpression", "jumpStatement", "compilationUnit", 
			"translationUnit", "externalDeclaration", "functionDefinition", "declarationList", 
			"variableDeclarationBlock"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, "'variables'", "'break'", "'case'", "'char'", "'byte'", "'continue'", 
			"'default'", "'do'", "'double'", "'else'", "'float'", "'for'", "'if'", 
			"'int'", "'word'", "'dword'", "'message'", "'timer'", "'msTimer'", "'long'", 
			"'int64'", "'return'", "'switch'", "'void'", "'while'", "'('", "')'", 
			"'['", "']'", "'{'", "'}'", "'<'", "'<='", "'>'", "'>='", "'<<'", "'>>'", 
			"'+'", "'++'", "'-'", "'--'", "'*'", "'/'", "'%'", "'&'", "'|'", "'&&'", 
			"'||'", "'^'", "'!'", "'~'", "'?'", "':'", "';'", "','", "'='", "'*='", 
			"'/='", "'%='", "'+='", "'-='", "'<<='", "'>>='", "'&='", "'^='", "'|='", 
			"'=='", "'!='", "'...'", null, "'this'", "'.'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "MessageIdHex", "Variables", "Break", "Case", "Char", "Byte", "Continue", 
			"Default", "Do", "Double", "Else", "Float", "For", "If", "Int", "Word", 
			"Dword", "Message", "Timer", "MsTimer", "Long", "Int64", "Return", "Switch", 
			"Void", "While", "LeftParen", "RightParen", "LeftBracket", "RightBracket", 
			"LeftBrace", "RightBrace", "Less", "LessEqual", "Greater", "GreaterEqual", 
			"LeftShift", "RightShift", "Plus", "PlusPlus", "Minus", "MinusMinus", 
			"Star", "Div", "Mod", "And", "Or", "AndAnd", "OrOr", "Caret", "Not", 
			"Tilde", "Question", "Colon", "Semi", "Comma", "Assign", "StarAssign", 
			"DivAssign", "ModAssign", "PlusAssign", "MinusAssign", "LeftShiftAssign", 
			"RightShiftAssign", "AndAssign", "XorAssign", "OrAssign", "Equal", "NotEqual", 
			"Ellipsis", "Identifier", "This", "Dot", "Constant", "DigitSequence", 
			"StringLiteral", "Whitespace", "Newline", "BlockComment", "LineComment"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Capl.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public CaplParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class PrimaryExpressionContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode Constant() { return getToken(CaplParser.Constant, 0); }
		public List<TerminalNode> StringLiteral() { return getTokens(CaplParser.StringLiteral); }
		public TerminalNode StringLiteral(int i) {
			return getToken(CaplParser.StringLiteral, i);
		}
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public CompoundStatementContext compoundStatement() {
			return getRuleContext(CompoundStatementContext.class,0);
		}
		public VariableDeclarationBlockContext variableDeclarationBlock() {
			return getRuleContext(VariableDeclarationBlockContext.class,0);
		}
		public PrimaryExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primaryExpression; }
	}

	public final PrimaryExpressionContext primaryExpression() throws RecognitionException {
		PrimaryExpressionContext _localctx = new PrimaryExpressionContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_primaryExpression);
		int _la;
		try {
			setState(144);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(128);
				match(Identifier);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(129);
				match(Constant);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(131); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(130);
					match(StringLiteral);
					}
					}
					setState(133); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==StringLiteral );
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(135);
				match(LeftParen);
				setState(136);
				expression();
				setState(137);
				match(RightParen);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(139);
				match(LeftParen);
				setState(140);
				compoundStatement();
				setState(141);
				match(RightParen);
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(143);
				variableDeclarationBlock();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PostfixExpressionContext extends ParserRuleContext {
		public PrimaryExpressionContext primaryExpression() {
			return getRuleContext(PrimaryExpressionContext.class,0);
		}
		public List<TerminalNode> LeftParen() { return getTokens(CaplParser.LeftParen); }
		public TerminalNode LeftParen(int i) {
			return getToken(CaplParser.LeftParen, i);
		}
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public List<TerminalNode> RightParen() { return getTokens(CaplParser.RightParen); }
		public TerminalNode RightParen(int i) {
			return getToken(CaplParser.RightParen, i);
		}
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public InitializerListContext initializerList() {
			return getRuleContext(InitializerListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public List<TerminalNode> LeftBracket() { return getTokens(CaplParser.LeftBracket); }
		public TerminalNode LeftBracket(int i) {
			return getToken(CaplParser.LeftBracket, i);
		}
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> RightBracket() { return getTokens(CaplParser.RightBracket); }
		public TerminalNode RightBracket(int i) {
			return getToken(CaplParser.RightBracket, i);
		}
		public List<TerminalNode> Dot() { return getTokens(CaplParser.Dot); }
		public TerminalNode Dot(int i) {
			return getToken(CaplParser.Dot, i);
		}
		public List<TerminalNode> Identifier() { return getTokens(CaplParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(CaplParser.Identifier, i);
		}
		public List<TerminalNode> PlusPlus() { return getTokens(CaplParser.PlusPlus); }
		public TerminalNode PlusPlus(int i) {
			return getToken(CaplParser.PlusPlus, i);
		}
		public List<TerminalNode> MinusMinus() { return getTokens(CaplParser.MinusMinus); }
		public TerminalNode MinusMinus(int i) {
			return getToken(CaplParser.MinusMinus, i);
		}
		public TerminalNode Comma() { return getToken(CaplParser.Comma, 0); }
		public List<ArgumentExpressionListContext> argumentExpressionList() {
			return getRuleContexts(ArgumentExpressionListContext.class);
		}
		public ArgumentExpressionListContext argumentExpressionList(int i) {
			return getRuleContext(ArgumentExpressionListContext.class,i);
		}
		public PostfixExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_postfixExpression; }
	}

	public final PostfixExpressionContext postfixExpression() throws RecognitionException {
		PostfixExpressionContext _localctx = new PostfixExpressionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_postfixExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(157);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				{
				setState(146);
				primaryExpression();
				}
				break;
			case 2:
				{
				setState(147);
				match(LeftParen);
				setState(148);
				typeName();
				setState(149);
				match(RightParen);
				setState(150);
				match(LeftBrace);
				setState(151);
				initializerList();
				setState(153);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(152);
					match(Comma);
					}
				}

				setState(155);
				match(RightBrace);
				}
				break;
			}
			setState(173);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 27)) & ~0x3f) == 0 && ((1L << (_la - 27)) & ((1L << (LeftParen - 27)) | (1L << (LeftBracket - 27)) | (1L << (PlusPlus - 27)) | (1L << (MinusMinus - 27)) | (1L << (Dot - 27)))) != 0)) {
				{
				setState(171);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LeftBracket:
					{
					setState(159);
					match(LeftBracket);
					setState(160);
					expression();
					setState(161);
					match(RightBracket);
					}
					break;
				case LeftParen:
					{
					setState(163);
					match(LeftParen);
					setState(165);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
						{
						setState(164);
						argumentExpressionList();
						}
					}

					setState(167);
					match(RightParen);
					}
					break;
				case Dot:
					{
					setState(168);
					match(Dot);
					setState(169);
					match(Identifier);
					}
					break;
				case PlusPlus:
				case MinusMinus:
					{
					setState(170);
					_la = _input.LA(1);
					if ( !(_la==PlusPlus || _la==MinusMinus) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(175);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArgumentExpressionListContext extends ParserRuleContext {
		public List<AssignmentExpressionContext> assignmentExpression() {
			return getRuleContexts(AssignmentExpressionContext.class);
		}
		public AssignmentExpressionContext assignmentExpression(int i) {
			return getRuleContext(AssignmentExpressionContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public ArgumentExpressionListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argumentExpressionList; }
	}

	public final ArgumentExpressionListContext argumentExpressionList() throws RecognitionException {
		ArgumentExpressionListContext _localctx = new ArgumentExpressionListContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_argumentExpressionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(176);
			assignmentExpression();
			setState(181);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(177);
				match(Comma);
				setState(178);
				assignmentExpression();
				}
				}
				setState(183);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UnaryExpressionContext extends ParserRuleContext {
		public PostfixExpressionContext postfixExpression() {
			return getRuleContext(PostfixExpressionContext.class,0);
		}
		public UnaryOperatorContext unaryOperator() {
			return getRuleContext(UnaryOperatorContext.class,0);
		}
		public CastExpressionContext castExpression() {
			return getRuleContext(CastExpressionContext.class,0);
		}
		public List<TerminalNode> PlusPlus() { return getTokens(CaplParser.PlusPlus); }
		public TerminalNode PlusPlus(int i) {
			return getToken(CaplParser.PlusPlus, i);
		}
		public List<TerminalNode> MinusMinus() { return getTokens(CaplParser.MinusMinus); }
		public TerminalNode MinusMinus(int i) {
			return getToken(CaplParser.MinusMinus, i);
		}
		public UnaryExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unaryExpression; }
	}

	public final UnaryExpressionContext unaryExpression() throws RecognitionException {
		UnaryExpressionContext _localctx = new UnaryExpressionContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_unaryExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(187);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==PlusPlus || _la==MinusMinus) {
				{
				{
				setState(184);
				_la = _input.LA(1);
				if ( !(_la==PlusPlus || _la==MinusMinus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				}
				setState(189);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(194);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Variables:
			case LeftParen:
			case Identifier:
			case Constant:
			case StringLiteral:
				{
				setState(190);
				postfixExpression();
				}
				break;
			case Plus:
			case Minus:
			case Not:
			case Tilde:
				{
				setState(191);
				unaryOperator();
				setState(192);
				castExpression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UnaryOperatorContext extends ParserRuleContext {
		public TerminalNode Plus() { return getToken(CaplParser.Plus, 0); }
		public TerminalNode Minus() { return getToken(CaplParser.Minus, 0); }
		public TerminalNode Tilde() { return getToken(CaplParser.Tilde, 0); }
		public TerminalNode Not() { return getToken(CaplParser.Not, 0); }
		public UnaryOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unaryOperator; }
	}

	public final UnaryOperatorContext unaryOperator() throws RecognitionException {
		UnaryOperatorContext _localctx = new UnaryOperatorContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_unaryOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(196);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Plus) | (1L << Minus) | (1L << Not) | (1L << Tilde))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CastExpressionContext extends ParserRuleContext {
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public CastExpressionContext castExpression() {
			return getRuleContext(CastExpressionContext.class,0);
		}
		public UnaryExpressionContext unaryExpression() {
			return getRuleContext(UnaryExpressionContext.class,0);
		}
		public TerminalNode DigitSequence() { return getToken(CaplParser.DigitSequence, 0); }
		public CastExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_castExpression; }
	}

	public final CastExpressionContext castExpression() throws RecognitionException {
		CastExpressionContext _localctx = new CastExpressionContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_castExpression);
		try {
			setState(205);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(198);
				match(LeftParen);
				setState(199);
				typeName();
				setState(200);
				match(RightParen);
				setState(201);
				castExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(203);
				unaryExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(204);
				match(DigitSequence);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MultiplicativeExpressionContext extends ParserRuleContext {
		public List<CastExpressionContext> castExpression() {
			return getRuleContexts(CastExpressionContext.class);
		}
		public CastExpressionContext castExpression(int i) {
			return getRuleContext(CastExpressionContext.class,i);
		}
		public List<TerminalNode> Star() { return getTokens(CaplParser.Star); }
		public TerminalNode Star(int i) {
			return getToken(CaplParser.Star, i);
		}
		public List<TerminalNode> Div() { return getTokens(CaplParser.Div); }
		public TerminalNode Div(int i) {
			return getToken(CaplParser.Div, i);
		}
		public List<TerminalNode> Mod() { return getTokens(CaplParser.Mod); }
		public TerminalNode Mod(int i) {
			return getToken(CaplParser.Mod, i);
		}
		public MultiplicativeExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multiplicativeExpression; }
	}

	public final MultiplicativeExpressionContext multiplicativeExpression() throws RecognitionException {
		MultiplicativeExpressionContext _localctx = new MultiplicativeExpressionContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_multiplicativeExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(207);
			castExpression();
			setState(212);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) {
				{
				{
				setState(208);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(209);
				castExpression();
				}
				}
				setState(214);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AdditiveExpressionContext extends ParserRuleContext {
		public List<MultiplicativeExpressionContext> multiplicativeExpression() {
			return getRuleContexts(MultiplicativeExpressionContext.class);
		}
		public MultiplicativeExpressionContext multiplicativeExpression(int i) {
			return getRuleContext(MultiplicativeExpressionContext.class,i);
		}
		public List<TerminalNode> Plus() { return getTokens(CaplParser.Plus); }
		public TerminalNode Plus(int i) {
			return getToken(CaplParser.Plus, i);
		}
		public List<TerminalNode> Minus() { return getTokens(CaplParser.Minus); }
		public TerminalNode Minus(int i) {
			return getToken(CaplParser.Minus, i);
		}
		public AdditiveExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_additiveExpression; }
	}

	public final AdditiveExpressionContext additiveExpression() throws RecognitionException {
		AdditiveExpressionContext _localctx = new AdditiveExpressionContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_additiveExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(215);
			multiplicativeExpression();
			setState(220);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Plus || _la==Minus) {
				{
				{
				setState(216);
				_la = _input.LA(1);
				if ( !(_la==Plus || _la==Minus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(217);
				multiplicativeExpression();
				}
				}
				setState(222);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ShiftExpressionContext extends ParserRuleContext {
		public List<AdditiveExpressionContext> additiveExpression() {
			return getRuleContexts(AdditiveExpressionContext.class);
		}
		public AdditiveExpressionContext additiveExpression(int i) {
			return getRuleContext(AdditiveExpressionContext.class,i);
		}
		public List<TerminalNode> LeftShift() { return getTokens(CaplParser.LeftShift); }
		public TerminalNode LeftShift(int i) {
			return getToken(CaplParser.LeftShift, i);
		}
		public List<TerminalNode> RightShift() { return getTokens(CaplParser.RightShift); }
		public TerminalNode RightShift(int i) {
			return getToken(CaplParser.RightShift, i);
		}
		public ShiftExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_shiftExpression; }
	}

	public final ShiftExpressionContext shiftExpression() throws RecognitionException {
		ShiftExpressionContext _localctx = new ShiftExpressionContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_shiftExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(223);
			additiveExpression();
			setState(228);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==LeftShift || _la==RightShift) {
				{
				{
				setState(224);
				_la = _input.LA(1);
				if ( !(_la==LeftShift || _la==RightShift) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(225);
				additiveExpression();
				}
				}
				setState(230);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RelationalExpressionContext extends ParserRuleContext {
		public List<ShiftExpressionContext> shiftExpression() {
			return getRuleContexts(ShiftExpressionContext.class);
		}
		public ShiftExpressionContext shiftExpression(int i) {
			return getRuleContext(ShiftExpressionContext.class,i);
		}
		public List<TerminalNode> Less() { return getTokens(CaplParser.Less); }
		public TerminalNode Less(int i) {
			return getToken(CaplParser.Less, i);
		}
		public List<TerminalNode> Greater() { return getTokens(CaplParser.Greater); }
		public TerminalNode Greater(int i) {
			return getToken(CaplParser.Greater, i);
		}
		public List<TerminalNode> LessEqual() { return getTokens(CaplParser.LessEqual); }
		public TerminalNode LessEqual(int i) {
			return getToken(CaplParser.LessEqual, i);
		}
		public List<TerminalNode> GreaterEqual() { return getTokens(CaplParser.GreaterEqual); }
		public TerminalNode GreaterEqual(int i) {
			return getToken(CaplParser.GreaterEqual, i);
		}
		public RelationalExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relationalExpression; }
	}

	public final RelationalExpressionContext relationalExpression() throws RecognitionException {
		RelationalExpressionContext _localctx = new RelationalExpressionContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_relationalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(231);
			shiftExpression();
			setState(236);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) {
				{
				{
				setState(232);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(233);
				shiftExpression();
				}
				}
				setState(238);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class EqualityExpressionContext extends ParserRuleContext {
		public List<RelationalExpressionContext> relationalExpression() {
			return getRuleContexts(RelationalExpressionContext.class);
		}
		public RelationalExpressionContext relationalExpression(int i) {
			return getRuleContext(RelationalExpressionContext.class,i);
		}
		public List<TerminalNode> Equal() { return getTokens(CaplParser.Equal); }
		public TerminalNode Equal(int i) {
			return getToken(CaplParser.Equal, i);
		}
		public List<TerminalNode> NotEqual() { return getTokens(CaplParser.NotEqual); }
		public TerminalNode NotEqual(int i) {
			return getToken(CaplParser.NotEqual, i);
		}
		public EqualityExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_equalityExpression; }
	}

	public final EqualityExpressionContext equalityExpression() throws RecognitionException {
		EqualityExpressionContext _localctx = new EqualityExpressionContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_equalityExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(239);
			relationalExpression();
			setState(244);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Equal || _la==NotEqual) {
				{
				{
				setState(240);
				_la = _input.LA(1);
				if ( !(_la==Equal || _la==NotEqual) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(241);
				relationalExpression();
				}
				}
				setState(246);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AndExpressionContext extends ParserRuleContext {
		public List<EqualityExpressionContext> equalityExpression() {
			return getRuleContexts(EqualityExpressionContext.class);
		}
		public EqualityExpressionContext equalityExpression(int i) {
			return getRuleContext(EqualityExpressionContext.class,i);
		}
		public List<TerminalNode> And() { return getTokens(CaplParser.And); }
		public TerminalNode And(int i) {
			return getToken(CaplParser.And, i);
		}
		public AndExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_andExpression; }
	}

	public final AndExpressionContext andExpression() throws RecognitionException {
		AndExpressionContext _localctx = new AndExpressionContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_andExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(247);
			equalityExpression();
			setState(252);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==And) {
				{
				{
				setState(248);
				match(And);
				setState(249);
				equalityExpression();
				}
				}
				setState(254);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExclusiveOrExpressionContext extends ParserRuleContext {
		public List<AndExpressionContext> andExpression() {
			return getRuleContexts(AndExpressionContext.class);
		}
		public AndExpressionContext andExpression(int i) {
			return getRuleContext(AndExpressionContext.class,i);
		}
		public List<TerminalNode> Caret() { return getTokens(CaplParser.Caret); }
		public TerminalNode Caret(int i) {
			return getToken(CaplParser.Caret, i);
		}
		public ExclusiveOrExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exclusiveOrExpression; }
	}

	public final ExclusiveOrExpressionContext exclusiveOrExpression() throws RecognitionException {
		ExclusiveOrExpressionContext _localctx = new ExclusiveOrExpressionContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_exclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(255);
			andExpression();
			setState(260);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Caret) {
				{
				{
				setState(256);
				match(Caret);
				setState(257);
				andExpression();
				}
				}
				setState(262);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InclusiveOrExpressionContext extends ParserRuleContext {
		public List<ExclusiveOrExpressionContext> exclusiveOrExpression() {
			return getRuleContexts(ExclusiveOrExpressionContext.class);
		}
		public ExclusiveOrExpressionContext exclusiveOrExpression(int i) {
			return getRuleContext(ExclusiveOrExpressionContext.class,i);
		}
		public List<TerminalNode> Or() { return getTokens(CaplParser.Or); }
		public TerminalNode Or(int i) {
			return getToken(CaplParser.Or, i);
		}
		public InclusiveOrExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inclusiveOrExpression; }
	}

	public final InclusiveOrExpressionContext inclusiveOrExpression() throws RecognitionException {
		InclusiveOrExpressionContext _localctx = new InclusiveOrExpressionContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_inclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(263);
			exclusiveOrExpression();
			setState(268);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Or) {
				{
				{
				setState(264);
				match(Or);
				setState(265);
				exclusiveOrExpression();
				}
				}
				setState(270);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogicalAndExpressionContext extends ParserRuleContext {
		public List<InclusiveOrExpressionContext> inclusiveOrExpression() {
			return getRuleContexts(InclusiveOrExpressionContext.class);
		}
		public InclusiveOrExpressionContext inclusiveOrExpression(int i) {
			return getRuleContext(InclusiveOrExpressionContext.class,i);
		}
		public List<TerminalNode> AndAnd() { return getTokens(CaplParser.AndAnd); }
		public TerminalNode AndAnd(int i) {
			return getToken(CaplParser.AndAnd, i);
		}
		public LogicalAndExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicalAndExpression; }
	}

	public final LogicalAndExpressionContext logicalAndExpression() throws RecognitionException {
		LogicalAndExpressionContext _localctx = new LogicalAndExpressionContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_logicalAndExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(271);
			inclusiveOrExpression();
			setState(276);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AndAnd) {
				{
				{
				setState(272);
				match(AndAnd);
				setState(273);
				inclusiveOrExpression();
				}
				}
				setState(278);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogicalOrExpressionContext extends ParserRuleContext {
		public List<LogicalAndExpressionContext> logicalAndExpression() {
			return getRuleContexts(LogicalAndExpressionContext.class);
		}
		public LogicalAndExpressionContext logicalAndExpression(int i) {
			return getRuleContext(LogicalAndExpressionContext.class,i);
		}
		public List<TerminalNode> OrOr() { return getTokens(CaplParser.OrOr); }
		public TerminalNode OrOr(int i) {
			return getToken(CaplParser.OrOr, i);
		}
		public LogicalOrExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicalOrExpression; }
	}

	public final LogicalOrExpressionContext logicalOrExpression() throws RecognitionException {
		LogicalOrExpressionContext _localctx = new LogicalOrExpressionContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_logicalOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(279);
			logicalAndExpression();
			setState(284);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OrOr) {
				{
				{
				setState(280);
				match(OrOr);
				setState(281);
				logicalAndExpression();
				}
				}
				setState(286);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConditionalExpressionContext extends ParserRuleContext {
		public LogicalOrExpressionContext logicalOrExpression() {
			return getRuleContext(LogicalOrExpressionContext.class,0);
		}
		public TerminalNode Question() { return getToken(CaplParser.Question, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode Colon() { return getToken(CaplParser.Colon, 0); }
		public ConditionalExpressionContext conditionalExpression() {
			return getRuleContext(ConditionalExpressionContext.class,0);
		}
		public ConditionalExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditionalExpression; }
	}

	public final ConditionalExpressionContext conditionalExpression() throws RecognitionException {
		ConditionalExpressionContext _localctx = new ConditionalExpressionContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_conditionalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(287);
			logicalOrExpression();
			setState(293);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Question) {
				{
				setState(288);
				match(Question);
				setState(289);
				expression();
				setState(290);
				match(Colon);
				setState(291);
				conditionalExpression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentExpressionContext extends ParserRuleContext {
		public ConditionalExpressionContext conditionalExpression() {
			return getRuleContext(ConditionalExpressionContext.class,0);
		}
		public UnaryExpressionContext unaryExpression() {
			return getRuleContext(UnaryExpressionContext.class,0);
		}
		public AssignmentOperatorContext assignmentOperator() {
			return getRuleContext(AssignmentOperatorContext.class,0);
		}
		public AssignmentExpressionContext assignmentExpression() {
			return getRuleContext(AssignmentExpressionContext.class,0);
		}
		public TerminalNode DigitSequence() { return getToken(CaplParser.DigitSequence, 0); }
		public AssignmentExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignmentExpression; }
	}

	public final AssignmentExpressionContext assignmentExpression() throws RecognitionException {
		AssignmentExpressionContext _localctx = new AssignmentExpressionContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_assignmentExpression);
		try {
			setState(301);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(295);
				conditionalExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(296);
				unaryExpression();
				setState(297);
				assignmentOperator();
				setState(298);
				assignmentExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(300);
				match(DigitSequence);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentOperatorContext extends ParserRuleContext {
		public TerminalNode Assign() { return getToken(CaplParser.Assign, 0); }
		public TerminalNode StarAssign() { return getToken(CaplParser.StarAssign, 0); }
		public TerminalNode DivAssign() { return getToken(CaplParser.DivAssign, 0); }
		public TerminalNode ModAssign() { return getToken(CaplParser.ModAssign, 0); }
		public TerminalNode PlusAssign() { return getToken(CaplParser.PlusAssign, 0); }
		public TerminalNode MinusAssign() { return getToken(CaplParser.MinusAssign, 0); }
		public TerminalNode LeftShiftAssign() { return getToken(CaplParser.LeftShiftAssign, 0); }
		public TerminalNode RightShiftAssign() { return getToken(CaplParser.RightShiftAssign, 0); }
		public TerminalNode AndAssign() { return getToken(CaplParser.AndAssign, 0); }
		public TerminalNode XorAssign() { return getToken(CaplParser.XorAssign, 0); }
		public TerminalNode OrAssign() { return getToken(CaplParser.OrAssign, 0); }
		public AssignmentOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignmentOperator; }
	}

	public final AssignmentOperatorContext assignmentOperator() throws RecognitionException {
		AssignmentOperatorContext _localctx = new AssignmentOperatorContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_assignmentOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(303);
			_la = _input.LA(1);
			if ( !(((((_la - 57)) & ~0x3f) == 0 && ((1L << (_la - 57)) & ((1L << (Assign - 57)) | (1L << (StarAssign - 57)) | (1L << (DivAssign - 57)) | (1L << (ModAssign - 57)) | (1L << (PlusAssign - 57)) | (1L << (MinusAssign - 57)) | (1L << (LeftShiftAssign - 57)) | (1L << (RightShiftAssign - 57)) | (1L << (AndAssign - 57)) | (1L << (XorAssign - 57)) | (1L << (OrAssign - 57)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public List<AssignmentExpressionContext> assignmentExpression() {
			return getRuleContexts(AssignmentExpressionContext.class);
		}
		public AssignmentExpressionContext assignmentExpression(int i) {
			return getRuleContext(AssignmentExpressionContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(305);
			assignmentExpression();
			setState(310);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(306);
				match(Comma);
				setState(307);
				assignmentExpression();
				}
				}
				setState(312);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstantExpressionContext extends ParserRuleContext {
		public ConditionalExpressionContext conditionalExpression() {
			return getRuleContext(ConditionalExpressionContext.class,0);
		}
		public ConstantExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constantExpression; }
	}

	public final ConstantExpressionContext constantExpression() throws RecognitionException {
		ConstantExpressionContext _localctx = new ConstantExpressionContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_constantExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(313);
			conditionalExpression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationContext extends ParserRuleContext {
		public DeclarationSpecifiersContext declarationSpecifiers() {
			return getRuleContext(DeclarationSpecifiersContext.class,0);
		}
		public TerminalNode Semi() { return getToken(CaplParser.Semi, 0); }
		public InitDeclaratorListContext initDeclaratorList() {
			return getRuleContext(InitDeclaratorListContext.class,0);
		}
		public DeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declaration; }
	}

	public final DeclarationContext declaration() throws RecognitionException {
		DeclarationContext _localctx = new DeclarationContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(315);
			declarationSpecifiers();
			setState(317);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(316);
				initDeclaratorList();
				}
			}

			setState(319);
			match(Semi);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationSpecifiersContext extends ParserRuleContext {
		public List<DeclarationSpecifierContext> declarationSpecifier() {
			return getRuleContexts(DeclarationSpecifierContext.class);
		}
		public DeclarationSpecifierContext declarationSpecifier(int i) {
			return getRuleContext(DeclarationSpecifierContext.class,i);
		}
		public DeclarationSpecifiersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarationSpecifiers; }
	}

	public final DeclarationSpecifiersContext declarationSpecifiers() throws RecognitionException {
		DeclarationSpecifiersContext _localctx = new DeclarationSpecifiersContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_declarationSpecifiers);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(322); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(321);
				declarationSpecifier();
				}
				}
				setState(324); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationSpecifiers2Context extends ParserRuleContext {
		public List<DeclarationSpecifierContext> declarationSpecifier() {
			return getRuleContexts(DeclarationSpecifierContext.class);
		}
		public DeclarationSpecifierContext declarationSpecifier(int i) {
			return getRuleContext(DeclarationSpecifierContext.class,i);
		}
		public DeclarationSpecifiers2Context(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarationSpecifiers2; }
	}

	public final DeclarationSpecifiers2Context declarationSpecifiers2() throws RecognitionException {
		DeclarationSpecifiers2Context _localctx = new DeclarationSpecifiers2Context(_ctx, getState());
		enterRule(_localctx, 46, RULE_declarationSpecifiers2);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(327); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(326);
				declarationSpecifier();
				}
				}
				setState(329); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationSpecifierContext extends ParserRuleContext {
		public TypeSpecifierContext typeSpecifier() {
			return getRuleContext(TypeSpecifierContext.class,0);
		}
		public DeclarationSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarationSpecifier; }
	}

	public final DeclarationSpecifierContext declarationSpecifier() throws RecognitionException {
		DeclarationSpecifierContext _localctx = new DeclarationSpecifierContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_declarationSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(331);
			typeSpecifier();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InitDeclaratorListContext extends ParserRuleContext {
		public List<InitDeclaratorContext> initDeclarator() {
			return getRuleContexts(InitDeclaratorContext.class);
		}
		public InitDeclaratorContext initDeclarator(int i) {
			return getRuleContext(InitDeclaratorContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public InitDeclaratorListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initDeclaratorList; }
	}

	public final InitDeclaratorListContext initDeclaratorList() throws RecognitionException {
		InitDeclaratorListContext _localctx = new InitDeclaratorListContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_initDeclaratorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(333);
			initDeclarator();
			setState(338);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(334);
				match(Comma);
				setState(335);
				initDeclarator();
				}
				}
				setState(340);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InitDeclaratorContext extends ParserRuleContext {
		public DeclaratorContext declarator() {
			return getRuleContext(DeclaratorContext.class,0);
		}
		public TerminalNode Assign() { return getToken(CaplParser.Assign, 0); }
		public InitializerContext initializer() {
			return getRuleContext(InitializerContext.class,0);
		}
		public InitDeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initDeclarator; }
	}

	public final InitDeclaratorContext initDeclarator() throws RecognitionException {
		InitDeclaratorContext _localctx = new InitDeclaratorContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_initDeclarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(341);
			declarator();
			setState(344);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Assign) {
				{
				setState(342);
				match(Assign);
				setState(343);
				initializer();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeSpecifierContext extends ParserRuleContext {
		public TerminalNode Void() { return getToken(CaplParser.Void, 0); }
		public TerminalNode Char() { return getToken(CaplParser.Char, 0); }
		public TerminalNode Byte() { return getToken(CaplParser.Byte, 0); }
		public TerminalNode Int() { return getToken(CaplParser.Int, 0); }
		public TerminalNode Long() { return getToken(CaplParser.Long, 0); }
		public TerminalNode Int64() { return getToken(CaplParser.Int64, 0); }
		public TerminalNode Float() { return getToken(CaplParser.Float, 0); }
		public TerminalNode Double() { return getToken(CaplParser.Double, 0); }
		public TerminalNode Word() { return getToken(CaplParser.Word, 0); }
		public TerminalNode Dword() { return getToken(CaplParser.Dword, 0); }
		public TerminalNode Timer() { return getToken(CaplParser.Timer, 0); }
		public TerminalNode MsTimer() { return getToken(CaplParser.MsTimer, 0); }
		public MessageTypeContext messageType() {
			return getRuleContext(MessageTypeContext.class,0);
		}
		public TypeSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeSpecifier; }
	}

	public final TypeSpecifierContext typeSpecifier() throws RecognitionException {
		TypeSpecifierContext _localctx = new TypeSpecifierContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_typeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(359);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Void:
				{
				setState(346);
				match(Void);
				}
				break;
			case Char:
				{
				setState(347);
				match(Char);
				}
				break;
			case Byte:
				{
				setState(348);
				match(Byte);
				}
				break;
			case Int:
				{
				setState(349);
				match(Int);
				}
				break;
			case Long:
				{
				setState(350);
				match(Long);
				}
				break;
			case Int64:
				{
				setState(351);
				match(Int64);
				}
				break;
			case Float:
				{
				setState(352);
				match(Float);
				}
				break;
			case Double:
				{
				setState(353);
				match(Double);
				}
				break;
			case Word:
				{
				setState(354);
				match(Word);
				}
				break;
			case Dword:
				{
				setState(355);
				match(Dword);
				}
				break;
			case Timer:
				{
				setState(356);
				match(Timer);
				}
				break;
			case MsTimer:
				{
				setState(357);
				match(MsTimer);
				}
				break;
			case Message:
				{
				setState(358);
				messageType();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MessageTypeContext extends ParserRuleContext {
		public TerminalNode Message() { return getToken(CaplParser.Message, 0); }
		public HexConstMessageContext hexConstMessage() {
			return getRuleContext(HexConstMessageContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public MessageTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_messageType; }
	}

	public final MessageTypeContext messageType() throws RecognitionException {
		MessageTypeContext _localctx = new MessageTypeContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_messageType);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(361);
			match(Message);
			setState(364);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case MessageIdHex:
				{
				setState(362);
				hexConstMessage();
				}
				break;
			case Identifier:
				{
				setState(363);
				match(Identifier);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class HexConstMessageContext extends ParserRuleContext {
		public TerminalNode MessageIdHex() { return getToken(CaplParser.MessageIdHex, 0); }
		public HexConstMessageContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_hexConstMessage; }
	}

	public final HexConstMessageContext hexConstMessage() throws RecognitionException {
		HexConstMessageContext _localctx = new HexConstMessageContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_hexConstMessage);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(366);
			match(MessageIdHex);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SpecifierQualifierListContext extends ParserRuleContext {
		public TypeSpecifierContext typeSpecifier() {
			return getRuleContext(TypeSpecifierContext.class,0);
		}
		public SpecifierQualifierListContext specifierQualifierList() {
			return getRuleContext(SpecifierQualifierListContext.class,0);
		}
		public SpecifierQualifierListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_specifierQualifierList; }
	}

	public final SpecifierQualifierListContext specifierQualifierList() throws RecognitionException {
		SpecifierQualifierListContext _localctx = new SpecifierQualifierListContext(_ctx, getState());
		enterRule(_localctx, 60, RULE_specifierQualifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(368);
			typeSpecifier();
			setState(370);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(369);
				specifierQualifierList();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclaratorContext extends ParserRuleContext {
		public DirectDeclaratorContext directDeclarator() {
			return getRuleContext(DirectDeclaratorContext.class,0);
		}
		public DeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarator; }
	}

	public final DeclaratorContext declarator() throws RecognitionException {
		DeclaratorContext _localctx = new DeclaratorContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_declarator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(372);
			directDeclarator(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DirectDeclaratorContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public DeclaratorContext declarator() {
			return getRuleContext(DeclaratorContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public DirectDeclaratorContext directDeclarator() {
			return getRuleContext(DirectDeclaratorContext.class,0);
		}
		public TerminalNode LeftBracket() { return getToken(CaplParser.LeftBracket, 0); }
		public TerminalNode RightBracket() { return getToken(CaplParser.RightBracket, 0); }
		public AssignmentExpressionContext assignmentExpression() {
			return getRuleContext(AssignmentExpressionContext.class,0);
		}
		public ParameterTypeListContext parameterTypeList() {
			return getRuleContext(ParameterTypeListContext.class,0);
		}
		public IdentifierListContext identifierList() {
			return getRuleContext(IdentifierListContext.class,0);
		}
		public DirectDeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_directDeclarator; }
	}

	public final DirectDeclaratorContext directDeclarator() throws RecognitionException {
		return directDeclarator(0);
	}

	private DirectDeclaratorContext directDeclarator(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		DirectDeclaratorContext _localctx = new DirectDeclaratorContext(_ctx, _parentState);
		DirectDeclaratorContext _prevctx = _localctx;
		int _startState = 64;
		enterRecursionRule(_localctx, 64, RULE_directDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(380);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				{
				setState(375);
				match(Identifier);
				}
				break;
			case LeftParen:
				{
				setState(376);
				match(LeftParen);
				setState(377);
				declarator();
				setState(378);
				match(RightParen);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(401);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,36,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(399);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,35,_ctx) ) {
					case 1:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(382);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(383);
						match(LeftBracket);
						setState(385);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
							{
							setState(384);
							assignmentExpression();
							}
						}

						setState(387);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(388);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(389);
						match(LeftParen);
						setState(390);
						parameterTypeList();
						setState(391);
						match(RightParen);
						}
						break;
					case 3:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(393);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(394);
						match(LeftParen);
						setState(396);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==Identifier) {
							{
							setState(395);
							identifierList();
							}
						}

						setState(398);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(403);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,36,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class NestedParenthesesBlockContext extends ParserRuleContext {
		public List<TerminalNode> LeftParen() { return getTokens(CaplParser.LeftParen); }
		public TerminalNode LeftParen(int i) {
			return getToken(CaplParser.LeftParen, i);
		}
		public List<NestedParenthesesBlockContext> nestedParenthesesBlock() {
			return getRuleContexts(NestedParenthesesBlockContext.class);
		}
		public NestedParenthesesBlockContext nestedParenthesesBlock(int i) {
			return getRuleContext(NestedParenthesesBlockContext.class,i);
		}
		public List<TerminalNode> RightParen() { return getTokens(CaplParser.RightParen); }
		public TerminalNode RightParen(int i) {
			return getToken(CaplParser.RightParen, i);
		}
		public NestedParenthesesBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_nestedParenthesesBlock; }
	}

	public final NestedParenthesesBlockContext nestedParenthesesBlock() throws RecognitionException {
		NestedParenthesesBlockContext _localctx = new NestedParenthesesBlockContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_nestedParenthesesBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(411);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << MessageIdHex) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Colon) | (1L << Semi) | (1L << Comma) | (1L << Assign) | (1L << StarAssign) | (1L << DivAssign) | (1L << ModAssign) | (1L << PlusAssign) | (1L << MinusAssign) | (1L << LeftShiftAssign))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (This - 64)) | (1L << (Dot - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(409);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case MessageIdHex:
				case Variables:
				case Break:
				case Case:
				case Char:
				case Byte:
				case Continue:
				case Default:
				case Do:
				case Double:
				case Else:
				case Float:
				case For:
				case If:
				case Int:
				case Word:
				case Dword:
				case Message:
				case Timer:
				case MsTimer:
				case Long:
				case Int64:
				case Return:
				case Switch:
				case Void:
				case While:
				case LeftBracket:
				case RightBracket:
				case LeftBrace:
				case RightBrace:
				case Less:
				case LessEqual:
				case Greater:
				case GreaterEqual:
				case LeftShift:
				case RightShift:
				case Plus:
				case PlusPlus:
				case Minus:
				case MinusMinus:
				case Star:
				case Div:
				case Mod:
				case And:
				case Or:
				case AndAnd:
				case OrOr:
				case Caret:
				case Not:
				case Tilde:
				case Question:
				case Colon:
				case Semi:
				case Comma:
				case Assign:
				case StarAssign:
				case DivAssign:
				case ModAssign:
				case PlusAssign:
				case MinusAssign:
				case LeftShiftAssign:
				case RightShiftAssign:
				case AndAssign:
				case XorAssign:
				case OrAssign:
				case Equal:
				case NotEqual:
				case Ellipsis:
				case Identifier:
				case This:
				case Dot:
				case Constant:
				case DigitSequence:
				case StringLiteral:
				case Whitespace:
				case Newline:
				case BlockComment:
				case LineComment:
					{
					setState(404);
					_la = _input.LA(1);
					if ( _la <= 0 || (_la==LeftParen || _la==RightParen) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
					break;
				case LeftParen:
					{
					setState(405);
					match(LeftParen);
					setState(406);
					nestedParenthesesBlock();
					setState(407);
					match(RightParen);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(413);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterTypeListContext extends ParserRuleContext {
		public ParameterListContext parameterList() {
			return getRuleContext(ParameterListContext.class,0);
		}
		public TerminalNode Comma() { return getToken(CaplParser.Comma, 0); }
		public TerminalNode Ellipsis() { return getToken(CaplParser.Ellipsis, 0); }
		public ParameterTypeListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterTypeList; }
	}

	public final ParameterTypeListContext parameterTypeList() throws RecognitionException {
		ParameterTypeListContext _localctx = new ParameterTypeListContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_parameterTypeList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(414);
			parameterList();
			setState(417);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Comma) {
				{
				setState(415);
				match(Comma);
				setState(416);
				match(Ellipsis);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterListContext extends ParserRuleContext {
		public List<ParameterDeclarationContext> parameterDeclaration() {
			return getRuleContexts(ParameterDeclarationContext.class);
		}
		public ParameterDeclarationContext parameterDeclaration(int i) {
			return getRuleContext(ParameterDeclarationContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public ParameterListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterList; }
	}

	public final ParameterListContext parameterList() throws RecognitionException {
		ParameterListContext _localctx = new ParameterListContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_parameterList);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(419);
			parameterDeclaration();
			setState(424);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,40,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(420);
					match(Comma);
					setState(421);
					parameterDeclaration();
					}
					} 
				}
				setState(426);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,40,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterDeclarationContext extends ParserRuleContext {
		public DeclarationSpecifiersContext declarationSpecifiers() {
			return getRuleContext(DeclarationSpecifiersContext.class,0);
		}
		public DeclaratorContext declarator() {
			return getRuleContext(DeclaratorContext.class,0);
		}
		public DeclarationSpecifiers2Context declarationSpecifiers2() {
			return getRuleContext(DeclarationSpecifiers2Context.class,0);
		}
		public AbstractDeclaratorContext abstractDeclarator() {
			return getRuleContext(AbstractDeclaratorContext.class,0);
		}
		public ParameterDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterDeclaration; }
	}

	public final ParameterDeclarationContext parameterDeclaration() throws RecognitionException {
		ParameterDeclarationContext _localctx = new ParameterDeclarationContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_parameterDeclaration);
		int _la;
		try {
			setState(434);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,42,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(427);
				declarationSpecifiers();
				setState(428);
				declarator();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(430);
				declarationSpecifiers2();
				setState(432);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LeftParen || _la==LeftBracket) {
					{
					setState(431);
					abstractDeclarator();
					}
				}

				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IdentifierListContext extends ParserRuleContext {
		public List<TerminalNode> Identifier() { return getTokens(CaplParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(CaplParser.Identifier, i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public IdentifierListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifierList; }
	}

	public final IdentifierListContext identifierList() throws RecognitionException {
		IdentifierListContext _localctx = new IdentifierListContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_identifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(436);
			match(Identifier);
			setState(441);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(437);
				match(Comma);
				setState(438);
				match(Identifier);
				}
				}
				setState(443);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeNameContext extends ParserRuleContext {
		public SpecifierQualifierListContext specifierQualifierList() {
			return getRuleContext(SpecifierQualifierListContext.class,0);
		}
		public AbstractDeclaratorContext abstractDeclarator() {
			return getRuleContext(AbstractDeclaratorContext.class,0);
		}
		public TypeNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeName; }
	}

	public final TypeNameContext typeName() throws RecognitionException {
		TypeNameContext _localctx = new TypeNameContext(_ctx, getState());
		enterRule(_localctx, 76, RULE_typeName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(444);
			specifierQualifierList();
			setState(446);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==LeftBracket) {
				{
				setState(445);
				abstractDeclarator();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AbstractDeclaratorContext extends ParserRuleContext {
		public DirectAbstractDeclaratorContext directAbstractDeclarator() {
			return getRuleContext(DirectAbstractDeclaratorContext.class,0);
		}
		public AbstractDeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_abstractDeclarator; }
	}

	public final AbstractDeclaratorContext abstractDeclarator() throws RecognitionException {
		AbstractDeclaratorContext _localctx = new AbstractDeclaratorContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_abstractDeclarator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(448);
			directAbstractDeclarator(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DirectAbstractDeclaratorContext extends ParserRuleContext {
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public AbstractDeclaratorContext abstractDeclarator() {
			return getRuleContext(AbstractDeclaratorContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public TerminalNode LeftBracket() { return getToken(CaplParser.LeftBracket, 0); }
		public TerminalNode RightBracket() { return getToken(CaplParser.RightBracket, 0); }
		public AssignmentExpressionContext assignmentExpression() {
			return getRuleContext(AssignmentExpressionContext.class,0);
		}
		public TerminalNode Star() { return getToken(CaplParser.Star, 0); }
		public ParameterTypeListContext parameterTypeList() {
			return getRuleContext(ParameterTypeListContext.class,0);
		}
		public DirectAbstractDeclaratorContext directAbstractDeclarator() {
			return getRuleContext(DirectAbstractDeclaratorContext.class,0);
		}
		public DirectAbstractDeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_directAbstractDeclarator; }
	}

	public final DirectAbstractDeclaratorContext directAbstractDeclarator() throws RecognitionException {
		return directAbstractDeclarator(0);
	}

	private DirectAbstractDeclaratorContext directAbstractDeclarator(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		DirectAbstractDeclaratorContext _localctx = new DirectAbstractDeclaratorContext(_ctx, _parentState);
		DirectAbstractDeclaratorContext _prevctx = _localctx;
		int _startState = 80;
		enterRecursionRule(_localctx, 80, RULE_directAbstractDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(468);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,47,_ctx) ) {
			case 1:
				{
				setState(451);
				match(LeftParen);
				setState(452);
				abstractDeclarator();
				setState(453);
				match(RightParen);
				}
				break;
			case 2:
				{
				setState(455);
				match(LeftBracket);
				setState(457);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
					{
					setState(456);
					assignmentExpression();
					}
				}

				setState(459);
				match(RightBracket);
				}
				break;
			case 3:
				{
				setState(460);
				match(LeftBracket);
				setState(461);
				match(Star);
				setState(462);
				match(RightBracket);
				}
				break;
			case 4:
				{
				setState(463);
				match(LeftParen);
				setState(465);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
					{
					setState(464);
					parameterTypeList();
					}
				}

				setState(467);
				match(RightParen);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(488);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,51,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(486);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,50,_ctx) ) {
					case 1:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(470);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(471);
						match(LeftBracket);
						setState(473);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
							{
							setState(472);
							assignmentExpression();
							}
						}

						setState(475);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(476);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(477);
						match(LeftBracket);
						setState(478);
						match(Star);
						setState(479);
						match(RightBracket);
						}
						break;
					case 3:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(480);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(481);
						match(LeftParen);
						setState(483);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
							{
							setState(482);
							parameterTypeList();
							}
						}

						setState(485);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(490);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,51,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class InitializerContext extends ParserRuleContext {
		public AssignmentExpressionContext assignmentExpression() {
			return getRuleContext(AssignmentExpressionContext.class,0);
		}
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public InitializerListContext initializerList() {
			return getRuleContext(InitializerListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public TerminalNode Comma() { return getToken(CaplParser.Comma, 0); }
		public InitializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initializer; }
	}

	public final InitializerContext initializer() throws RecognitionException {
		InitializerContext _localctx = new InitializerContext(_ctx, getState());
		enterRule(_localctx, 82, RULE_initializer);
		int _la;
		try {
			setState(499);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Variables:
			case LeftParen:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case Not:
			case Tilde:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(491);
				assignmentExpression();
				}
				break;
			case LeftBrace:
				enterOuterAlt(_localctx, 2);
				{
				setState(492);
				match(LeftBrace);
				setState(493);
				initializerList();
				setState(495);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(494);
					match(Comma);
					}
				}

				setState(497);
				match(RightBrace);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InitializerListContext extends ParserRuleContext {
		public List<InitializerContext> initializer() {
			return getRuleContexts(InitializerContext.class);
		}
		public InitializerContext initializer(int i) {
			return getRuleContext(InitializerContext.class,i);
		}
		public List<DesignationContext> designation() {
			return getRuleContexts(DesignationContext.class);
		}
		public DesignationContext designation(int i) {
			return getRuleContext(DesignationContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public InitializerListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initializerList; }
	}

	public final InitializerListContext initializerList() throws RecognitionException {
		InitializerListContext _localctx = new InitializerListContext(_ctx, getState());
		enterRule(_localctx, 84, RULE_initializerList);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(502);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftBracket) {
				{
				setState(501);
				designation();
				}
			}

			setState(504);
			initializer();
			setState(512);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(505);
					match(Comma);
					setState(507);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==LeftBracket) {
						{
						setState(506);
						designation();
						}
					}

					setState(509);
					initializer();
					}
					} 
				}
				setState(514);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DesignationContext extends ParserRuleContext {
		public DesignatorListContext designatorList() {
			return getRuleContext(DesignatorListContext.class,0);
		}
		public TerminalNode Assign() { return getToken(CaplParser.Assign, 0); }
		public DesignationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_designation; }
	}

	public final DesignationContext designation() throws RecognitionException {
		DesignationContext _localctx = new DesignationContext(_ctx, getState());
		enterRule(_localctx, 86, RULE_designation);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(515);
			designatorList();
			setState(516);
			match(Assign);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DesignatorListContext extends ParserRuleContext {
		public List<DesignatorContext> designator() {
			return getRuleContexts(DesignatorContext.class);
		}
		public DesignatorContext designator(int i) {
			return getRuleContext(DesignatorContext.class,i);
		}
		public DesignatorListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_designatorList; }
	}

	public final DesignatorListContext designatorList() throws RecognitionException {
		DesignatorListContext _localctx = new DesignatorListContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_designatorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(519); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(518);
				designator();
				}
				}
				setState(521); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==LeftBracket );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DesignatorContext extends ParserRuleContext {
		public TerminalNode LeftBracket() { return getToken(CaplParser.LeftBracket, 0); }
		public ConstantExpressionContext constantExpression() {
			return getRuleContext(ConstantExpressionContext.class,0);
		}
		public TerminalNode RightBracket() { return getToken(CaplParser.RightBracket, 0); }
		public DesignatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_designator; }
	}

	public final DesignatorContext designator() throws RecognitionException {
		DesignatorContext _localctx = new DesignatorContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_designator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(523);
			match(LeftBracket);
			setState(524);
			constantExpression();
			setState(525);
			match(RightBracket);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public LabeledStatementContext labeledStatement() {
			return getRuleContext(LabeledStatementContext.class,0);
		}
		public CompoundStatementContext compoundStatement() {
			return getRuleContext(CompoundStatementContext.class,0);
		}
		public ExpressionStatementContext expressionStatement() {
			return getRuleContext(ExpressionStatementContext.class,0);
		}
		public SelectionStatementContext selectionStatement() {
			return getRuleContext(SelectionStatementContext.class,0);
		}
		public IterationStatementContext iterationStatement() {
			return getRuleContext(IterationStatementContext.class,0);
		}
		public JumpStatementContext jumpStatement() {
			return getRuleContext(JumpStatementContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 92, RULE_statement);
		try {
			setState(533);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,58,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(527);
				labeledStatement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(528);
				compoundStatement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(529);
				expressionStatement();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(530);
				selectionStatement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(531);
				iterationStatement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(532);
				jumpStatement();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LabeledStatementContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode Colon() { return getToken(CaplParser.Colon, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode Case() { return getToken(CaplParser.Case, 0); }
		public ConstantExpressionContext constantExpression() {
			return getRuleContext(ConstantExpressionContext.class,0);
		}
		public TerminalNode Default() { return getToken(CaplParser.Default, 0); }
		public LabeledStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_labeledStatement; }
	}

	public final LabeledStatementContext labeledStatement() throws RecognitionException {
		LabeledStatementContext _localctx = new LabeledStatementContext(_ctx, getState());
		enterRule(_localctx, 94, RULE_labeledStatement);
		try {
			setState(546);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				enterOuterAlt(_localctx, 1);
				{
				setState(535);
				match(Identifier);
				setState(536);
				match(Colon);
				setState(537);
				statement();
				}
				break;
			case Case:
				enterOuterAlt(_localctx, 2);
				{
				setState(538);
				match(Case);
				setState(539);
				constantExpression();
				setState(540);
				match(Colon);
				setState(541);
				statement();
				}
				break;
			case Default:
				enterOuterAlt(_localctx, 3);
				{
				setState(543);
				match(Default);
				setState(544);
				match(Colon);
				setState(545);
				statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CompoundStatementContext extends ParserRuleContext {
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public CompoundStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compoundStatement; }
	}

	public final CompoundStatementContext compoundStatement() throws RecognitionException {
		CompoundStatementContext _localctx = new CompoundStatementContext(_ctx, getState());
		enterRule(_localctx, 96, RULE_compoundStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(548);
			match(LeftBrace);
			setState(550);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde) | (1L << Semi))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
				{
				setState(549);
				blockItemList();
				}
			}

			setState(552);
			match(RightBrace);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BlockItemListContext extends ParserRuleContext {
		public List<BlockItemContext> blockItem() {
			return getRuleContexts(BlockItemContext.class);
		}
		public BlockItemContext blockItem(int i) {
			return getRuleContext(BlockItemContext.class,i);
		}
		public BlockItemListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_blockItemList; }
	}

	public final BlockItemListContext blockItemList() throws RecognitionException {
		BlockItemListContext _localctx = new BlockItemListContext(_ctx, getState());
		enterRule(_localctx, 98, RULE_blockItemList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(555); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(554);
				blockItem();
				}
				}
				setState(557); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde) | (1L << Semi))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BlockItemContext extends ParserRuleContext {
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public DeclarationContext declaration() {
			return getRuleContext(DeclarationContext.class,0);
		}
		public BlockItemContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_blockItem; }
	}

	public final BlockItemContext blockItem() throws RecognitionException {
		BlockItemContext _localctx = new BlockItemContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_blockItem);
		try {
			setState(561);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Variables:
			case Break:
			case Case:
			case Continue:
			case Default:
			case Do:
			case For:
			case If:
			case Return:
			case Switch:
			case While:
			case LeftParen:
			case LeftBrace:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case Not:
			case Tilde:
			case Semi:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(559);
				statement();
				}
				break;
			case Char:
			case Byte:
			case Double:
			case Float:
			case Int:
			case Word:
			case Dword:
			case Message:
			case Timer:
			case MsTimer:
			case Long:
			case Int64:
			case Void:
				enterOuterAlt(_localctx, 2);
				{
				setState(560);
				declaration();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionStatementContext extends ParserRuleContext {
		public TerminalNode Semi() { return getToken(CaplParser.Semi, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExpressionStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expressionStatement; }
	}

	public final ExpressionStatementContext expressionStatement() throws RecognitionException {
		ExpressionStatementContext _localctx = new ExpressionStatementContext(_ctx, getState());
		enterRule(_localctx, 102, RULE_expressionStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(564);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
				{
				setState(563);
				expression();
				}
			}

			setState(566);
			match(Semi);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SelectionStatementContext extends ParserRuleContext {
		public TerminalNode If() { return getToken(CaplParser.If, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public TerminalNode Else() { return getToken(CaplParser.Else, 0); }
		public TerminalNode Switch() { return getToken(CaplParser.Switch, 0); }
		public SelectionStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_selectionStatement; }
	}

	public final SelectionStatementContext selectionStatement() throws RecognitionException {
		SelectionStatementContext _localctx = new SelectionStatementContext(_ctx, getState());
		enterRule(_localctx, 104, RULE_selectionStatement);
		try {
			setState(583);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case If:
				enterOuterAlt(_localctx, 1);
				{
				setState(568);
				match(If);
				setState(569);
				match(LeftParen);
				setState(570);
				expression();
				setState(571);
				match(RightParen);
				setState(572);
				statement();
				setState(575);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,64,_ctx) ) {
				case 1:
					{
					setState(573);
					match(Else);
					setState(574);
					statement();
					}
					break;
				}
				}
				break;
			case Switch:
				enterOuterAlt(_localctx, 2);
				{
				setState(577);
				match(Switch);
				setState(578);
				match(LeftParen);
				setState(579);
				expression();
				setState(580);
				match(RightParen);
				setState(581);
				statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IterationStatementContext extends ParserRuleContext {
		public TerminalNode While() { return getToken(CaplParser.While, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode Do() { return getToken(CaplParser.Do, 0); }
		public TerminalNode Semi() { return getToken(CaplParser.Semi, 0); }
		public TerminalNode For() { return getToken(CaplParser.For, 0); }
		public ForConditionContext forCondition() {
			return getRuleContext(ForConditionContext.class,0);
		}
		public IterationStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_iterationStatement; }
	}

	public final IterationStatementContext iterationStatement() throws RecognitionException {
		IterationStatementContext _localctx = new IterationStatementContext(_ctx, getState());
		enterRule(_localctx, 106, RULE_iterationStatement);
		try {
			setState(605);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case While:
				enterOuterAlt(_localctx, 1);
				{
				setState(585);
				match(While);
				setState(586);
				match(LeftParen);
				setState(587);
				expression();
				setState(588);
				match(RightParen);
				setState(589);
				statement();
				}
				break;
			case Do:
				enterOuterAlt(_localctx, 2);
				{
				setState(591);
				match(Do);
				setState(592);
				statement();
				setState(593);
				match(While);
				setState(594);
				match(LeftParen);
				setState(595);
				expression();
				setState(596);
				match(RightParen);
				setState(597);
				match(Semi);
				}
				break;
			case For:
				enterOuterAlt(_localctx, 3);
				{
				setState(599);
				match(For);
				setState(600);
				match(LeftParen);
				setState(601);
				forCondition();
				setState(602);
				match(RightParen);
				setState(603);
				statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForConditionContext extends ParserRuleContext {
		public List<TerminalNode> Semi() { return getTokens(CaplParser.Semi); }
		public TerminalNode Semi(int i) {
			return getToken(CaplParser.Semi, i);
		}
		public ForDeclarationContext forDeclaration() {
			return getRuleContext(ForDeclarationContext.class,0);
		}
		public List<ForExpressionContext> forExpression() {
			return getRuleContexts(ForExpressionContext.class);
		}
		public ForExpressionContext forExpression(int i) {
			return getRuleContext(ForExpressionContext.class,i);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ForConditionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forCondition; }
	}

	public final ForConditionContext forCondition() throws RecognitionException {
		ForConditionContext _localctx = new ForConditionContext(_ctx, getState());
		enterRule(_localctx, 108, RULE_forCondition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(611);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Char:
			case Byte:
			case Double:
			case Float:
			case Int:
			case Word:
			case Dword:
			case Message:
			case Timer:
			case MsTimer:
			case Long:
			case Int64:
			case Void:
				{
				setState(607);
				forDeclaration();
				}
				break;
			case Variables:
			case LeftParen:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case Not:
			case Tilde:
			case Semi:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				{
				setState(609);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
					{
					setState(608);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(613);
			match(Semi);
			setState(615);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
				{
				setState(614);
				forExpression();
				}
			}

			setState(617);
			match(Semi);
			setState(619);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
				{
				setState(618);
				forExpression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForDeclarationContext extends ParserRuleContext {
		public DeclarationSpecifiersContext declarationSpecifiers() {
			return getRuleContext(DeclarationSpecifiersContext.class,0);
		}
		public InitDeclaratorListContext initDeclaratorList() {
			return getRuleContext(InitDeclaratorListContext.class,0);
		}
		public ForDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forDeclaration; }
	}

	public final ForDeclarationContext forDeclaration() throws RecognitionException {
		ForDeclarationContext _localctx = new ForDeclarationContext(_ctx, getState());
		enterRule(_localctx, 110, RULE_forDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(621);
			declarationSpecifiers();
			setState(623);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(622);
				initDeclaratorList();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForExpressionContext extends ParserRuleContext {
		public List<AssignmentExpressionContext> assignmentExpression() {
			return getRuleContexts(AssignmentExpressionContext.class);
		}
		public AssignmentExpressionContext assignmentExpression(int i) {
			return getRuleContext(AssignmentExpressionContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public ForExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forExpression; }
	}

	public final ForExpressionContext forExpression() throws RecognitionException {
		ForExpressionContext _localctx = new ForExpressionContext(_ctx, getState());
		enterRule(_localctx, 112, RULE_forExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(625);
			assignmentExpression();
			setState(630);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(626);
				match(Comma);
				setState(627);
				assignmentExpression();
				}
				}
				setState(632);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class JumpStatementContext extends ParserRuleContext {
		public TerminalNode Semi() { return getToken(CaplParser.Semi, 0); }
		public TerminalNode Return() { return getToken(CaplParser.Return, 0); }
		public TerminalNode Continue() { return getToken(CaplParser.Continue, 0); }
		public TerminalNode Break() { return getToken(CaplParser.Break, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public JumpStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_jumpStatement; }
	}

	public final JumpStatementContext jumpStatement() throws RecognitionException {
		JumpStatementContext _localctx = new JumpStatementContext(_ctx, getState());
		enterRule(_localctx, 114, RULE_jumpStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(638);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Break:
			case Continue:
				{
				setState(633);
				_la = _input.LA(1);
				if ( !(_la==Break || _la==Continue) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case Return:
				{
				setState(634);
				match(Return);
				setState(636);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
					{
					setState(635);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(640);
			match(Semi);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CompilationUnitContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(CaplParser.EOF, 0); }
		public TranslationUnitContext translationUnit() {
			return getRuleContext(TranslationUnitContext.class,0);
		}
		public CompilationUnitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compilationUnit; }
	}

	public final CompilationUnitContext compilationUnit() throws RecognitionException {
		CompilationUnitContext _localctx = new CompilationUnitContext(_ctx, getState());
		enterRule(_localctx, 116, RULE_compilationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(643);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << LeftParen) | (1L << Semi))) != 0) || _la==Identifier) {
				{
				setState(642);
				translationUnit();
				}
			}

			setState(645);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TranslationUnitContext extends ParserRuleContext {
		public List<ExternalDeclarationContext> externalDeclaration() {
			return getRuleContexts(ExternalDeclarationContext.class);
		}
		public ExternalDeclarationContext externalDeclaration(int i) {
			return getRuleContext(ExternalDeclarationContext.class,i);
		}
		public TranslationUnitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_translationUnit; }
	}

	public final TranslationUnitContext translationUnit() throws RecognitionException {
		TranslationUnitContext _localctx = new TranslationUnitContext(_ctx, getState());
		enterRule(_localctx, 118, RULE_translationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(648); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(647);
				externalDeclaration();
				}
				}
				setState(650); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << LeftParen) | (1L << Semi))) != 0) || _la==Identifier );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExternalDeclarationContext extends ParserRuleContext {
		public FunctionDefinitionContext functionDefinition() {
			return getRuleContext(FunctionDefinitionContext.class,0);
		}
		public DeclarationContext declaration() {
			return getRuleContext(DeclarationContext.class,0);
		}
		public TerminalNode Semi() { return getToken(CaplParser.Semi, 0); }
		public ExternalDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_externalDeclaration; }
	}

	public final ExternalDeclarationContext externalDeclaration() throws RecognitionException {
		ExternalDeclarationContext _localctx = new ExternalDeclarationContext(_ctx, getState());
		enterRule(_localctx, 120, RULE_externalDeclaration);
		try {
			setState(655);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,77,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(652);
				functionDefinition();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(653);
				declaration();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(654);
				match(Semi);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionDefinitionContext extends ParserRuleContext {
		public DeclaratorContext declarator() {
			return getRuleContext(DeclaratorContext.class,0);
		}
		public CompoundStatementContext compoundStatement() {
			return getRuleContext(CompoundStatementContext.class,0);
		}
		public DeclarationSpecifiersContext declarationSpecifiers() {
			return getRuleContext(DeclarationSpecifiersContext.class,0);
		}
		public DeclarationListContext declarationList() {
			return getRuleContext(DeclarationListContext.class,0);
		}
		public FunctionDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionDefinition; }
	}

	public final FunctionDefinitionContext functionDefinition() throws RecognitionException {
		FunctionDefinitionContext _localctx = new FunctionDefinitionContext(_ctx, getState());
		enterRule(_localctx, 122, RULE_functionDefinition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(658);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(657);
				declarationSpecifiers();
				}
			}

			setState(660);
			declarator();
			setState(662);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(661);
				declarationList();
				}
			}

			setState(664);
			compoundStatement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationListContext extends ParserRuleContext {
		public List<DeclarationContext> declaration() {
			return getRuleContexts(DeclarationContext.class);
		}
		public DeclarationContext declaration(int i) {
			return getRuleContext(DeclarationContext.class,i);
		}
		public DeclarationListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarationList; }
	}

	public final DeclarationListContext declarationList() throws RecognitionException {
		DeclarationListContext _localctx = new DeclarationListContext(_ctx, getState());
		enterRule(_localctx, 124, RULE_declarationList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(667); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(666);
				declaration();
				}
				}
				setState(669); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VariableDeclarationBlockContext extends ParserRuleContext {
		public TerminalNode Variables() { return getToken(CaplParser.Variables, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public VariableDeclarationBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variableDeclarationBlock; }
	}

	public final VariableDeclarationBlockContext variableDeclarationBlock() throws RecognitionException {
		VariableDeclarationBlockContext _localctx = new VariableDeclarationBlockContext(_ctx, getState());
		enterRule(_localctx, 126, RULE_variableDeclarationBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(671);
			match(Variables);
			setState(672);
			match(LeftBrace);
			setState(674);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Not) | (1L << Tilde) | (1L << Semi))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (Identifier - 71)) | (1L << (Constant - 71)) | (1L << (DigitSequence - 71)) | (1L << (StringLiteral - 71)))) != 0)) {
				{
				setState(673);
				blockItemList();
				}
			}

			setState(676);
			match(RightBrace);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 32:
			return directDeclarator_sempred((DirectDeclaratorContext)_localctx, predIndex);
		case 40:
			return directAbstractDeclarator_sempred((DirectAbstractDeclaratorContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean directDeclarator_sempred(DirectDeclaratorContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 3);
		case 1:
			return precpred(_ctx, 2);
		case 2:
			return precpred(_ctx, 1);
		}
		return true;
	}
	private boolean directAbstractDeclarator_sempred(DirectAbstractDeclaratorContext _localctx, int predIndex) {
		switch (predIndex) {
		case 3:
			return precpred(_ctx, 3);
		case 4:
			return precpred(_ctx, 2);
		case 5:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3R\u02a9\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\3\2\3\2\3\2\6\2\u0086\n\2\r\2\16\2\u0087\3\2"+
		"\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\5\2\u0093\n\2\3\3\3\3\3\3\3\3\3\3\3\3"+
		"\3\3\5\3\u009c\n\3\3\3\3\3\5\3\u00a0\n\3\3\3\3\3\3\3\3\3\3\3\3\3\5\3\u00a8"+
		"\n\3\3\3\3\3\3\3\3\3\7\3\u00ae\n\3\f\3\16\3\u00b1\13\3\3\4\3\4\3\4\7\4"+
		"\u00b6\n\4\f\4\16\4\u00b9\13\4\3\5\7\5\u00bc\n\5\f\5\16\5\u00bf\13\5\3"+
		"\5\3\5\3\5\3\5\5\5\u00c5\n\5\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7\u00d0"+
		"\n\7\3\b\3\b\3\b\7\b\u00d5\n\b\f\b\16\b\u00d8\13\b\3\t\3\t\3\t\7\t\u00dd"+
		"\n\t\f\t\16\t\u00e0\13\t\3\n\3\n\3\n\7\n\u00e5\n\n\f\n\16\n\u00e8\13\n"+
		"\3\13\3\13\3\13\7\13\u00ed\n\13\f\13\16\13\u00f0\13\13\3\f\3\f\3\f\7\f"+
		"\u00f5\n\f\f\f\16\f\u00f8\13\f\3\r\3\r\3\r\7\r\u00fd\n\r\f\r\16\r\u0100"+
		"\13\r\3\16\3\16\3\16\7\16\u0105\n\16\f\16\16\16\u0108\13\16\3\17\3\17"+
		"\3\17\7\17\u010d\n\17\f\17\16\17\u0110\13\17\3\20\3\20\3\20\7\20\u0115"+
		"\n\20\f\20\16\20\u0118\13\20\3\21\3\21\3\21\7\21\u011d\n\21\f\21\16\21"+
		"\u0120\13\21\3\22\3\22\3\22\3\22\3\22\3\22\5\22\u0128\n\22\3\23\3\23\3"+
		"\23\3\23\3\23\3\23\5\23\u0130\n\23\3\24\3\24\3\25\3\25\3\25\7\25\u0137"+
		"\n\25\f\25\16\25\u013a\13\25\3\26\3\26\3\27\3\27\5\27\u0140\n\27\3\27"+
		"\3\27\3\30\6\30\u0145\n\30\r\30\16\30\u0146\3\31\6\31\u014a\n\31\r\31"+
		"\16\31\u014b\3\32\3\32\3\33\3\33\3\33\7\33\u0153\n\33\f\33\16\33\u0156"+
		"\13\33\3\34\3\34\3\34\5\34\u015b\n\34\3\35\3\35\3\35\3\35\3\35\3\35\3"+
		"\35\3\35\3\35\3\35\3\35\3\35\3\35\5\35\u016a\n\35\3\36\3\36\3\36\5\36"+
		"\u016f\n\36\3\37\3\37\3 \3 \5 \u0175\n \3!\3!\3\"\3\"\3\"\3\"\3\"\3\""+
		"\5\"\u017f\n\"\3\"\3\"\3\"\5\"\u0184\n\"\3\"\3\"\3\"\3\"\3\"\3\"\3\"\3"+
		"\"\3\"\5\"\u018f\n\"\3\"\7\"\u0192\n\"\f\"\16\"\u0195\13\"\3#\3#\3#\3"+
		"#\3#\7#\u019c\n#\f#\16#\u019f\13#\3$\3$\3$\5$\u01a4\n$\3%\3%\3%\7%\u01a9"+
		"\n%\f%\16%\u01ac\13%\3&\3&\3&\3&\3&\5&\u01b3\n&\5&\u01b5\n&\3\'\3\'\3"+
		"\'\7\'\u01ba\n\'\f\'\16\'\u01bd\13\'\3(\3(\5(\u01c1\n(\3)\3)\3*\3*\3*"+
		"\3*\3*\3*\3*\5*\u01cc\n*\3*\3*\3*\3*\3*\3*\5*\u01d4\n*\3*\5*\u01d7\n*"+
		"\3*\3*\3*\5*\u01dc\n*\3*\3*\3*\3*\3*\3*\3*\3*\5*\u01e6\n*\3*\7*\u01e9"+
		"\n*\f*\16*\u01ec\13*\3+\3+\3+\3+\5+\u01f2\n+\3+\3+\5+\u01f6\n+\3,\5,\u01f9"+
		"\n,\3,\3,\3,\5,\u01fe\n,\3,\7,\u0201\n,\f,\16,\u0204\13,\3-\3-\3-\3.\6"+
		".\u020a\n.\r.\16.\u020b\3/\3/\3/\3/\3\60\3\60\3\60\3\60\3\60\3\60\5\60"+
		"\u0218\n\60\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\5\61"+
		"\u0225\n\61\3\62\3\62\5\62\u0229\n\62\3\62\3\62\3\63\6\63\u022e\n\63\r"+
		"\63\16\63\u022f\3\64\3\64\5\64\u0234\n\64\3\65\5\65\u0237\n\65\3\65\3"+
		"\65\3\66\3\66\3\66\3\66\3\66\3\66\3\66\5\66\u0242\n\66\3\66\3\66\3\66"+
		"\3\66\3\66\3\66\5\66\u024a\n\66\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67"+
		"\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67\5\67\u0260"+
		"\n\67\38\38\58\u0264\n8\58\u0266\n8\38\38\58\u026a\n8\38\38\58\u026e\n"+
		"8\39\39\59\u0272\n9\3:\3:\3:\7:\u0277\n:\f:\16:\u027a\13:\3;\3;\3;\5;"+
		"\u027f\n;\5;\u0281\n;\3;\3;\3<\5<\u0286\n<\3<\3<\3=\6=\u028b\n=\r=\16"+
		"=\u028c\3>\3>\3>\5>\u0292\n>\3?\5?\u0295\n?\3?\3?\5?\u0299\n?\3?\3?\3"+
		"@\6@\u029e\n@\r@\16@\u029f\3A\3A\3A\5A\u02a5\nA\3A\3A\3A\2\4BRB\2\4\6"+
		"\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\64\668:<>@BDFHJLNPRT"+
		"VXZ\\^`bdfhjlnprtvxz|~\u0080\2\f\4\2**,,\5\2))++\65\66\3\2-/\4\2))++\3"+
		"\2\'(\3\2#&\3\2FG\3\2;E\3\2\35\36\4\2\5\5\t\t\2\u02d8\2\u0092\3\2\2\2"+
		"\4\u009f\3\2\2\2\6\u00b2\3\2\2\2\b\u00bd\3\2\2\2\n\u00c6\3\2\2\2\f\u00cf"+
		"\3\2\2\2\16\u00d1\3\2\2\2\20\u00d9\3\2\2\2\22\u00e1\3\2\2\2\24\u00e9\3"+
		"\2\2\2\26\u00f1\3\2\2\2\30\u00f9\3\2\2\2\32\u0101\3\2\2\2\34\u0109\3\2"+
		"\2\2\36\u0111\3\2\2\2 \u0119\3\2\2\2\"\u0121\3\2\2\2$\u012f\3\2\2\2&\u0131"+
		"\3\2\2\2(\u0133\3\2\2\2*\u013b\3\2\2\2,\u013d\3\2\2\2.\u0144\3\2\2\2\60"+
		"\u0149\3\2\2\2\62\u014d\3\2\2\2\64\u014f\3\2\2\2\66\u0157\3\2\2\28\u0169"+
		"\3\2\2\2:\u016b\3\2\2\2<\u0170\3\2\2\2>\u0172\3\2\2\2@\u0176\3\2\2\2B"+
		"\u017e\3\2\2\2D\u019d\3\2\2\2F\u01a0\3\2\2\2H\u01a5\3\2\2\2J\u01b4\3\2"+
		"\2\2L\u01b6\3\2\2\2N\u01be\3\2\2\2P\u01c2\3\2\2\2R\u01d6\3\2\2\2T\u01f5"+
		"\3\2\2\2V\u01f8\3\2\2\2X\u0205\3\2\2\2Z\u0209\3\2\2\2\\\u020d\3\2\2\2"+
		"^\u0217\3\2\2\2`\u0224\3\2\2\2b\u0226\3\2\2\2d\u022d\3\2\2\2f\u0233\3"+
		"\2\2\2h\u0236\3\2\2\2j\u0249\3\2\2\2l\u025f\3\2\2\2n\u0265\3\2\2\2p\u026f"+
		"\3\2\2\2r\u0273\3\2\2\2t\u0280\3\2\2\2v\u0285\3\2\2\2x\u028a\3\2\2\2z"+
		"\u0291\3\2\2\2|\u0294\3\2\2\2~\u029d\3\2\2\2\u0080\u02a1\3\2\2\2\u0082"+
		"\u0093\7I\2\2\u0083\u0093\7L\2\2\u0084\u0086\7N\2\2\u0085\u0084\3\2\2"+
		"\2\u0086\u0087\3\2\2\2\u0087\u0085\3\2\2\2\u0087\u0088\3\2\2\2\u0088\u0093"+
		"\3\2\2\2\u0089\u008a\7\35\2\2\u008a\u008b\5(\25\2\u008b\u008c\7\36\2\2"+
		"\u008c\u0093\3\2\2\2\u008d\u008e\7\35\2\2\u008e\u008f\5b\62\2\u008f\u0090"+
		"\7\36\2\2\u0090\u0093\3\2\2\2\u0091\u0093\5\u0080A\2\u0092\u0082\3\2\2"+
		"\2\u0092\u0083\3\2\2\2\u0092\u0085\3\2\2\2\u0092\u0089\3\2\2\2\u0092\u008d"+
		"\3\2\2\2\u0092\u0091\3\2\2\2\u0093\3\3\2\2\2\u0094\u00a0\5\2\2\2\u0095"+
		"\u0096\7\35\2\2\u0096\u0097\5N(\2\u0097\u0098\7\36\2\2\u0098\u0099\7!"+
		"\2\2\u0099\u009b\5V,\2\u009a\u009c\7:\2\2\u009b\u009a\3\2\2\2\u009b\u009c"+
		"\3\2\2\2\u009c\u009d\3\2\2\2\u009d\u009e\7\"\2\2\u009e\u00a0\3\2\2\2\u009f"+
		"\u0094\3\2\2\2\u009f\u0095\3\2\2\2\u00a0\u00af\3\2\2\2\u00a1\u00a2\7\37"+
		"\2\2\u00a2\u00a3\5(\25\2\u00a3\u00a4\7 \2\2\u00a4\u00ae\3\2\2\2\u00a5"+
		"\u00a7\7\35\2\2\u00a6\u00a8\5\6\4\2\u00a7\u00a6\3\2\2\2\u00a7\u00a8\3"+
		"\2\2\2\u00a8\u00a9\3\2\2\2\u00a9\u00ae\7\36\2\2\u00aa\u00ab\7K\2\2\u00ab"+
		"\u00ae\7I\2\2\u00ac\u00ae\t\2\2\2\u00ad\u00a1\3\2\2\2\u00ad\u00a5\3\2"+
		"\2\2\u00ad\u00aa\3\2\2\2\u00ad\u00ac\3\2\2\2\u00ae\u00b1\3\2\2\2\u00af"+
		"\u00ad\3\2\2\2\u00af\u00b0\3\2\2\2\u00b0\5\3\2\2\2\u00b1\u00af\3\2\2\2"+
		"\u00b2\u00b7\5$\23\2\u00b3\u00b4\7:\2\2\u00b4\u00b6\5$\23\2\u00b5\u00b3"+
		"\3\2\2\2\u00b6\u00b9\3\2\2\2\u00b7\u00b5\3\2\2\2\u00b7\u00b8\3\2\2\2\u00b8"+
		"\7\3\2\2\2\u00b9\u00b7\3\2\2\2\u00ba\u00bc\t\2\2\2\u00bb\u00ba\3\2\2\2"+
		"\u00bc\u00bf\3\2\2\2\u00bd\u00bb\3\2\2\2\u00bd\u00be\3\2\2\2\u00be\u00c4"+
		"\3\2\2\2\u00bf\u00bd\3\2\2\2\u00c0\u00c5\5\4\3\2\u00c1\u00c2\5\n\6\2\u00c2"+
		"\u00c3\5\f\7\2\u00c3\u00c5\3\2\2\2\u00c4\u00c0\3\2\2\2\u00c4\u00c1\3\2"+
		"\2\2\u00c5\t\3\2\2\2\u00c6\u00c7\t\3\2\2\u00c7\13\3\2\2\2\u00c8\u00c9"+
		"\7\35\2\2\u00c9\u00ca\5N(\2\u00ca\u00cb\7\36\2\2\u00cb\u00cc\5\f\7\2\u00cc"+
		"\u00d0\3\2\2\2\u00cd\u00d0\5\b\5\2\u00ce\u00d0\7M\2\2\u00cf\u00c8\3\2"+
		"\2\2\u00cf\u00cd\3\2\2\2\u00cf\u00ce\3\2\2\2\u00d0\r\3\2\2\2\u00d1\u00d6"+
		"\5\f\7\2\u00d2\u00d3\t\4\2\2\u00d3\u00d5\5\f\7\2\u00d4\u00d2\3\2\2\2\u00d5"+
		"\u00d8\3\2\2\2\u00d6\u00d4\3\2\2\2\u00d6\u00d7\3\2\2\2\u00d7\17\3\2\2"+
		"\2\u00d8\u00d6\3\2\2\2\u00d9\u00de\5\16\b\2\u00da\u00db\t\5\2\2\u00db"+
		"\u00dd\5\16\b\2\u00dc\u00da\3\2\2\2\u00dd\u00e0\3\2\2\2\u00de\u00dc\3"+
		"\2\2\2\u00de\u00df\3\2\2\2\u00df\21\3\2\2\2\u00e0\u00de\3\2\2\2\u00e1"+
		"\u00e6\5\20\t\2\u00e2\u00e3\t\6\2\2\u00e3\u00e5\5\20\t\2\u00e4\u00e2\3"+
		"\2\2\2\u00e5\u00e8\3\2\2\2\u00e6\u00e4\3\2\2\2\u00e6\u00e7\3\2\2\2\u00e7"+
		"\23\3\2\2\2\u00e8\u00e6\3\2\2\2\u00e9\u00ee\5\22\n\2\u00ea\u00eb\t\7\2"+
		"\2\u00eb\u00ed\5\22\n\2\u00ec\u00ea\3\2\2\2\u00ed\u00f0\3\2\2\2\u00ee"+
		"\u00ec\3\2\2\2\u00ee\u00ef\3\2\2\2\u00ef\25\3\2\2\2\u00f0\u00ee\3\2\2"+
		"\2\u00f1\u00f6\5\24\13\2\u00f2\u00f3\t\b\2\2\u00f3\u00f5\5\24\13\2\u00f4"+
		"\u00f2\3\2\2\2\u00f5\u00f8\3\2\2\2\u00f6\u00f4\3\2\2\2\u00f6\u00f7\3\2"+
		"\2\2\u00f7\27\3\2\2\2\u00f8\u00f6\3\2\2\2\u00f9\u00fe\5\26\f\2\u00fa\u00fb"+
		"\7\60\2\2\u00fb\u00fd\5\26\f\2\u00fc\u00fa\3\2\2\2\u00fd\u0100\3\2\2\2"+
		"\u00fe\u00fc\3\2\2\2\u00fe\u00ff\3\2\2\2\u00ff\31\3\2\2\2\u0100\u00fe"+
		"\3\2\2\2\u0101\u0106\5\30\r\2\u0102\u0103\7\64\2\2\u0103\u0105\5\30\r"+
		"\2\u0104\u0102\3\2\2\2\u0105\u0108\3\2\2\2\u0106\u0104\3\2\2\2\u0106\u0107"+
		"\3\2\2\2\u0107\33\3\2\2\2\u0108\u0106\3\2\2\2\u0109\u010e\5\32\16\2\u010a"+
		"\u010b\7\61\2\2\u010b\u010d\5\32\16\2\u010c\u010a\3\2\2\2\u010d\u0110"+
		"\3\2\2\2\u010e\u010c\3\2\2\2\u010e\u010f\3\2\2\2\u010f\35\3\2\2\2\u0110"+
		"\u010e\3\2\2\2\u0111\u0116\5\34\17\2\u0112\u0113\7\62\2\2\u0113\u0115"+
		"\5\34\17\2\u0114\u0112\3\2\2\2\u0115\u0118\3\2\2\2\u0116\u0114\3\2\2\2"+
		"\u0116\u0117\3\2\2\2\u0117\37\3\2\2\2\u0118\u0116\3\2\2\2\u0119\u011e"+
		"\5\36\20\2\u011a\u011b\7\63\2\2\u011b\u011d\5\36\20\2\u011c\u011a\3\2"+
		"\2\2\u011d\u0120\3\2\2\2\u011e\u011c\3\2\2\2\u011e\u011f\3\2\2\2\u011f"+
		"!\3\2\2\2\u0120\u011e\3\2\2\2\u0121\u0127\5 \21\2\u0122\u0123\7\67\2\2"+
		"\u0123\u0124\5(\25\2\u0124\u0125\78\2\2\u0125\u0126\5\"\22\2\u0126\u0128"+
		"\3\2\2\2\u0127\u0122\3\2\2\2\u0127\u0128\3\2\2\2\u0128#\3\2\2\2\u0129"+
		"\u0130\5\"\22\2\u012a\u012b\5\b\5\2\u012b\u012c\5&\24\2\u012c\u012d\5"+
		"$\23\2\u012d\u0130\3\2\2\2\u012e\u0130\7M\2\2\u012f\u0129\3\2\2\2\u012f"+
		"\u012a\3\2\2\2\u012f\u012e\3\2\2\2\u0130%\3\2\2\2\u0131\u0132\t\t\2\2"+
		"\u0132\'\3\2\2\2\u0133\u0138\5$\23\2\u0134\u0135\7:\2\2\u0135\u0137\5"+
		"$\23\2\u0136\u0134\3\2\2\2\u0137\u013a\3\2\2\2\u0138\u0136\3\2\2\2\u0138"+
		"\u0139\3\2\2\2\u0139)\3\2\2\2\u013a\u0138\3\2\2\2\u013b\u013c\5\"\22\2"+
		"\u013c+\3\2\2\2\u013d\u013f\5.\30\2\u013e\u0140\5\64\33\2\u013f\u013e"+
		"\3\2\2\2\u013f\u0140\3\2\2\2\u0140\u0141\3\2\2\2\u0141\u0142\79\2\2\u0142"+
		"-\3\2\2\2\u0143\u0145\5\62\32\2\u0144\u0143\3\2\2\2\u0145\u0146\3\2\2"+
		"\2\u0146\u0144\3\2\2\2\u0146\u0147\3\2\2\2\u0147/\3\2\2\2\u0148\u014a"+
		"\5\62\32\2\u0149\u0148\3\2\2\2\u014a\u014b\3\2\2\2\u014b\u0149\3\2\2\2"+
		"\u014b\u014c\3\2\2\2\u014c\61\3\2\2\2\u014d\u014e\58\35\2\u014e\63\3\2"+
		"\2\2\u014f\u0154\5\66\34\2\u0150\u0151\7:\2\2\u0151\u0153\5\66\34\2\u0152"+
		"\u0150\3\2\2\2\u0153\u0156\3\2\2\2\u0154\u0152\3\2\2\2\u0154\u0155\3\2"+
		"\2\2\u0155\65\3\2\2\2\u0156\u0154\3\2\2\2\u0157\u015a\5@!\2\u0158\u0159"+
		"\7;\2\2\u0159\u015b\5T+\2\u015a\u0158\3\2\2\2\u015a\u015b\3\2\2\2\u015b"+
		"\67\3\2\2\2\u015c\u016a\7\33\2\2\u015d\u016a\7\7\2\2\u015e\u016a\7\b\2"+
		"\2\u015f\u016a\7\21\2\2\u0160\u016a\7\27\2\2\u0161\u016a\7\30\2\2\u0162"+
		"\u016a\7\16\2\2\u0163\u016a\7\f\2\2\u0164\u016a\7\22\2\2\u0165\u016a\7"+
		"\23\2\2\u0166\u016a\7\25\2\2\u0167\u016a\7\26\2\2\u0168\u016a\5:\36\2"+
		"\u0169\u015c\3\2\2\2\u0169\u015d\3\2\2\2\u0169\u015e\3\2\2\2\u0169\u015f"+
		"\3\2\2\2\u0169\u0160\3\2\2\2\u0169\u0161\3\2\2\2\u0169\u0162\3\2\2\2\u0169"+
		"\u0163\3\2\2\2\u0169\u0164\3\2\2\2\u0169\u0165\3\2\2\2\u0169\u0166\3\2"+
		"\2\2\u0169\u0167\3\2\2\2\u0169\u0168\3\2\2\2\u016a9\3\2\2\2\u016b\u016e"+
		"\7\24\2\2\u016c\u016f\5<\37\2\u016d\u016f\7I\2\2\u016e\u016c\3\2\2\2\u016e"+
		"\u016d\3\2\2\2\u016f;\3\2\2\2\u0170\u0171\7\3\2\2\u0171=\3\2\2\2\u0172"+
		"\u0174\58\35\2\u0173\u0175\5> \2\u0174\u0173\3\2\2\2\u0174\u0175\3\2\2"+
		"\2\u0175?\3\2\2\2\u0176\u0177\5B\"\2\u0177A\3\2\2\2\u0178\u0179\b\"\1"+
		"\2\u0179\u017f\7I\2\2\u017a\u017b\7\35\2\2\u017b\u017c\5@!\2\u017c\u017d"+
		"\7\36\2\2\u017d\u017f\3\2\2\2\u017e\u0178\3\2\2\2\u017e\u017a\3\2\2\2"+
		"\u017f\u0193\3\2\2\2\u0180\u0181\f\5\2\2\u0181\u0183\7\37\2\2\u0182\u0184"+
		"\5$\23\2\u0183\u0182\3\2\2\2\u0183\u0184\3\2\2\2\u0184\u0185\3\2\2\2\u0185"+
		"\u0192\7 \2\2\u0186\u0187\f\4\2\2\u0187\u0188\7\35\2\2\u0188\u0189\5F"+
		"$\2\u0189\u018a\7\36\2\2\u018a\u0192\3\2\2\2\u018b\u018c\f\3\2\2\u018c"+
		"\u018e\7\35\2\2\u018d\u018f\5L\'\2\u018e\u018d\3\2\2\2\u018e\u018f\3\2"+
		"\2\2\u018f\u0190\3\2\2\2\u0190\u0192\7\36\2\2\u0191\u0180\3\2\2\2\u0191"+
		"\u0186\3\2\2\2\u0191\u018b\3\2\2\2\u0192\u0195\3\2\2\2\u0193\u0191\3\2"+
		"\2\2\u0193\u0194\3\2\2\2\u0194C\3\2\2\2\u0195\u0193\3\2\2\2\u0196\u019c"+
		"\n\n\2\2\u0197\u0198\7\35\2\2\u0198\u0199\5D#\2\u0199\u019a\7\36\2\2\u019a"+
		"\u019c\3\2\2\2\u019b\u0196\3\2\2\2\u019b\u0197\3\2\2\2\u019c\u019f\3\2"+
		"\2\2\u019d\u019b\3\2\2\2\u019d\u019e\3\2\2\2\u019eE\3\2\2\2\u019f\u019d"+
		"\3\2\2\2\u01a0\u01a3\5H%\2\u01a1\u01a2\7:\2\2\u01a2\u01a4\7H\2\2\u01a3"+
		"\u01a1\3\2\2\2\u01a3\u01a4\3\2\2\2\u01a4G\3\2\2\2\u01a5\u01aa\5J&\2\u01a6"+
		"\u01a7\7:\2\2\u01a7\u01a9\5J&\2\u01a8\u01a6\3\2\2\2\u01a9\u01ac\3\2\2"+
		"\2\u01aa\u01a8\3\2\2\2\u01aa\u01ab\3\2\2\2\u01abI\3\2\2\2\u01ac\u01aa"+
		"\3\2\2\2\u01ad\u01ae\5.\30\2\u01ae\u01af\5@!\2\u01af\u01b5\3\2\2\2\u01b0"+
		"\u01b2\5\60\31\2\u01b1\u01b3\5P)\2\u01b2\u01b1\3\2\2\2\u01b2\u01b3\3\2"+
		"\2\2\u01b3\u01b5\3\2\2\2\u01b4\u01ad\3\2\2\2\u01b4\u01b0\3\2\2\2\u01b5"+
		"K\3\2\2\2\u01b6\u01bb\7I\2\2\u01b7\u01b8\7:\2\2\u01b8\u01ba\7I\2\2\u01b9"+
		"\u01b7\3\2\2\2\u01ba\u01bd\3\2\2\2\u01bb\u01b9\3\2\2\2\u01bb\u01bc\3\2"+
		"\2\2\u01bcM\3\2\2\2\u01bd\u01bb\3\2\2\2\u01be\u01c0\5> \2\u01bf\u01c1"+
		"\5P)\2\u01c0\u01bf\3\2\2\2\u01c0\u01c1\3\2\2\2\u01c1O\3\2\2\2\u01c2\u01c3"+
		"\5R*\2\u01c3Q\3\2\2\2\u01c4\u01c5\b*\1\2\u01c5\u01c6\7\35\2\2\u01c6\u01c7"+
		"\5P)\2\u01c7\u01c8\7\36\2\2\u01c8\u01d7\3\2\2\2\u01c9\u01cb\7\37\2\2\u01ca"+
		"\u01cc\5$\23\2\u01cb\u01ca\3\2\2\2\u01cb\u01cc\3\2\2\2\u01cc\u01cd\3\2"+
		"\2\2\u01cd\u01d7\7 \2\2\u01ce\u01cf\7\37\2\2\u01cf\u01d0\7-\2\2\u01d0"+
		"\u01d7\7 \2\2\u01d1\u01d3\7\35\2\2\u01d2\u01d4\5F$\2\u01d3\u01d2\3\2\2"+
		"\2\u01d3\u01d4\3\2\2\2\u01d4\u01d5\3\2\2\2\u01d5\u01d7\7\36\2\2\u01d6"+
		"\u01c4\3\2\2\2\u01d6\u01c9\3\2\2\2\u01d6\u01ce\3\2\2\2\u01d6\u01d1\3\2"+
		"\2\2\u01d7\u01ea\3\2\2\2\u01d8\u01d9\f\5\2\2\u01d9\u01db\7\37\2\2\u01da"+
		"\u01dc\5$\23\2\u01db\u01da\3\2\2\2\u01db\u01dc\3\2\2\2\u01dc\u01dd\3\2"+
		"\2\2\u01dd\u01e9\7 \2\2\u01de\u01df\f\4\2\2\u01df\u01e0\7\37\2\2\u01e0"+
		"\u01e1\7-\2\2\u01e1\u01e9\7 \2\2\u01e2\u01e3\f\3\2\2\u01e3\u01e5\7\35"+
		"\2\2\u01e4\u01e6\5F$\2\u01e5\u01e4\3\2\2\2\u01e5\u01e6\3\2\2\2\u01e6\u01e7"+
		"\3\2\2\2\u01e7\u01e9\7\36\2\2\u01e8\u01d8\3\2\2\2\u01e8\u01de\3\2\2\2"+
		"\u01e8\u01e2\3\2\2\2\u01e9\u01ec\3\2\2\2\u01ea\u01e8\3\2\2\2\u01ea\u01eb"+
		"\3\2\2\2\u01ebS\3\2\2\2\u01ec\u01ea\3\2\2\2\u01ed\u01f6\5$\23\2\u01ee"+
		"\u01ef\7!\2\2\u01ef\u01f1\5V,\2\u01f0\u01f2\7:\2\2\u01f1\u01f0\3\2\2\2"+
		"\u01f1\u01f2\3\2\2\2\u01f2\u01f3\3\2\2\2\u01f3\u01f4\7\"\2\2\u01f4\u01f6"+
		"\3\2\2\2\u01f5\u01ed\3\2\2\2\u01f5\u01ee\3\2\2\2\u01f6U\3\2\2\2\u01f7"+
		"\u01f9\5X-\2\u01f8\u01f7\3\2\2\2\u01f8\u01f9\3\2\2\2\u01f9\u01fa\3\2\2"+
		"\2\u01fa\u0202\5T+\2\u01fb\u01fd\7:\2\2\u01fc\u01fe\5X-\2\u01fd\u01fc"+
		"\3\2\2\2\u01fd\u01fe\3\2\2\2\u01fe\u01ff\3\2\2\2\u01ff\u0201\5T+\2\u0200"+
		"\u01fb\3\2\2\2\u0201\u0204\3\2\2\2\u0202\u0200\3\2\2\2\u0202\u0203\3\2"+
		"\2\2\u0203W\3\2\2\2\u0204\u0202\3\2\2\2\u0205\u0206\5Z.\2\u0206\u0207"+
		"\7;\2\2\u0207Y\3\2\2\2\u0208\u020a\5\\/\2\u0209\u0208\3\2\2\2\u020a\u020b"+
		"\3\2\2\2\u020b\u0209\3\2\2\2\u020b\u020c\3\2\2\2\u020c[\3\2\2\2\u020d"+
		"\u020e\7\37\2\2\u020e\u020f\5*\26\2\u020f\u0210\7 \2\2\u0210]\3\2\2\2"+
		"\u0211\u0218\5`\61\2\u0212\u0218\5b\62\2\u0213\u0218\5h\65\2\u0214\u0218"+
		"\5j\66\2\u0215\u0218\5l\67\2\u0216\u0218\5t;\2\u0217\u0211\3\2\2\2\u0217"+
		"\u0212\3\2\2\2\u0217\u0213\3\2\2\2\u0217\u0214\3\2\2\2\u0217\u0215\3\2"+
		"\2\2\u0217\u0216\3\2\2\2\u0218_\3\2\2\2\u0219\u021a\7I\2\2\u021a\u021b"+
		"\78\2\2\u021b\u0225\5^\60\2\u021c\u021d\7\6\2\2\u021d\u021e\5*\26\2\u021e"+
		"\u021f\78\2\2\u021f\u0220\5^\60\2\u0220\u0225\3\2\2\2\u0221\u0222\7\n"+
		"\2\2\u0222\u0223\78\2\2\u0223\u0225\5^\60\2\u0224\u0219\3\2\2\2\u0224"+
		"\u021c\3\2\2\2\u0224\u0221\3\2\2\2\u0225a\3\2\2\2\u0226\u0228\7!\2\2\u0227"+
		"\u0229\5d\63\2\u0228\u0227\3\2\2\2\u0228\u0229\3\2\2\2\u0229\u022a\3\2"+
		"\2\2\u022a\u022b\7\"\2\2\u022bc\3\2\2\2\u022c\u022e\5f\64\2\u022d\u022c"+
		"\3\2\2\2\u022e\u022f\3\2\2\2\u022f\u022d\3\2\2\2\u022f\u0230\3\2\2\2\u0230"+
		"e\3\2\2\2\u0231\u0234\5^\60\2\u0232\u0234\5,\27\2\u0233\u0231\3\2\2\2"+
		"\u0233\u0232\3\2\2\2\u0234g\3\2\2\2\u0235\u0237\5(\25\2\u0236\u0235\3"+
		"\2\2\2\u0236\u0237\3\2\2\2\u0237\u0238\3\2\2\2\u0238\u0239\79\2\2\u0239"+
		"i\3\2\2\2\u023a\u023b\7\20\2\2\u023b\u023c\7\35\2\2\u023c\u023d\5(\25"+
		"\2\u023d\u023e\7\36\2\2\u023e\u0241\5^\60\2\u023f\u0240\7\r\2\2\u0240"+
		"\u0242\5^\60\2\u0241\u023f\3\2\2\2\u0241\u0242\3\2\2\2\u0242\u024a\3\2"+
		"\2\2\u0243\u0244\7\32\2\2\u0244\u0245\7\35\2\2\u0245\u0246\5(\25\2\u0246"+
		"\u0247\7\36\2\2\u0247\u0248\5^\60\2\u0248\u024a\3\2\2\2\u0249\u023a\3"+
		"\2\2\2\u0249\u0243\3\2\2\2\u024ak\3\2\2\2\u024b\u024c\7\34\2\2\u024c\u024d"+
		"\7\35\2\2\u024d\u024e\5(\25\2\u024e\u024f\7\36\2\2\u024f\u0250\5^\60\2"+
		"\u0250\u0260\3\2\2\2\u0251\u0252\7\13\2\2\u0252\u0253\5^\60\2\u0253\u0254"+
		"\7\34\2\2\u0254\u0255\7\35\2\2\u0255\u0256\5(\25\2\u0256\u0257\7\36\2"+
		"\2\u0257\u0258\79\2\2\u0258\u0260\3\2\2\2\u0259\u025a\7\17\2\2\u025a\u025b"+
		"\7\35\2\2\u025b\u025c\5n8\2\u025c\u025d\7\36\2\2\u025d\u025e\5^\60\2\u025e"+
		"\u0260\3\2\2\2\u025f\u024b\3\2\2\2\u025f\u0251\3\2\2\2\u025f\u0259\3\2"+
		"\2\2\u0260m\3\2\2\2\u0261\u0266\5p9\2\u0262\u0264\5(\25\2\u0263\u0262"+
		"\3\2\2\2\u0263\u0264\3\2\2\2\u0264\u0266\3\2\2\2\u0265\u0261\3\2\2\2\u0265"+
		"\u0263\3\2\2\2\u0266\u0267\3\2\2\2\u0267\u0269\79\2\2\u0268\u026a\5r:"+
		"\2\u0269\u0268\3\2\2\2\u0269\u026a\3\2\2\2\u026a\u026b\3\2\2\2\u026b\u026d"+
		"\79\2\2\u026c\u026e\5r:\2\u026d\u026c\3\2\2\2\u026d\u026e\3\2\2\2\u026e"+
		"o\3\2\2\2\u026f\u0271\5.\30\2\u0270\u0272\5\64\33\2\u0271\u0270\3\2\2"+
		"\2\u0271\u0272\3\2\2\2\u0272q\3\2\2\2\u0273\u0278\5$\23\2\u0274\u0275"+
		"\7:\2\2\u0275\u0277\5$\23\2\u0276\u0274\3\2\2\2\u0277\u027a\3\2\2\2\u0278"+
		"\u0276\3\2\2\2\u0278\u0279\3\2\2\2\u0279s\3\2\2\2\u027a\u0278\3\2\2\2"+
		"\u027b\u0281\t\13\2\2\u027c\u027e\7\31\2\2\u027d\u027f\5(\25\2\u027e\u027d"+
		"\3\2\2\2\u027e\u027f\3\2\2\2\u027f\u0281\3\2\2\2\u0280\u027b\3\2\2\2\u0280"+
		"\u027c\3\2\2\2\u0281\u0282\3\2\2\2\u0282\u0283\79\2\2\u0283u\3\2\2\2\u0284"+
		"\u0286\5x=\2\u0285\u0284\3\2\2\2\u0285\u0286\3\2\2\2\u0286\u0287\3\2\2"+
		"\2\u0287\u0288\7\2\2\3\u0288w\3\2\2\2\u0289\u028b\5z>\2\u028a\u0289\3"+
		"\2\2\2\u028b\u028c\3\2\2\2\u028c\u028a\3\2\2\2\u028c\u028d\3\2\2\2\u028d"+
		"y\3\2\2\2\u028e\u0292\5|?\2\u028f\u0292\5,\27\2\u0290\u0292\79\2\2\u0291"+
		"\u028e\3\2\2\2\u0291\u028f\3\2\2\2\u0291\u0290\3\2\2\2\u0292{\3\2\2\2"+
		"\u0293\u0295\5.\30\2\u0294\u0293\3\2\2\2\u0294\u0295\3\2\2\2\u0295\u0296"+
		"\3\2\2\2\u0296\u0298\5@!\2\u0297\u0299\5~@\2\u0298\u0297\3\2\2\2\u0298"+
		"\u0299\3\2\2\2\u0299\u029a\3\2\2\2\u029a\u029b\5b\62\2\u029b}\3\2\2\2"+
		"\u029c\u029e\5,\27\2\u029d\u029c\3\2\2\2\u029e\u029f\3\2\2\2\u029f\u029d"+
		"\3\2\2\2\u029f\u02a0\3\2\2\2\u02a0\177\3\2\2\2\u02a1\u02a2\7\4\2\2\u02a2"+
		"\u02a4\7!\2\2\u02a3\u02a5\5d\63\2\u02a4\u02a3\3\2\2\2\u02a4\u02a5\3\2"+
		"\2\2\u02a5\u02a6\3\2\2\2\u02a6\u02a7\7\"\2\2\u02a7\u0081\3\2\2\2T\u0087"+
		"\u0092\u009b\u009f\u00a7\u00ad\u00af\u00b7\u00bd\u00c4\u00cf\u00d6\u00de"+
		"\u00e6\u00ee\u00f6\u00fe\u0106\u010e\u0116\u011e\u0127\u012f\u0138\u013f"+
		"\u0146\u014b\u0154\u015a\u0169\u016e\u0174\u017e\u0183\u018e\u0191\u0193"+
		"\u019b\u019d\u01a3\u01aa\u01b2\u01b4\u01bb\u01c0\u01cb\u01d3\u01d6\u01db"+
		"\u01e5\u01e8\u01ea\u01f1\u01f5\u01f8\u01fd\u0202\u020b\u0217\u0224\u0228"+
		"\u022f\u0233\u0236\u0241\u0249\u025f\u0263\u0265\u0269\u026d\u0271\u0278"+
		"\u027e\u0280\u0285\u028c\u0291\u0294\u0298\u029f\u02a4";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}