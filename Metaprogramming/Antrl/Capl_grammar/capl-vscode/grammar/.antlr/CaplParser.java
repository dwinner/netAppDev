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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, Variables=9, 
		Break=10, Case=11, Char=12, Byte=13, Continue=14, Default=15, Do=16, Double=17, 
		Else=18, Float=19, For=20, If=21, Inline=22, Int=23, Word=24, Dword=25, 
		Message=26, Timer=27, MsTimer=28, Long=29, Int64=30, Restrict=31, Return=32, 
		Switch=33, Void=34, While=35, Alignas=36, Alignof=37, Atomic=38, Bool=39, 
		Complex=40, Imaginary=41, Noreturn=42, ThreadLocal=43, LeftParen=44, RightParen=45, 
		LeftBracket=46, RightBracket=47, LeftBrace=48, RightBrace=49, Less=50, 
		LessEqual=51, Greater=52, GreaterEqual=53, LeftShift=54, RightShift=55, 
		Plus=56, PlusPlus=57, Minus=58, MinusMinus=59, Star=60, Div=61, Mod=62, 
		And=63, Or=64, AndAnd=65, OrOr=66, Caret=67, Not=68, Tilde=69, Question=70, 
		Colon=71, Semi=72, Comma=73, Assign=74, StarAssign=75, DivAssign=76, ModAssign=77, 
		PlusAssign=78, MinusAssign=79, LeftShiftAssign=80, RightShiftAssign=81, 
		AndAssign=82, XorAssign=83, OrAssign=84, Equal=85, NotEqual=86, Dot=87, 
		Ellipsis=88, Identifier=89, Constant=90, DigitSequence=91, StringLiteral=92, 
		AsmBlock=93, Whitespace=94, Newline=95, BlockComment=96, LineComment=97;
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
		RULE_typeSpecifier = 27, RULE_specifierQualifierList = 28, RULE_atomicTypeSpecifier = 29, 
		RULE_functionSpecifier = 30, RULE_alignmentSpecifier = 31, RULE_declarator = 32, 
		RULE_directDeclarator = 33, RULE_gccDeclaratorExtension = 34, RULE_gccAttributeSpecifier = 35, 
		RULE_gccAttributeList = 36, RULE_gccAttribute = 37, RULE_nestedParenthesesBlock = 38, 
		RULE_parameterTypeList = 39, RULE_parameterList = 40, RULE_parameterDeclaration = 41, 
		RULE_identifierList = 42, RULE_typeName = 43, RULE_abstractDeclarator = 44, 
		RULE_directAbstractDeclarator = 45, RULE_initializer = 46, RULE_initializerList = 47, 
		RULE_designation = 48, RULE_designatorList = 49, RULE_designator = 50, 
		RULE_statement = 51, RULE_labeledStatement = 52, RULE_compoundStatement = 53, 
		RULE_blockItemList = 54, RULE_blockItem = 55, RULE_expressionStatement = 56, 
		RULE_selectionStatement = 57, RULE_iterationStatement = 58, RULE_forCondition = 59, 
		RULE_forDeclaration = 60, RULE_forExpression = 61, RULE_jumpStatement = 62, 
		RULE_compilationUnit = 63, RULE_translationUnit = 64, RULE_externalDeclaration = 65, 
		RULE_functionDefinition = 66, RULE_declarationList = 67, RULE_variableDeclarationBlock = 68;
	private static String[] makeRuleNames() {
		return new String[] {
			"primaryExpression", "postfixExpression", "argumentExpressionList", "unaryExpression", 
			"unaryOperator", "castExpression", "multiplicativeExpression", "additiveExpression", 
			"shiftExpression", "relationalExpression", "equalityExpression", "andExpression", 
			"exclusiveOrExpression", "inclusiveOrExpression", "logicalAndExpression", 
			"logicalOrExpression", "conditionalExpression", "assignmentExpression", 
			"assignmentOperator", "expression", "constantExpression", "declaration", 
			"declarationSpecifiers", "declarationSpecifiers2", "declarationSpecifier", 
			"initDeclaratorList", "initDeclarator", "typeSpecifier", "specifierQualifierList", 
			"atomicTypeSpecifier", "functionSpecifier", "alignmentSpecifier", "declarator", 
			"directDeclarator", "gccDeclaratorExtension", "gccAttributeSpecifier", 
			"gccAttributeList", "gccAttribute", "nestedParenthesesBlock", "parameterTypeList", 
			"parameterList", "parameterDeclaration", "identifierList", "typeName", 
			"abstractDeclarator", "directAbstractDeclarator", "initializer", "initializerList", 
			"designation", "designatorList", "designator", "statement", "labeledStatement", 
			"compoundStatement", "blockItemList", "blockItem", "expressionStatement", 
			"selectionStatement", "iterationStatement", "forCondition", "forDeclaration", 
			"forExpression", "jumpStatement", "compilationUnit", "translationUnit", 
			"externalDeclaration", "functionDefinition", "declarationList", "variableDeclarationBlock"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'__m128'", "'__m128d'", "'__m128i'", "'__inline__'", "'__stdcall'", 
			"'__declspec'", "'__asm'", "'__attribute__'", "'variables'", "'break'", 
			"'case'", "'char'", "'byte'", "'continue'", "'default'", "'do'", "'double'", 
			"'else'", "'float'", "'for'", "'if'", "'inline'", "'int'", "'word'", 
			"'dword'", "'message'", "'timer'", "'msTimer'", "'long'", "'int64'", 
			"'restrict'", "'return'", "'switch'", "'void'", "'while'", "'_Alignas'", 
			"'_Alignof'", "'_Atomic'", "'_Bool'", "'_Complex'", "'_Imaginary'", "'_Noreturn'", 
			"'_Thread_local'", "'('", "')'", "'['", "']'", "'{'", "'}'", "'<'", "'<='", 
			"'>'", "'>='", "'<<'", "'>>'", "'+'", "'++'", "'-'", "'--'", "'*'", "'/'", 
			"'%'", "'&'", "'|'", "'&&'", "'||'", "'^'", "'!'", "'~'", "'?'", "':'", 
			"';'", "','", "'='", "'*='", "'/='", "'%='", "'+='", "'-='", "'<<='", 
			"'>>='", "'&='", "'^='", "'|='", "'=='", "'!='", "'.'", "'...'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, "Variables", "Break", 
			"Case", "Char", "Byte", "Continue", "Default", "Do", "Double", "Else", 
			"Float", "For", "If", "Inline", "Int", "Word", "Dword", "Message", "Timer", 
			"MsTimer", "Long", "Int64", "Restrict", "Return", "Switch", "Void", "While", 
			"Alignas", "Alignof", "Atomic", "Bool", "Complex", "Imaginary", "Noreturn", 
			"ThreadLocal", "LeftParen", "RightParen", "LeftBracket", "RightBracket", 
			"LeftBrace", "RightBrace", "Less", "LessEqual", "Greater", "GreaterEqual", 
			"LeftShift", "RightShift", "Plus", "PlusPlus", "Minus", "MinusMinus", 
			"Star", "Div", "Mod", "And", "Or", "AndAnd", "OrOr", "Caret", "Not", 
			"Tilde", "Question", "Colon", "Semi", "Comma", "Assign", "StarAssign", 
			"DivAssign", "ModAssign", "PlusAssign", "MinusAssign", "LeftShiftAssign", 
			"RightShiftAssign", "AndAssign", "XorAssign", "OrAssign", "Equal", "NotEqual", 
			"Dot", "Ellipsis", "Identifier", "Constant", "DigitSequence", "StringLiteral", 
			"AsmBlock", "Whitespace", "Newline", "BlockComment", "LineComment"
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
			setState(154);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(138);
				match(Identifier);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(139);
				match(Constant);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(141); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(140);
					match(StringLiteral);
					}
					}
					setState(143); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==StringLiteral );
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(145);
				match(LeftParen);
				setState(146);
				expression();
				setState(147);
				match(RightParen);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(149);
				match(LeftParen);
				setState(150);
				compoundStatement();
				setState(151);
				match(RightParen);
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(153);
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
			setState(167);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				{
				setState(156);
				primaryExpression();
				}
				break;
			case 2:
				{
				setState(157);
				match(LeftParen);
				setState(158);
				typeName();
				setState(159);
				match(RightParen);
				setState(160);
				match(LeftBrace);
				setState(161);
				initializerList();
				setState(163);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(162);
					match(Comma);
					}
				}

				setState(165);
				match(RightBrace);
				}
				break;
			}
			setState(183);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 44)) & ~0x3f) == 0 && ((1L << (_la - 44)) & ((1L << (LeftParen - 44)) | (1L << (LeftBracket - 44)) | (1L << (PlusPlus - 44)) | (1L << (MinusMinus - 44)) | (1L << (Dot - 44)))) != 0)) {
				{
				setState(181);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LeftBracket:
					{
					setState(169);
					match(LeftBracket);
					setState(170);
					expression();
					setState(171);
					match(RightBracket);
					}
					break;
				case LeftParen:
					{
					setState(173);
					match(LeftParen);
					setState(175);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
						{
						setState(174);
						argumentExpressionList();
						}
					}

					setState(177);
					match(RightParen);
					}
					break;
				case Dot:
					{
					setState(178);
					match(Dot);
					setState(179);
					match(Identifier);
					}
					break;
				case PlusPlus:
				case MinusMinus:
					{
					setState(180);
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
				setState(185);
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
			setState(186);
			assignmentExpression();
			setState(191);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(187);
				match(Comma);
				setState(188);
				assignmentExpression();
				}
				}
				setState(193);
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
		public TerminalNode Alignof() { return getToken(CaplParser.Alignof, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public TerminalNode AndAnd() { return getToken(CaplParser.AndAnd, 0); }
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
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
			setState(197);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==PlusPlus || _la==MinusMinus) {
				{
				{
				setState(194);
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
				setState(199);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(211);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Variables:
			case LeftParen:
			case Identifier:
			case Constant:
			case StringLiteral:
				{
				setState(200);
				postfixExpression();
				}
				break;
			case Plus:
			case Minus:
			case Not:
			case Tilde:
				{
				setState(201);
				unaryOperator();
				setState(202);
				castExpression();
				}
				break;
			case Alignof:
				{
				setState(204);
				match(Alignof);
				setState(205);
				match(LeftParen);
				setState(206);
				typeName();
				setState(207);
				match(RightParen);
				}
				break;
			case AndAnd:
				{
				setState(209);
				match(AndAnd);
				setState(210);
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
			setState(213);
			_la = _input.LA(1);
			if ( !(((((_la - 56)) & ~0x3f) == 0 && ((1L << (_la - 56)) & ((1L << (Plus - 56)) | (1L << (Minus - 56)) | (1L << (Not - 56)) | (1L << (Tilde - 56)))) != 0)) ) {
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
			setState(222);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(215);
				match(LeftParen);
				setState(216);
				typeName();
				setState(217);
				match(RightParen);
				setState(218);
				castExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(220);
				unaryExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(221);
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
			setState(224);
			castExpression();
			setState(229);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) {
				{
				{
				setState(225);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(226);
				castExpression();
				}
				}
				setState(231);
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
			setState(232);
			multiplicativeExpression();
			setState(237);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Plus || _la==Minus) {
				{
				{
				setState(233);
				_la = _input.LA(1);
				if ( !(_la==Plus || _la==Minus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(234);
				multiplicativeExpression();
				}
				}
				setState(239);
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
			setState(240);
			additiveExpression();
			setState(245);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==LeftShift || _la==RightShift) {
				{
				{
				setState(241);
				_la = _input.LA(1);
				if ( !(_la==LeftShift || _la==RightShift) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(242);
				additiveExpression();
				}
				}
				setState(247);
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
			setState(248);
			shiftExpression();
			setState(253);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) {
				{
				{
				setState(249);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(250);
				shiftExpression();
				}
				}
				setState(255);
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
			setState(256);
			relationalExpression();
			setState(261);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Equal || _la==NotEqual) {
				{
				{
				setState(257);
				_la = _input.LA(1);
				if ( !(_la==Equal || _la==NotEqual) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(258);
				relationalExpression();
				}
				}
				setState(263);
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
			setState(264);
			equalityExpression();
			setState(269);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==And) {
				{
				{
				setState(265);
				match(And);
				setState(266);
				equalityExpression();
				}
				}
				setState(271);
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
			setState(272);
			andExpression();
			setState(277);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Caret) {
				{
				{
				setState(273);
				match(Caret);
				setState(274);
				andExpression();
				}
				}
				setState(279);
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
			setState(280);
			exclusiveOrExpression();
			setState(285);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Or) {
				{
				{
				setState(281);
				match(Or);
				setState(282);
				exclusiveOrExpression();
				}
				}
				setState(287);
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
			setState(288);
			inclusiveOrExpression();
			setState(293);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AndAnd) {
				{
				{
				setState(289);
				match(AndAnd);
				setState(290);
				inclusiveOrExpression();
				}
				}
				setState(295);
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
			setState(296);
			logicalAndExpression();
			setState(301);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OrOr) {
				{
				{
				setState(297);
				match(OrOr);
				setState(298);
				logicalAndExpression();
				}
				}
				setState(303);
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
			setState(304);
			logicalOrExpression();
			setState(310);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Question) {
				{
				setState(305);
				match(Question);
				setState(306);
				expression();
				setState(307);
				match(Colon);
				setState(308);
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
			setState(318);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(312);
				conditionalExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(313);
				unaryExpression();
				setState(314);
				assignmentOperator();
				setState(315);
				assignmentExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(317);
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
			setState(320);
			_la = _input.LA(1);
			if ( !(((((_la - 74)) & ~0x3f) == 0 && ((1L << (_la - 74)) & ((1L << (Assign - 74)) | (1L << (StarAssign - 74)) | (1L << (DivAssign - 74)) | (1L << (ModAssign - 74)) | (1L << (PlusAssign - 74)) | (1L << (MinusAssign - 74)) | (1L << (LeftShiftAssign - 74)) | (1L << (RightShiftAssign - 74)) | (1L << (AndAssign - 74)) | (1L << (XorAssign - 74)) | (1L << (OrAssign - 74)))) != 0)) ) {
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
			setState(322);
			assignmentExpression();
			setState(327);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(323);
				match(Comma);
				setState(324);
				assignmentExpression();
				}
				}
				setState(329);
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
			setState(330);
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
			setState(332);
			declarationSpecifiers();
			setState(334);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(333);
				initDeclaratorList();
				}
			}

			setState(336);
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
			setState(339); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(338);
				declarationSpecifier();
				}
				}
				setState(341); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0) );
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
			setState(344); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(343);
				declarationSpecifier();
				}
				}
				setState(346); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0) );
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
		public FunctionSpecifierContext functionSpecifier() {
			return getRuleContext(FunctionSpecifierContext.class,0);
		}
		public AlignmentSpecifierContext alignmentSpecifier() {
			return getRuleContext(AlignmentSpecifierContext.class,0);
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
			setState(351);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case T__1:
			case T__2:
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
			case Atomic:
			case Bool:
			case Complex:
				enterOuterAlt(_localctx, 1);
				{
				setState(348);
				typeSpecifier();
				}
				break;
			case T__3:
			case T__4:
			case T__5:
			case T__7:
			case Inline:
			case Noreturn:
				enterOuterAlt(_localctx, 2);
				{
				setState(349);
				functionSpecifier();
				}
				break;
			case Alignas:
				enterOuterAlt(_localctx, 3);
				{
				setState(350);
				alignmentSpecifier();
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
			setState(353);
			initDeclarator();
			setState(358);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(354);
				match(Comma);
				setState(355);
				initDeclarator();
				}
				}
				setState(360);
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
			setState(361);
			declarator();
			setState(364);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Assign) {
				{
				setState(362);
				match(Assign);
				setState(363);
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
		public TerminalNode Message() { return getToken(CaplParser.Message, 0); }
		public TerminalNode Timer() { return getToken(CaplParser.Timer, 0); }
		public TerminalNode MsTimer() { return getToken(CaplParser.MsTimer, 0); }
		public TerminalNode Bool() { return getToken(CaplParser.Bool, 0); }
		public TerminalNode Complex() { return getToken(CaplParser.Complex, 0); }
		public AtomicTypeSpecifierContext atomicTypeSpecifier() {
			return getRuleContext(AtomicTypeSpecifierContext.class,0);
		}
		public TypeSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeSpecifier; }
	}

	public final TypeSpecifierContext typeSpecifier() throws RecognitionException {
		TypeSpecifierContext _localctx = new TypeSpecifierContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_typeSpecifier);
		int _la;
		try {
			setState(368);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case T__1:
			case T__2:
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
			case Bool:
			case Complex:
				enterOuterAlt(_localctx, 1);
				{
				setState(366);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Bool) | (1L << Complex))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case Atomic:
				enterOuterAlt(_localctx, 2);
				{
				setState(367);
				atomicTypeSpecifier();
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
		enterRule(_localctx, 56, RULE_specifierQualifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(370);
			typeSpecifier();
			setState(372);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Atomic) | (1L << Bool) | (1L << Complex))) != 0)) {
				{
				setState(371);
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

	public static class AtomicTypeSpecifierContext extends ParserRuleContext {
		public TerminalNode Atomic() { return getToken(CaplParser.Atomic, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public AtomicTypeSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_atomicTypeSpecifier; }
	}

	public final AtomicTypeSpecifierContext atomicTypeSpecifier() throws RecognitionException {
		AtomicTypeSpecifierContext _localctx = new AtomicTypeSpecifierContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_atomicTypeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(374);
			match(Atomic);
			setState(375);
			match(LeftParen);
			setState(376);
			typeName();
			setState(377);
			match(RightParen);
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

	public static class FunctionSpecifierContext extends ParserRuleContext {
		public TerminalNode Inline() { return getToken(CaplParser.Inline, 0); }
		public TerminalNode Noreturn() { return getToken(CaplParser.Noreturn, 0); }
		public GccAttributeSpecifierContext gccAttributeSpecifier() {
			return getRuleContext(GccAttributeSpecifierContext.class,0);
		}
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public FunctionSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionSpecifier; }
	}

	public final FunctionSpecifierContext functionSpecifier() throws RecognitionException {
		FunctionSpecifierContext _localctx = new FunctionSpecifierContext(_ctx, getState());
		enterRule(_localctx, 60, RULE_functionSpecifier);
		int _la;
		try {
			setState(385);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__4:
			case Inline:
			case Noreturn:
				enterOuterAlt(_localctx, 1);
				{
				setState(379);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__4) | (1L << Inline) | (1L << Noreturn))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case T__7:
				enterOuterAlt(_localctx, 2);
				{
				setState(380);
				gccAttributeSpecifier();
				}
				break;
			case T__5:
				enterOuterAlt(_localctx, 3);
				{
				setState(381);
				match(T__5);
				setState(382);
				match(LeftParen);
				setState(383);
				match(Identifier);
				setState(384);
				match(RightParen);
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

	public static class AlignmentSpecifierContext extends ParserRuleContext {
		public TerminalNode Alignas() { return getToken(CaplParser.Alignas, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public ConstantExpressionContext constantExpression() {
			return getRuleContext(ConstantExpressionContext.class,0);
		}
		public AlignmentSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_alignmentSpecifier; }
	}

	public final AlignmentSpecifierContext alignmentSpecifier() throws RecognitionException {
		AlignmentSpecifierContext _localctx = new AlignmentSpecifierContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_alignmentSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(387);
			match(Alignas);
			setState(388);
			match(LeftParen);
			setState(391);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case T__1:
			case T__2:
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
			case Atomic:
			case Bool:
			case Complex:
				{
				setState(389);
				typeName();
				}
				break;
			case Variables:
			case Alignof:
			case LeftParen:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case AndAnd:
			case Not:
			case Tilde:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				{
				setState(390);
				constantExpression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(393);
			match(RightParen);
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
		public List<GccDeclaratorExtensionContext> gccDeclaratorExtension() {
			return getRuleContexts(GccDeclaratorExtensionContext.class);
		}
		public GccDeclaratorExtensionContext gccDeclaratorExtension(int i) {
			return getRuleContext(GccDeclaratorExtensionContext.class,i);
		}
		public DeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarator; }
	}

	public final DeclaratorContext declarator() throws RecognitionException {
		DeclaratorContext _localctx = new DeclaratorContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_declarator);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(395);
			directDeclarator(0);
			setState(399);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,34,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(396);
					gccDeclaratorExtension();
					}
					} 
				}
				setState(401);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,34,_ctx);
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

	public static class DirectDeclaratorContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public DeclaratorContext declarator() {
			return getRuleContext(DeclaratorContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public TerminalNode Colon() { return getToken(CaplParser.Colon, 0); }
		public TerminalNode DigitSequence() { return getToken(CaplParser.DigitSequence, 0); }
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
		int _startState = 66;
		enterRecursionRule(_localctx, 66, RULE_directDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(411);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,35,_ctx) ) {
			case 1:
				{
				setState(403);
				match(Identifier);
				}
				break;
			case 2:
				{
				setState(404);
				match(LeftParen);
				setState(405);
				declarator();
				setState(406);
				match(RightParen);
				}
				break;
			case 3:
				{
				setState(408);
				match(Identifier);
				setState(409);
				match(Colon);
				setState(410);
				match(DigitSequence);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(432);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,39,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(430);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,38,_ctx) ) {
					case 1:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(413);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(414);
						match(LeftBracket);
						setState(416);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
							{
							setState(415);
							assignmentExpression();
							}
						}

						setState(418);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(419);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(420);
						match(LeftParen);
						setState(421);
						parameterTypeList();
						setState(422);
						match(RightParen);
						}
						break;
					case 3:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(424);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(425);
						match(LeftParen);
						setState(427);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==Identifier) {
							{
							setState(426);
							identifierList();
							}
						}

						setState(429);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(434);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,39,_ctx);
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

	public static class GccDeclaratorExtensionContext extends ParserRuleContext {
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public List<TerminalNode> StringLiteral() { return getTokens(CaplParser.StringLiteral); }
		public TerminalNode StringLiteral(int i) {
			return getToken(CaplParser.StringLiteral, i);
		}
		public GccAttributeSpecifierContext gccAttributeSpecifier() {
			return getRuleContext(GccAttributeSpecifierContext.class,0);
		}
		public GccDeclaratorExtensionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gccDeclaratorExtension; }
	}

	public final GccDeclaratorExtensionContext gccDeclaratorExtension() throws RecognitionException {
		GccDeclaratorExtensionContext _localctx = new GccDeclaratorExtensionContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_gccDeclaratorExtension);
		int _la;
		try {
			setState(444);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__6:
				enterOuterAlt(_localctx, 1);
				{
				setState(435);
				match(T__6);
				setState(436);
				match(LeftParen);
				setState(438); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(437);
					match(StringLiteral);
					}
					}
					setState(440); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==StringLiteral );
				setState(442);
				match(RightParen);
				}
				break;
			case T__7:
				enterOuterAlt(_localctx, 2);
				{
				setState(443);
				gccAttributeSpecifier();
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

	public static class GccAttributeSpecifierContext extends ParserRuleContext {
		public List<TerminalNode> LeftParen() { return getTokens(CaplParser.LeftParen); }
		public TerminalNode LeftParen(int i) {
			return getToken(CaplParser.LeftParen, i);
		}
		public GccAttributeListContext gccAttributeList() {
			return getRuleContext(GccAttributeListContext.class,0);
		}
		public List<TerminalNode> RightParen() { return getTokens(CaplParser.RightParen); }
		public TerminalNode RightParen(int i) {
			return getToken(CaplParser.RightParen, i);
		}
		public GccAttributeSpecifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gccAttributeSpecifier; }
	}

	public final GccAttributeSpecifierContext gccAttributeSpecifier() throws RecognitionException {
		GccAttributeSpecifierContext _localctx = new GccAttributeSpecifierContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_gccAttributeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(446);
			match(T__7);
			setState(447);
			match(LeftParen);
			setState(448);
			match(LeftParen);
			setState(449);
			gccAttributeList();
			setState(450);
			match(RightParen);
			setState(451);
			match(RightParen);
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

	public static class GccAttributeListContext extends ParserRuleContext {
		public List<GccAttributeContext> gccAttribute() {
			return getRuleContexts(GccAttributeContext.class);
		}
		public GccAttributeContext gccAttribute(int i) {
			return getRuleContext(GccAttributeContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public GccAttributeListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gccAttributeList; }
	}

	public final GccAttributeListContext gccAttributeList() throws RecognitionException {
		GccAttributeListContext _localctx = new GccAttributeListContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_gccAttributeList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(454);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Or - 64)) | (1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(453);
				gccAttribute();
				}
			}

			setState(462);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(456);
				match(Comma);
				setState(458);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Or - 64)) | (1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
					{
					setState(457);
					gccAttribute();
					}
				}

				}
				}
				setState(464);
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

	public static class GccAttributeContext extends ParserRuleContext {
		public TerminalNode Comma() { return getToken(CaplParser.Comma, 0); }
		public List<TerminalNode> LeftParen() { return getTokens(CaplParser.LeftParen); }
		public TerminalNode LeftParen(int i) {
			return getToken(CaplParser.LeftParen, i);
		}
		public List<TerminalNode> RightParen() { return getTokens(CaplParser.RightParen); }
		public TerminalNode RightParen(int i) {
			return getToken(CaplParser.RightParen, i);
		}
		public ArgumentExpressionListContext argumentExpressionList() {
			return getRuleContext(ArgumentExpressionListContext.class,0);
		}
		public GccAttributeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_gccAttribute; }
	}

	public final GccAttributeContext gccAttribute() throws RecognitionException {
		GccAttributeContext _localctx = new GccAttributeContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_gccAttribute);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(465);
			_la = _input.LA(1);
			if ( _la <= 0 || (((((_la - 44)) & ~0x3f) == 0 && ((1L << (_la - 44)) & ((1L << (LeftParen - 44)) | (1L << (RightParen - 44)) | (1L << (Comma - 44)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(471);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen) {
				{
				setState(466);
				match(LeftParen);
				setState(468);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
					{
					setState(467);
					argumentExpressionList();
					}
				}

				setState(470);
				match(RightParen);
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
		enterRule(_localctx, 76, RULE_nestedParenthesesBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(480);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftParen) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Or - 64)) | (1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Comma - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(478);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case T__0:
				case T__1:
				case T__2:
				case T__3:
				case T__4:
				case T__5:
				case T__6:
				case T__7:
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
				case Inline:
				case Int:
				case Word:
				case Dword:
				case Message:
				case Timer:
				case MsTimer:
				case Long:
				case Int64:
				case Restrict:
				case Return:
				case Switch:
				case Void:
				case While:
				case Alignas:
				case Alignof:
				case Atomic:
				case Bool:
				case Complex:
				case Imaginary:
				case Noreturn:
				case ThreadLocal:
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
				case Dot:
				case Ellipsis:
				case Identifier:
				case Constant:
				case DigitSequence:
				case StringLiteral:
				case AsmBlock:
				case Whitespace:
				case Newline:
				case BlockComment:
				case LineComment:
					{
					setState(473);
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
					setState(474);
					match(LeftParen);
					setState(475);
					nestedParenthesesBlock();
					setState(476);
					match(RightParen);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(482);
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
		enterRule(_localctx, 78, RULE_parameterTypeList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(483);
			parameterList();
			setState(486);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Comma) {
				{
				setState(484);
				match(Comma);
				setState(485);
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
		enterRule(_localctx, 80, RULE_parameterList);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(488);
			parameterDeclaration();
			setState(493);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,50,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(489);
					match(Comma);
					setState(490);
					parameterDeclaration();
					}
					} 
				}
				setState(495);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,50,_ctx);
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
		enterRule(_localctx, 82, RULE_parameterDeclaration);
		int _la;
		try {
			setState(503);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,52,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(496);
				declarationSpecifiers();
				setState(497);
				declarator();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(499);
				declarationSpecifiers2();
				setState(501);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LeftParen || _la==LeftBracket) {
					{
					setState(500);
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
		enterRule(_localctx, 84, RULE_identifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(505);
			match(Identifier);
			setState(510);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(506);
				match(Comma);
				setState(507);
				match(Identifier);
				}
				}
				setState(512);
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
		enterRule(_localctx, 86, RULE_typeName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(513);
			specifierQualifierList();
			setState(515);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==LeftBracket) {
				{
				setState(514);
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
		public List<GccDeclaratorExtensionContext> gccDeclaratorExtension() {
			return getRuleContexts(GccDeclaratorExtensionContext.class);
		}
		public GccDeclaratorExtensionContext gccDeclaratorExtension(int i) {
			return getRuleContext(GccDeclaratorExtensionContext.class,i);
		}
		public AbstractDeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_abstractDeclarator; }
	}

	public final AbstractDeclaratorContext abstractDeclarator() throws RecognitionException {
		AbstractDeclaratorContext _localctx = new AbstractDeclaratorContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_abstractDeclarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(517);
			directAbstractDeclarator(0);
			setState(521);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__6 || _la==T__7) {
				{
				{
				setState(518);
				gccDeclaratorExtension();
				}
				}
				setState(523);
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

	public static class DirectAbstractDeclaratorContext extends ParserRuleContext {
		public TerminalNode LeftParen() { return getToken(CaplParser.LeftParen, 0); }
		public AbstractDeclaratorContext abstractDeclarator() {
			return getRuleContext(AbstractDeclaratorContext.class,0);
		}
		public TerminalNode RightParen() { return getToken(CaplParser.RightParen, 0); }
		public List<GccDeclaratorExtensionContext> gccDeclaratorExtension() {
			return getRuleContexts(GccDeclaratorExtensionContext.class);
		}
		public GccDeclaratorExtensionContext gccDeclaratorExtension(int i) {
			return getRuleContext(GccDeclaratorExtensionContext.class,i);
		}
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
		int _startState = 90;
		enterRecursionRule(_localctx, 90, RULE_directAbstractDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(553);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,60,_ctx) ) {
			case 1:
				{
				setState(525);
				match(LeftParen);
				setState(526);
				abstractDeclarator();
				setState(527);
				match(RightParen);
				setState(531);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(528);
						gccDeclaratorExtension();
						}
						} 
					}
					setState(533);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
				}
				}
				break;
			case 2:
				{
				setState(534);
				match(LeftBracket);
				setState(536);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
					{
					setState(535);
					assignmentExpression();
					}
				}

				setState(538);
				match(RightBracket);
				}
				break;
			case 3:
				{
				setState(539);
				match(LeftBracket);
				setState(540);
				match(Star);
				setState(541);
				match(RightBracket);
				}
				break;
			case 4:
				{
				setState(542);
				match(LeftParen);
				setState(544);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
					{
					setState(543);
					parameterTypeList();
					}
				}

				setState(546);
				match(RightParen);
				setState(550);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,59,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(547);
						gccDeclaratorExtension();
						}
						} 
					}
					setState(552);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,59,_ctx);
				}
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(579);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(577);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,64,_ctx) ) {
					case 1:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(555);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(556);
						match(LeftBracket);
						setState(558);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
							{
							setState(557);
							assignmentExpression();
							}
						}

						setState(560);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(561);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(562);
						match(LeftBracket);
						setState(563);
						match(Star);
						setState(564);
						match(RightBracket);
						}
						break;
					case 3:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(565);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(566);
						match(LeftParen);
						setState(568);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
							{
							setState(567);
							parameterTypeList();
							}
						}

						setState(570);
						match(RightParen);
						setState(574);
						_errHandler.sync(this);
						_alt = getInterpreter().adaptivePredict(_input,63,_ctx);
						while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
							if ( _alt==1 ) {
								{
								{
								setState(571);
								gccDeclaratorExtension();
								}
								} 
							}
							setState(576);
							_errHandler.sync(this);
							_alt = getInterpreter().adaptivePredict(_input,63,_ctx);
						}
						}
						break;
					}
					} 
				}
				setState(581);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
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
		enterRule(_localctx, 92, RULE_initializer);
		int _la;
		try {
			setState(590);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Variables:
			case Alignof:
			case LeftParen:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case AndAnd:
			case Not:
			case Tilde:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(582);
				assignmentExpression();
				}
				break;
			case LeftBrace:
				enterOuterAlt(_localctx, 2);
				{
				setState(583);
				match(LeftBrace);
				setState(584);
				initializerList();
				setState(586);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(585);
					match(Comma);
					}
				}

				setState(588);
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
		enterRule(_localctx, 94, RULE_initializerList);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(593);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftBracket || _la==Dot) {
				{
				setState(592);
				designation();
				}
			}

			setState(595);
			initializer();
			setState(603);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,70,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(596);
					match(Comma);
					setState(598);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==LeftBracket || _la==Dot) {
						{
						setState(597);
						designation();
						}
					}

					setState(600);
					initializer();
					}
					} 
				}
				setState(605);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,70,_ctx);
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
		enterRule(_localctx, 96, RULE_designation);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(606);
			designatorList();
			setState(607);
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
		enterRule(_localctx, 98, RULE_designatorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(610); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(609);
				designator();
				}
				}
				setState(612); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==LeftBracket || _la==Dot );
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
		public TerminalNode Dot() { return getToken(CaplParser.Dot, 0); }
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public DesignatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_designator; }
	}

	public final DesignatorContext designator() throws RecognitionException {
		DesignatorContext _localctx = new DesignatorContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_designator);
		try {
			setState(620);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LeftBracket:
				enterOuterAlt(_localctx, 1);
				{
				setState(614);
				match(LeftBracket);
				setState(615);
				constantExpression();
				setState(616);
				match(RightBracket);
				}
				break;
			case Dot:
				enterOuterAlt(_localctx, 2);
				{
				setState(618);
				match(Dot);
				setState(619);
				match(Identifier);
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
		enterRule(_localctx, 102, RULE_statement);
		try {
			setState(628);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,73,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(622);
				labeledStatement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(623);
				compoundStatement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(624);
				expressionStatement();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(625);
				selectionStatement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(626);
				iterationStatement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(627);
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
		enterRule(_localctx, 104, RULE_labeledStatement);
		try {
			setState(641);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				enterOuterAlt(_localctx, 1);
				{
				setState(630);
				match(Identifier);
				setState(631);
				match(Colon);
				setState(632);
				statement();
				}
				break;
			case Case:
				enterOuterAlt(_localctx, 2);
				{
				setState(633);
				match(Case);
				setState(634);
				constantExpression();
				setState(635);
				match(Colon);
				setState(636);
				statement();
				}
				break;
			case Default:
				enterOuterAlt(_localctx, 3);
				{
				setState(638);
				match(Default);
				setState(639);
				match(Colon);
				setState(640);
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
		enterRule(_localctx, 106, RULE_compoundStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(643);
			match(LeftBrace);
			setState(645);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Semi - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
				{
				setState(644);
				blockItemList();
				}
			}

			setState(647);
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
		enterRule(_localctx, 108, RULE_blockItemList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(650); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(649);
				blockItem();
				}
				}
				setState(652); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Semi - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0) );
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
		enterRule(_localctx, 110, RULE_blockItem);
		try {
			setState(656);
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
			case Alignof:
			case LeftParen:
			case LeftBrace:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case AndAnd:
			case Not:
			case Tilde:
			case Semi:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(654);
				statement();
				}
				break;
			case T__0:
			case T__1:
			case T__2:
			case T__3:
			case T__4:
			case T__5:
			case T__7:
			case Char:
			case Byte:
			case Double:
			case Float:
			case Inline:
			case Int:
			case Word:
			case Dword:
			case Message:
			case Timer:
			case MsTimer:
			case Long:
			case Int64:
			case Void:
			case Alignas:
			case Atomic:
			case Bool:
			case Complex:
			case Noreturn:
				enterOuterAlt(_localctx, 2);
				{
				setState(655);
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
		enterRule(_localctx, 112, RULE_expressionStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(659);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
				{
				setState(658);
				expression();
				}
			}

			setState(661);
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
		enterRule(_localctx, 114, RULE_selectionStatement);
		try {
			setState(678);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case If:
				enterOuterAlt(_localctx, 1);
				{
				setState(663);
				match(If);
				setState(664);
				match(LeftParen);
				setState(665);
				expression();
				setState(666);
				match(RightParen);
				setState(667);
				statement();
				setState(670);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,79,_ctx) ) {
				case 1:
					{
					setState(668);
					match(Else);
					setState(669);
					statement();
					}
					break;
				}
				}
				break;
			case Switch:
				enterOuterAlt(_localctx, 2);
				{
				setState(672);
				match(Switch);
				setState(673);
				match(LeftParen);
				setState(674);
				expression();
				setState(675);
				match(RightParen);
				setState(676);
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
		enterRule(_localctx, 116, RULE_iterationStatement);
		try {
			setState(700);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case While:
				enterOuterAlt(_localctx, 1);
				{
				setState(680);
				match(While);
				setState(681);
				match(LeftParen);
				setState(682);
				expression();
				setState(683);
				match(RightParen);
				setState(684);
				statement();
				}
				break;
			case Do:
				enterOuterAlt(_localctx, 2);
				{
				setState(686);
				match(Do);
				setState(687);
				statement();
				setState(688);
				match(While);
				setState(689);
				match(LeftParen);
				setState(690);
				expression();
				setState(691);
				match(RightParen);
				setState(692);
				match(Semi);
				}
				break;
			case For:
				enterOuterAlt(_localctx, 3);
				{
				setState(694);
				match(For);
				setState(695);
				match(LeftParen);
				setState(696);
				forCondition();
				setState(697);
				match(RightParen);
				setState(698);
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
		enterRule(_localctx, 118, RULE_forCondition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(706);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case T__1:
			case T__2:
			case T__3:
			case T__4:
			case T__5:
			case T__7:
			case Char:
			case Byte:
			case Double:
			case Float:
			case Inline:
			case Int:
			case Word:
			case Dword:
			case Message:
			case Timer:
			case MsTimer:
			case Long:
			case Int64:
			case Void:
			case Alignas:
			case Atomic:
			case Bool:
			case Complex:
			case Noreturn:
				{
				setState(702);
				forDeclaration();
				}
				break;
			case Variables:
			case Alignof:
			case LeftParen:
			case Plus:
			case PlusPlus:
			case Minus:
			case MinusMinus:
			case AndAnd:
			case Not:
			case Tilde:
			case Semi:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				{
				setState(704);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
					{
					setState(703);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(708);
			match(Semi);
			setState(710);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
				{
				setState(709);
				forExpression();
				}
			}

			setState(712);
			match(Semi);
			setState(714);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
				{
				setState(713);
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
		enterRule(_localctx, 120, RULE_forDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(716);
			declarationSpecifiers();
			setState(718);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(717);
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
		enterRule(_localctx, 122, RULE_forExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(720);
			assignmentExpression();
			setState(725);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(721);
				match(Comma);
				setState(722);
				assignmentExpression();
				}
				}
				setState(727);
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
		enterRule(_localctx, 124, RULE_jumpStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(733);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Break:
			case Continue:
				{
				setState(728);
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
				setState(729);
				match(Return);
				setState(731);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Variables) | (1L << Alignof) | (1L << LeftParen) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
					{
					setState(730);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(735);
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
		enterRule(_localctx, 126, RULE_compilationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(738);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen))) != 0) || _la==Semi || _la==Identifier) {
				{
				setState(737);
				translationUnit();
				}
			}

			setState(740);
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
		enterRule(_localctx, 128, RULE_translationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(743); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(742);
				externalDeclaration();
				}
				}
				setState(745); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen))) != 0) || _la==Semi || _la==Identifier );
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
		enterRule(_localctx, 130, RULE_externalDeclaration);
		try {
			setState(750);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,92,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(747);
				functionDefinition();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(748);
				declaration();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(749);
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
		enterRule(_localctx, 132, RULE_functionDefinition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(753);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
				{
				setState(752);
				declarationSpecifiers();
				}
			}

			setState(755);
			declarator();
			setState(757);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
				{
				setState(756);
				declarationList();
				}
			}

			setState(759);
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
		enterRule(_localctx, 134, RULE_declarationList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(762); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(761);
				declaration();
				}
				}
				setState(764); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0) );
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
		enterRule(_localctx, 136, RULE_variableDeclarationBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(766);
			match(Variables);
			setState(767);
			match(LeftBrace);
			setState(769);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 65)) & ~0x3f) == 0 && ((1L << (_la - 65)) & ((1L << (AndAnd - 65)) | (1L << (Not - 65)) | (1L << (Tilde - 65)) | (1L << (Semi - 65)) | (1L << (Identifier - 65)) | (1L << (Constant - 65)) | (1L << (DigitSequence - 65)) | (1L << (StringLiteral - 65)))) != 0)) {
				{
				setState(768);
				blockItemList();
				}
			}

			setState(771);
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
		case 33:
			return directDeclarator_sempred((DirectDeclaratorContext)_localctx, predIndex);
		case 45:
			return directAbstractDeclarator_sempred((DirectAbstractDeclaratorContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean directDeclarator_sempred(DirectDeclaratorContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 4);
		case 1:
			return precpred(_ctx, 3);
		case 2:
			return precpred(_ctx, 2);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3c\u0308\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\3\2\3\2\3\2\6\2"+
		"\u0090\n\2\r\2\16\2\u0091\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\5\2\u009d"+
		"\n\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\5\3\u00a6\n\3\3\3\3\3\5\3\u00aa\n\3\3"+
		"\3\3\3\3\3\3\3\3\3\3\3\5\3\u00b2\n\3\3\3\3\3\3\3\3\3\7\3\u00b8\n\3\f\3"+
		"\16\3\u00bb\13\3\3\4\3\4\3\4\7\4\u00c0\n\4\f\4\16\4\u00c3\13\4\3\5\7\5"+
		"\u00c6\n\5\f\5\16\5\u00c9\13\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\5\5\u00d6\n\5\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7\u00e1\n\7\3"+
		"\b\3\b\3\b\7\b\u00e6\n\b\f\b\16\b\u00e9\13\b\3\t\3\t\3\t\7\t\u00ee\n\t"+
		"\f\t\16\t\u00f1\13\t\3\n\3\n\3\n\7\n\u00f6\n\n\f\n\16\n\u00f9\13\n\3\13"+
		"\3\13\3\13\7\13\u00fe\n\13\f\13\16\13\u0101\13\13\3\f\3\f\3\f\7\f\u0106"+
		"\n\f\f\f\16\f\u0109\13\f\3\r\3\r\3\r\7\r\u010e\n\r\f\r\16\r\u0111\13\r"+
		"\3\16\3\16\3\16\7\16\u0116\n\16\f\16\16\16\u0119\13\16\3\17\3\17\3\17"+
		"\7\17\u011e\n\17\f\17\16\17\u0121\13\17\3\20\3\20\3\20\7\20\u0126\n\20"+
		"\f\20\16\20\u0129\13\20\3\21\3\21\3\21\7\21\u012e\n\21\f\21\16\21\u0131"+
		"\13\21\3\22\3\22\3\22\3\22\3\22\3\22\5\22\u0139\n\22\3\23\3\23\3\23\3"+
		"\23\3\23\3\23\5\23\u0141\n\23\3\24\3\24\3\25\3\25\3\25\7\25\u0148\n\25"+
		"\f\25\16\25\u014b\13\25\3\26\3\26\3\27\3\27\5\27\u0151\n\27\3\27\3\27"+
		"\3\30\6\30\u0156\n\30\r\30\16\30\u0157\3\31\6\31\u015b\n\31\r\31\16\31"+
		"\u015c\3\32\3\32\3\32\5\32\u0162\n\32\3\33\3\33\3\33\7\33\u0167\n\33\f"+
		"\33\16\33\u016a\13\33\3\34\3\34\3\34\5\34\u016f\n\34\3\35\3\35\5\35\u0173"+
		"\n\35\3\36\3\36\5\36\u0177\n\36\3\37\3\37\3\37\3\37\3\37\3 \3 \3 \3 \3"+
		" \3 \5 \u0184\n \3!\3!\3!\3!\5!\u018a\n!\3!\3!\3\"\3\"\7\"\u0190\n\"\f"+
		"\"\16\"\u0193\13\"\3#\3#\3#\3#\3#\3#\3#\3#\3#\5#\u019e\n#\3#\3#\3#\5#"+
		"\u01a3\n#\3#\3#\3#\3#\3#\3#\3#\3#\3#\5#\u01ae\n#\3#\7#\u01b1\n#\f#\16"+
		"#\u01b4\13#\3$\3$\3$\6$\u01b9\n$\r$\16$\u01ba\3$\3$\5$\u01bf\n$\3%\3%"+
		"\3%\3%\3%\3%\3%\3&\5&\u01c9\n&\3&\3&\5&\u01cd\n&\7&\u01cf\n&\f&\16&\u01d2"+
		"\13&\3\'\3\'\3\'\5\'\u01d7\n\'\3\'\5\'\u01da\n\'\3(\3(\3(\3(\3(\7(\u01e1"+
		"\n(\f(\16(\u01e4\13(\3)\3)\3)\5)\u01e9\n)\3*\3*\3*\7*\u01ee\n*\f*\16*"+
		"\u01f1\13*\3+\3+\3+\3+\3+\5+\u01f8\n+\5+\u01fa\n+\3,\3,\3,\7,\u01ff\n"+
		",\f,\16,\u0202\13,\3-\3-\5-\u0206\n-\3.\3.\7.\u020a\n.\f.\16.\u020d\13"+
		".\3/\3/\3/\3/\3/\7/\u0214\n/\f/\16/\u0217\13/\3/\3/\5/\u021b\n/\3/\3/"+
		"\3/\3/\3/\3/\5/\u0223\n/\3/\3/\7/\u0227\n/\f/\16/\u022a\13/\5/\u022c\n"+
		"/\3/\3/\3/\5/\u0231\n/\3/\3/\3/\3/\3/\3/\3/\3/\5/\u023b\n/\3/\3/\7/\u023f"+
		"\n/\f/\16/\u0242\13/\7/\u0244\n/\f/\16/\u0247\13/\3\60\3\60\3\60\3\60"+
		"\5\60\u024d\n\60\3\60\3\60\5\60\u0251\n\60\3\61\5\61\u0254\n\61\3\61\3"+
		"\61\3\61\5\61\u0259\n\61\3\61\7\61\u025c\n\61\f\61\16\61\u025f\13\61\3"+
		"\62\3\62\3\62\3\63\6\63\u0265\n\63\r\63\16\63\u0266\3\64\3\64\3\64\3\64"+
		"\3\64\3\64\5\64\u026f\n\64\3\65\3\65\3\65\3\65\3\65\3\65\5\65\u0277\n"+
		"\65\3\66\3\66\3\66\3\66\3\66\3\66\3\66\3\66\3\66\3\66\3\66\5\66\u0284"+
		"\n\66\3\67\3\67\5\67\u0288\n\67\3\67\3\67\38\68\u028d\n8\r8\168\u028e"+
		"\39\39\59\u0293\n9\3:\5:\u0296\n:\3:\3:\3;\3;\3;\3;\3;\3;\3;\5;\u02a1"+
		"\n;\3;\3;\3;\3;\3;\3;\5;\u02a9\n;\3<\3<\3<\3<\3<\3<\3<\3<\3<\3<\3<\3<"+
		"\3<\3<\3<\3<\3<\3<\3<\3<\5<\u02bf\n<\3=\3=\5=\u02c3\n=\5=\u02c5\n=\3="+
		"\3=\5=\u02c9\n=\3=\3=\5=\u02cd\n=\3>\3>\5>\u02d1\n>\3?\3?\3?\7?\u02d6"+
		"\n?\f?\16?\u02d9\13?\3@\3@\3@\5@\u02de\n@\5@\u02e0\n@\3@\3@\3A\5A\u02e5"+
		"\nA\3A\3A\3B\6B\u02ea\nB\rB\16B\u02eb\3C\3C\3C\5C\u02f1\nC\3D\5D\u02f4"+
		"\nD\3D\3D\5D\u02f8\nD\3D\3D\3E\6E\u02fd\nE\rE\16E\u02fe\3F\3F\3F\5F\u0304"+
		"\nF\3F\3F\3F\2\4D\\G\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60"+
		"\62\64\668:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|~\u0080\u0082\u0084\u0086"+
		"\u0088\u008a\2\17\4\2;;==\5\2::<<FG\3\2>@\4\2::<<\3\289\3\2\64\67\3\2"+
		"WX\3\2LV\t\2\3\5\16\17\23\23\25\25\31 $$)*\5\2\6\7\30\30,,\4\2./KK\3\2"+
		"./\4\2\f\f\20\20\2\u033b\2\u009c\3\2\2\2\4\u00a9\3\2\2\2\6\u00bc\3\2\2"+
		"\2\b\u00c7\3\2\2\2\n\u00d7\3\2\2\2\f\u00e0\3\2\2\2\16\u00e2\3\2\2\2\20"+
		"\u00ea\3\2\2\2\22\u00f2\3\2\2\2\24\u00fa\3\2\2\2\26\u0102\3\2\2\2\30\u010a"+
		"\3\2\2\2\32\u0112\3\2\2\2\34\u011a\3\2\2\2\36\u0122\3\2\2\2 \u012a\3\2"+
		"\2\2\"\u0132\3\2\2\2$\u0140\3\2\2\2&\u0142\3\2\2\2(\u0144\3\2\2\2*\u014c"+
		"\3\2\2\2,\u014e\3\2\2\2.\u0155\3\2\2\2\60\u015a\3\2\2\2\62\u0161\3\2\2"+
		"\2\64\u0163\3\2\2\2\66\u016b\3\2\2\28\u0172\3\2\2\2:\u0174\3\2\2\2<\u0178"+
		"\3\2\2\2>\u0183\3\2\2\2@\u0185\3\2\2\2B\u018d\3\2\2\2D\u019d\3\2\2\2F"+
		"\u01be\3\2\2\2H\u01c0\3\2\2\2J\u01c8\3\2\2\2L\u01d3\3\2\2\2N\u01e2\3\2"+
		"\2\2P\u01e5\3\2\2\2R\u01ea\3\2\2\2T\u01f9\3\2\2\2V\u01fb\3\2\2\2X\u0203"+
		"\3\2\2\2Z\u0207\3\2\2\2\\\u022b\3\2\2\2^\u0250\3\2\2\2`\u0253\3\2\2\2"+
		"b\u0260\3\2\2\2d\u0264\3\2\2\2f\u026e\3\2\2\2h\u0276\3\2\2\2j\u0283\3"+
		"\2\2\2l\u0285\3\2\2\2n\u028c\3\2\2\2p\u0292\3\2\2\2r\u0295\3\2\2\2t\u02a8"+
		"\3\2\2\2v\u02be\3\2\2\2x\u02c4\3\2\2\2z\u02ce\3\2\2\2|\u02d2\3\2\2\2~"+
		"\u02df\3\2\2\2\u0080\u02e4\3\2\2\2\u0082\u02e9\3\2\2\2\u0084\u02f0\3\2"+
		"\2\2\u0086\u02f3\3\2\2\2\u0088\u02fc\3\2\2\2\u008a\u0300\3\2\2\2\u008c"+
		"\u009d\7[\2\2\u008d\u009d\7\\\2\2\u008e\u0090\7^\2\2\u008f\u008e\3\2\2"+
		"\2\u0090\u0091\3\2\2\2\u0091\u008f\3\2\2\2\u0091\u0092\3\2\2\2\u0092\u009d"+
		"\3\2\2\2\u0093\u0094\7.\2\2\u0094\u0095\5(\25\2\u0095\u0096\7/\2\2\u0096"+
		"\u009d\3\2\2\2\u0097\u0098\7.\2\2\u0098\u0099\5l\67\2\u0099\u009a\7/\2"+
		"\2\u009a\u009d\3\2\2\2\u009b\u009d\5\u008aF\2\u009c\u008c\3\2\2\2\u009c"+
		"\u008d\3\2\2\2\u009c\u008f\3\2\2\2\u009c\u0093\3\2\2\2\u009c\u0097\3\2"+
		"\2\2\u009c\u009b\3\2\2\2\u009d\3\3\2\2\2\u009e\u00aa\5\2\2\2\u009f\u00a0"+
		"\7.\2\2\u00a0\u00a1\5X-\2\u00a1\u00a2\7/\2\2\u00a2\u00a3\7\62\2\2\u00a3"+
		"\u00a5\5`\61\2\u00a4\u00a6\7K\2\2\u00a5\u00a4\3\2\2\2\u00a5\u00a6\3\2"+
		"\2\2\u00a6\u00a7\3\2\2\2\u00a7\u00a8\7\63\2\2\u00a8\u00aa\3\2\2\2\u00a9"+
		"\u009e\3\2\2\2\u00a9\u009f\3\2\2\2\u00aa\u00b9\3\2\2\2\u00ab\u00ac\7\60"+
		"\2\2\u00ac\u00ad\5(\25\2\u00ad\u00ae\7\61\2\2\u00ae\u00b8\3\2\2\2\u00af"+
		"\u00b1\7.\2\2\u00b0\u00b2\5\6\4\2\u00b1\u00b0\3\2\2\2\u00b1\u00b2\3\2"+
		"\2\2\u00b2\u00b3\3\2\2\2\u00b3\u00b8\7/\2\2\u00b4\u00b5\7Y\2\2\u00b5\u00b8"+
		"\7[\2\2\u00b6\u00b8\t\2\2\2\u00b7\u00ab\3\2\2\2\u00b7\u00af\3\2\2\2\u00b7"+
		"\u00b4\3\2\2\2\u00b7\u00b6\3\2\2\2\u00b8\u00bb\3\2\2\2\u00b9\u00b7\3\2"+
		"\2\2\u00b9\u00ba\3\2\2\2\u00ba\5\3\2\2\2\u00bb\u00b9\3\2\2\2\u00bc\u00c1"+
		"\5$\23\2\u00bd\u00be\7K\2\2\u00be\u00c0\5$\23\2\u00bf\u00bd\3\2\2\2\u00c0"+
		"\u00c3\3\2\2\2\u00c1\u00bf\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2\7\3\2\2\2"+
		"\u00c3\u00c1\3\2\2\2\u00c4\u00c6\t\2\2\2\u00c5\u00c4\3\2\2\2\u00c6\u00c9"+
		"\3\2\2\2\u00c7\u00c5\3\2\2\2\u00c7\u00c8\3\2\2\2\u00c8\u00d5\3\2\2\2\u00c9"+
		"\u00c7\3\2\2\2\u00ca\u00d6\5\4\3\2\u00cb\u00cc\5\n\6\2\u00cc\u00cd\5\f"+
		"\7\2\u00cd\u00d6\3\2\2\2\u00ce\u00cf\7\'\2\2\u00cf\u00d0\7.\2\2\u00d0"+
		"\u00d1\5X-\2\u00d1\u00d2\7/\2\2\u00d2\u00d6\3\2\2\2\u00d3\u00d4\7C\2\2"+
		"\u00d4\u00d6\7[\2\2\u00d5\u00ca\3\2\2\2\u00d5\u00cb\3\2\2\2\u00d5\u00ce"+
		"\3\2\2\2\u00d5\u00d3\3\2\2\2\u00d6\t\3\2\2\2\u00d7\u00d8\t\3\2\2\u00d8"+
		"\13\3\2\2\2\u00d9\u00da\7.\2\2\u00da\u00db\5X-\2\u00db\u00dc\7/\2\2\u00dc"+
		"\u00dd\5\f\7\2\u00dd\u00e1\3\2\2\2\u00de\u00e1\5\b\5\2\u00df\u00e1\7]"+
		"\2\2\u00e0\u00d9\3\2\2\2\u00e0\u00de\3\2\2\2\u00e0\u00df\3\2\2\2\u00e1"+
		"\r\3\2\2\2\u00e2\u00e7\5\f\7\2\u00e3\u00e4\t\4\2\2\u00e4\u00e6\5\f\7\2"+
		"\u00e5\u00e3\3\2\2\2\u00e6\u00e9\3\2\2\2\u00e7\u00e5\3\2\2\2\u00e7\u00e8"+
		"\3\2\2\2\u00e8\17\3\2\2\2\u00e9\u00e7\3\2\2\2\u00ea\u00ef\5\16\b\2\u00eb"+
		"\u00ec\t\5\2\2\u00ec\u00ee\5\16\b\2\u00ed\u00eb\3\2\2\2\u00ee\u00f1\3"+
		"\2\2\2\u00ef\u00ed\3\2\2\2\u00ef\u00f0\3\2\2\2\u00f0\21\3\2\2\2\u00f1"+
		"\u00ef\3\2\2\2\u00f2\u00f7\5\20\t\2\u00f3\u00f4\t\6\2\2\u00f4\u00f6\5"+
		"\20\t\2\u00f5\u00f3\3\2\2\2\u00f6\u00f9\3\2\2\2\u00f7\u00f5\3\2\2\2\u00f7"+
		"\u00f8\3\2\2\2\u00f8\23\3\2\2\2\u00f9\u00f7\3\2\2\2\u00fa\u00ff\5\22\n"+
		"\2\u00fb\u00fc\t\7\2\2\u00fc\u00fe\5\22\n\2\u00fd\u00fb\3\2\2\2\u00fe"+
		"\u0101\3\2\2\2\u00ff\u00fd\3\2\2\2\u00ff\u0100\3\2\2\2\u0100\25\3\2\2"+
		"\2\u0101\u00ff\3\2\2\2\u0102\u0107\5\24\13\2\u0103\u0104\t\b\2\2\u0104"+
		"\u0106\5\24\13\2\u0105\u0103\3\2\2\2\u0106\u0109\3\2\2\2\u0107\u0105\3"+
		"\2\2\2\u0107\u0108\3\2\2\2\u0108\27\3\2\2\2\u0109\u0107\3\2\2\2\u010a"+
		"\u010f\5\26\f\2\u010b\u010c\7A\2\2\u010c\u010e\5\26\f\2\u010d\u010b\3"+
		"\2\2\2\u010e\u0111\3\2\2\2\u010f\u010d\3\2\2\2\u010f\u0110\3\2\2\2\u0110"+
		"\31\3\2\2\2\u0111\u010f\3\2\2\2\u0112\u0117\5\30\r\2\u0113\u0114\7E\2"+
		"\2\u0114\u0116\5\30\r\2\u0115\u0113\3\2\2\2\u0116\u0119\3\2\2\2\u0117"+
		"\u0115\3\2\2\2\u0117\u0118\3\2\2\2\u0118\33\3\2\2\2\u0119\u0117\3\2\2"+
		"\2\u011a\u011f\5\32\16\2\u011b\u011c\7B\2\2\u011c\u011e\5\32\16\2\u011d"+
		"\u011b\3\2\2\2\u011e\u0121\3\2\2\2\u011f\u011d\3\2\2\2\u011f\u0120\3\2"+
		"\2\2\u0120\35\3\2\2\2\u0121\u011f\3\2\2\2\u0122\u0127\5\34\17\2\u0123"+
		"\u0124\7C\2\2\u0124\u0126\5\34\17\2\u0125\u0123\3\2\2\2\u0126\u0129\3"+
		"\2\2\2\u0127\u0125\3\2\2\2\u0127\u0128\3\2\2\2\u0128\37\3\2\2\2\u0129"+
		"\u0127\3\2\2\2\u012a\u012f\5\36\20\2\u012b\u012c\7D\2\2\u012c\u012e\5"+
		"\36\20\2\u012d\u012b\3\2\2\2\u012e\u0131\3\2\2\2\u012f\u012d\3\2\2\2\u012f"+
		"\u0130\3\2\2\2\u0130!\3\2\2\2\u0131\u012f\3\2\2\2\u0132\u0138\5 \21\2"+
		"\u0133\u0134\7H\2\2\u0134\u0135\5(\25\2\u0135\u0136\7I\2\2\u0136\u0137"+
		"\5\"\22\2\u0137\u0139\3\2\2\2\u0138\u0133\3\2\2\2\u0138\u0139\3\2\2\2"+
		"\u0139#\3\2\2\2\u013a\u0141\5\"\22\2\u013b\u013c\5\b\5\2\u013c\u013d\5"+
		"&\24\2\u013d\u013e\5$\23\2\u013e\u0141\3\2\2\2\u013f\u0141\7]\2\2\u0140"+
		"\u013a\3\2\2\2\u0140\u013b\3\2\2\2\u0140\u013f\3\2\2\2\u0141%\3\2\2\2"+
		"\u0142\u0143\t\t\2\2\u0143\'\3\2\2\2\u0144\u0149\5$\23\2\u0145\u0146\7"+
		"K\2\2\u0146\u0148\5$\23\2\u0147\u0145\3\2\2\2\u0148\u014b\3\2\2\2\u0149"+
		"\u0147\3\2\2\2\u0149\u014a\3\2\2\2\u014a)\3\2\2\2\u014b\u0149\3\2\2\2"+
		"\u014c\u014d\5\"\22\2\u014d+\3\2\2\2\u014e\u0150\5.\30\2\u014f\u0151\5"+
		"\64\33\2\u0150\u014f\3\2\2\2\u0150\u0151\3\2\2\2\u0151\u0152\3\2\2\2\u0152"+
		"\u0153\7J\2\2\u0153-\3\2\2\2\u0154\u0156\5\62\32\2\u0155\u0154\3\2\2\2"+
		"\u0156\u0157\3\2\2\2\u0157\u0155\3\2\2\2\u0157\u0158\3\2\2\2\u0158/\3"+
		"\2\2\2\u0159\u015b\5\62\32\2\u015a\u0159\3\2\2\2\u015b\u015c\3\2\2\2\u015c"+
		"\u015a\3\2\2\2\u015c\u015d\3\2\2\2\u015d\61\3\2\2\2\u015e\u0162\58\35"+
		"\2\u015f\u0162\5> \2\u0160\u0162\5@!\2\u0161\u015e\3\2\2\2\u0161\u015f"+
		"\3\2\2\2\u0161\u0160\3\2\2\2\u0162\63\3\2\2\2\u0163\u0168\5\66\34\2\u0164"+
		"\u0165\7K\2\2\u0165\u0167\5\66\34\2\u0166\u0164\3\2\2\2\u0167\u016a\3"+
		"\2\2\2\u0168\u0166\3\2\2\2\u0168\u0169\3\2\2\2\u0169\65\3\2\2\2\u016a"+
		"\u0168\3\2\2\2\u016b\u016e\5B\"\2\u016c\u016d\7L\2\2\u016d\u016f\5^\60"+
		"\2\u016e\u016c\3\2\2\2\u016e\u016f\3\2\2\2\u016f\67\3\2\2\2\u0170\u0173"+
		"\t\n\2\2\u0171\u0173\5<\37\2\u0172\u0170\3\2\2\2\u0172\u0171\3\2\2\2\u0173"+
		"9\3\2\2\2\u0174\u0176\58\35\2\u0175\u0177\5:\36\2\u0176\u0175\3\2\2\2"+
		"\u0176\u0177\3\2\2\2\u0177;\3\2\2\2\u0178\u0179\7(\2\2\u0179\u017a\7."+
		"\2\2\u017a\u017b\5X-\2\u017b\u017c\7/\2\2\u017c=\3\2\2\2\u017d\u0184\t"+
		"\13\2\2\u017e\u0184\5H%\2\u017f\u0180\7\b\2\2\u0180\u0181\7.\2\2\u0181"+
		"\u0182\7[\2\2\u0182\u0184\7/\2\2\u0183\u017d\3\2\2\2\u0183\u017e\3\2\2"+
		"\2\u0183\u017f\3\2\2\2\u0184?\3\2\2\2\u0185\u0186\7&\2\2\u0186\u0189\7"+
		".\2\2\u0187\u018a\5X-\2\u0188\u018a\5*\26\2\u0189\u0187\3\2\2\2\u0189"+
		"\u0188\3\2\2\2\u018a\u018b\3\2\2\2\u018b\u018c\7/\2\2\u018cA\3\2\2\2\u018d"+
		"\u0191\5D#\2\u018e\u0190\5F$\2\u018f\u018e\3\2\2\2\u0190\u0193\3\2\2\2"+
		"\u0191\u018f\3\2\2\2\u0191\u0192\3\2\2\2\u0192C\3\2\2\2\u0193\u0191\3"+
		"\2\2\2\u0194\u0195\b#\1\2\u0195\u019e\7[\2\2\u0196\u0197\7.\2\2\u0197"+
		"\u0198\5B\"\2\u0198\u0199\7/\2\2\u0199\u019e\3\2\2\2\u019a\u019b\7[\2"+
		"\2\u019b\u019c\7I\2\2\u019c\u019e\7]\2\2\u019d\u0194\3\2\2\2\u019d\u0196"+
		"\3\2\2\2\u019d\u019a\3\2\2\2\u019e\u01b2\3\2\2\2\u019f\u01a0\f\6\2\2\u01a0"+
		"\u01a2\7\60\2\2\u01a1\u01a3\5$\23\2\u01a2\u01a1\3\2\2\2\u01a2\u01a3\3"+
		"\2\2\2\u01a3\u01a4\3\2\2\2\u01a4\u01b1\7\61\2\2\u01a5\u01a6\f\5\2\2\u01a6"+
		"\u01a7\7.\2\2\u01a7\u01a8\5P)\2\u01a8\u01a9\7/\2\2\u01a9\u01b1\3\2\2\2"+
		"\u01aa\u01ab\f\4\2\2\u01ab\u01ad\7.\2\2\u01ac\u01ae\5V,\2\u01ad\u01ac"+
		"\3\2\2\2\u01ad\u01ae\3\2\2\2\u01ae\u01af\3\2\2\2\u01af\u01b1\7/\2\2\u01b0"+
		"\u019f\3\2\2\2\u01b0\u01a5\3\2\2\2\u01b0\u01aa\3\2\2\2\u01b1\u01b4\3\2"+
		"\2\2\u01b2\u01b0\3\2\2\2\u01b2\u01b3\3\2\2\2\u01b3E\3\2\2\2\u01b4\u01b2"+
		"\3\2\2\2\u01b5\u01b6\7\t\2\2\u01b6\u01b8\7.\2\2\u01b7\u01b9\7^\2\2\u01b8"+
		"\u01b7\3\2\2\2\u01b9\u01ba\3\2\2\2\u01ba\u01b8\3\2\2\2\u01ba\u01bb\3\2"+
		"\2\2\u01bb\u01bc\3\2\2\2\u01bc\u01bf\7/\2\2\u01bd\u01bf\5H%\2\u01be\u01b5"+
		"\3\2\2\2\u01be\u01bd\3\2\2\2\u01bfG\3\2\2\2\u01c0\u01c1\7\n\2\2\u01c1"+
		"\u01c2\7.\2\2\u01c2\u01c3\7.\2\2\u01c3\u01c4\5J&\2\u01c4\u01c5\7/\2\2"+
		"\u01c5\u01c6\7/\2\2\u01c6I\3\2\2\2\u01c7\u01c9\5L\'\2\u01c8\u01c7\3\2"+
		"\2\2\u01c8\u01c9\3\2\2\2\u01c9\u01d0\3\2\2\2\u01ca\u01cc\7K\2\2\u01cb"+
		"\u01cd\5L\'\2\u01cc\u01cb\3\2\2\2\u01cc\u01cd\3\2\2\2\u01cd\u01cf\3\2"+
		"\2\2\u01ce\u01ca\3\2\2\2\u01cf\u01d2\3\2\2\2\u01d0\u01ce\3\2\2\2\u01d0"+
		"\u01d1\3\2\2\2\u01d1K\3\2\2\2\u01d2\u01d0\3\2\2\2\u01d3\u01d9\n\f\2\2"+
		"\u01d4\u01d6\7.\2\2\u01d5\u01d7\5\6\4\2\u01d6\u01d5\3\2\2\2\u01d6\u01d7"+
		"\3\2\2\2\u01d7\u01d8\3\2\2\2\u01d8\u01da\7/\2\2\u01d9\u01d4\3\2\2\2\u01d9"+
		"\u01da\3\2\2\2\u01daM\3\2\2\2\u01db\u01e1\n\r\2\2\u01dc\u01dd\7.\2\2\u01dd"+
		"\u01de\5N(\2\u01de\u01df\7/\2\2\u01df\u01e1\3\2\2\2\u01e0\u01db\3\2\2"+
		"\2\u01e0\u01dc\3\2\2\2\u01e1\u01e4\3\2\2\2\u01e2\u01e0\3\2\2\2\u01e2\u01e3"+
		"\3\2\2\2\u01e3O\3\2\2\2\u01e4\u01e2\3\2\2\2\u01e5\u01e8\5R*\2\u01e6\u01e7"+
		"\7K\2\2\u01e7\u01e9\7Z\2\2\u01e8\u01e6\3\2\2\2\u01e8\u01e9\3\2\2\2\u01e9"+
		"Q\3\2\2\2\u01ea\u01ef\5T+\2\u01eb\u01ec\7K\2\2\u01ec\u01ee\5T+\2\u01ed"+
		"\u01eb\3\2\2\2\u01ee\u01f1\3\2\2\2\u01ef\u01ed\3\2\2\2\u01ef\u01f0\3\2"+
		"\2\2\u01f0S\3\2\2\2\u01f1\u01ef\3\2\2\2\u01f2\u01f3\5.\30\2\u01f3\u01f4"+
		"\5B\"\2\u01f4\u01fa\3\2\2\2\u01f5\u01f7\5\60\31\2\u01f6\u01f8\5Z.\2\u01f7"+
		"\u01f6\3\2\2\2\u01f7\u01f8\3\2\2\2\u01f8\u01fa\3\2\2\2\u01f9\u01f2\3\2"+
		"\2\2\u01f9\u01f5\3\2\2\2\u01faU\3\2\2\2\u01fb\u0200\7[\2\2\u01fc\u01fd"+
		"\7K\2\2\u01fd\u01ff\7[\2\2\u01fe\u01fc\3\2\2\2\u01ff\u0202\3\2\2\2\u0200"+
		"\u01fe\3\2\2\2\u0200\u0201\3\2\2\2\u0201W\3\2\2\2\u0202\u0200\3\2\2\2"+
		"\u0203\u0205\5:\36\2\u0204\u0206\5Z.\2\u0205\u0204\3\2\2\2\u0205\u0206"+
		"\3\2\2\2\u0206Y\3\2\2\2\u0207\u020b\5\\/\2\u0208\u020a\5F$\2\u0209\u0208"+
		"\3\2\2\2\u020a\u020d\3\2\2\2\u020b\u0209\3\2\2\2\u020b\u020c\3\2\2\2\u020c"+
		"[\3\2\2\2\u020d\u020b\3\2\2\2\u020e\u020f\b/\1\2\u020f\u0210\7.\2\2\u0210"+
		"\u0211\5Z.\2\u0211\u0215\7/\2\2\u0212\u0214\5F$\2\u0213\u0212\3\2\2\2"+
		"\u0214\u0217\3\2\2\2\u0215\u0213\3\2\2\2\u0215\u0216\3\2\2\2\u0216\u022c"+
		"\3\2\2\2\u0217\u0215\3\2\2\2\u0218\u021a\7\60\2\2\u0219\u021b\5$\23\2"+
		"\u021a\u0219\3\2\2\2\u021a\u021b\3\2\2\2\u021b\u021c\3\2\2\2\u021c\u022c"+
		"\7\61\2\2\u021d\u021e\7\60\2\2\u021e\u021f\7>\2\2\u021f\u022c\7\61\2\2"+
		"\u0220\u0222\7.\2\2\u0221\u0223\5P)\2\u0222\u0221\3\2\2\2\u0222\u0223"+
		"\3\2\2\2\u0223\u0224\3\2\2\2\u0224\u0228\7/\2\2\u0225\u0227\5F$\2\u0226"+
		"\u0225\3\2\2\2\u0227\u022a\3\2\2\2\u0228\u0226\3\2\2\2\u0228\u0229\3\2"+
		"\2\2\u0229\u022c\3\2\2\2\u022a\u0228\3\2\2\2\u022b\u020e\3\2\2\2\u022b"+
		"\u0218\3\2\2\2\u022b\u021d\3\2\2\2\u022b\u0220\3\2\2\2\u022c\u0245\3\2"+
		"\2\2\u022d\u022e\f\5\2\2\u022e\u0230\7\60\2\2\u022f\u0231\5$\23\2\u0230"+
		"\u022f\3\2\2\2\u0230\u0231\3\2\2\2\u0231\u0232\3\2\2\2\u0232\u0244\7\61"+
		"\2\2\u0233\u0234\f\4\2\2\u0234\u0235\7\60\2\2\u0235\u0236\7>\2\2\u0236"+
		"\u0244\7\61\2\2\u0237\u0238\f\3\2\2\u0238\u023a\7.\2\2\u0239\u023b\5P"+
		")\2\u023a\u0239\3\2\2\2\u023a\u023b\3\2\2\2\u023b\u023c\3\2\2\2\u023c"+
		"\u0240\7/\2\2\u023d\u023f\5F$\2\u023e\u023d\3\2\2\2\u023f\u0242\3\2\2"+
		"\2\u0240\u023e\3\2\2\2\u0240\u0241\3\2\2\2\u0241\u0244\3\2\2\2\u0242\u0240"+
		"\3\2\2\2\u0243\u022d\3\2\2\2\u0243\u0233\3\2\2\2\u0243\u0237\3\2\2\2\u0244"+
		"\u0247\3\2\2\2\u0245\u0243\3\2\2\2\u0245\u0246\3\2\2\2\u0246]\3\2\2\2"+
		"\u0247\u0245\3\2\2\2\u0248\u0251\5$\23\2\u0249\u024a\7\62\2\2\u024a\u024c"+
		"\5`\61\2\u024b\u024d\7K\2\2\u024c\u024b\3\2\2\2\u024c\u024d\3\2\2\2\u024d"+
		"\u024e\3\2\2\2\u024e\u024f\7\63\2\2\u024f\u0251\3\2\2\2\u0250\u0248\3"+
		"\2\2\2\u0250\u0249\3\2\2\2\u0251_\3\2\2\2\u0252\u0254\5b\62\2\u0253\u0252"+
		"\3\2\2\2\u0253\u0254\3\2\2\2\u0254\u0255\3\2\2\2\u0255\u025d\5^\60\2\u0256"+
		"\u0258\7K\2\2\u0257\u0259\5b\62\2\u0258\u0257\3\2\2\2\u0258\u0259\3\2"+
		"\2\2\u0259\u025a\3\2\2\2\u025a\u025c\5^\60\2\u025b\u0256\3\2\2\2\u025c"+
		"\u025f\3\2\2\2\u025d\u025b\3\2\2\2\u025d\u025e\3\2\2\2\u025ea\3\2\2\2"+
		"\u025f\u025d\3\2\2\2\u0260\u0261\5d\63\2\u0261\u0262\7L\2\2\u0262c\3\2"+
		"\2\2\u0263\u0265\5f\64\2\u0264\u0263\3\2\2\2\u0265\u0266\3\2\2\2\u0266"+
		"\u0264\3\2\2\2\u0266\u0267\3\2\2\2\u0267e\3\2\2\2\u0268\u0269\7\60\2\2"+
		"\u0269\u026a\5*\26\2\u026a\u026b\7\61\2\2\u026b\u026f\3\2\2\2\u026c\u026d"+
		"\7Y\2\2\u026d\u026f\7[\2\2\u026e\u0268\3\2\2\2\u026e\u026c\3\2\2\2\u026f"+
		"g\3\2\2\2\u0270\u0277\5j\66\2\u0271\u0277\5l\67\2\u0272\u0277\5r:\2\u0273"+
		"\u0277\5t;\2\u0274\u0277\5v<\2\u0275\u0277\5~@\2\u0276\u0270\3\2\2\2\u0276"+
		"\u0271\3\2\2\2\u0276\u0272\3\2\2\2\u0276\u0273\3\2\2\2\u0276\u0274\3\2"+
		"\2\2\u0276\u0275\3\2\2\2\u0277i\3\2\2\2\u0278\u0279\7[\2\2\u0279\u027a"+
		"\7I\2\2\u027a\u0284\5h\65\2\u027b\u027c\7\r\2\2\u027c\u027d\5*\26\2\u027d"+
		"\u027e\7I\2\2\u027e\u027f\5h\65\2\u027f\u0284\3\2\2\2\u0280\u0281\7\21"+
		"\2\2\u0281\u0282\7I\2\2\u0282\u0284\5h\65\2\u0283\u0278\3\2\2\2\u0283"+
		"\u027b\3\2\2\2\u0283\u0280\3\2\2\2\u0284k\3\2\2\2\u0285\u0287\7\62\2\2"+
		"\u0286\u0288\5n8\2\u0287\u0286\3\2\2\2\u0287\u0288\3\2\2\2\u0288\u0289"+
		"\3\2\2\2\u0289\u028a\7\63\2\2\u028am\3\2\2\2\u028b\u028d\5p9\2\u028c\u028b"+
		"\3\2\2\2\u028d\u028e\3\2\2\2\u028e\u028c\3\2\2\2\u028e\u028f\3\2\2\2\u028f"+
		"o\3\2\2\2\u0290\u0293\5h\65\2\u0291\u0293\5,\27\2\u0292\u0290\3\2\2\2"+
		"\u0292\u0291\3\2\2\2\u0293q\3\2\2\2\u0294\u0296\5(\25\2\u0295\u0294\3"+
		"\2\2\2\u0295\u0296\3\2\2\2\u0296\u0297\3\2\2\2\u0297\u0298\7J\2\2\u0298"+
		"s\3\2\2\2\u0299\u029a\7\27\2\2\u029a\u029b\7.\2\2\u029b\u029c\5(\25\2"+
		"\u029c\u029d\7/\2\2\u029d\u02a0\5h\65\2\u029e\u029f\7\24\2\2\u029f\u02a1"+
		"\5h\65\2\u02a0\u029e\3\2\2\2\u02a0\u02a1\3\2\2\2\u02a1\u02a9\3\2\2\2\u02a2"+
		"\u02a3\7#\2\2\u02a3\u02a4\7.\2\2\u02a4\u02a5\5(\25\2\u02a5\u02a6\7/\2"+
		"\2\u02a6\u02a7\5h\65\2\u02a7\u02a9\3\2\2\2\u02a8\u0299\3\2\2\2\u02a8\u02a2"+
		"\3\2\2\2\u02a9u\3\2\2\2\u02aa\u02ab\7%\2\2\u02ab\u02ac\7.\2\2\u02ac\u02ad"+
		"\5(\25\2\u02ad\u02ae\7/\2\2\u02ae\u02af\5h\65\2\u02af\u02bf\3\2\2\2\u02b0"+
		"\u02b1\7\22\2\2\u02b1\u02b2\5h\65\2\u02b2\u02b3\7%\2\2\u02b3\u02b4\7."+
		"\2\2\u02b4\u02b5\5(\25\2\u02b5\u02b6\7/\2\2\u02b6\u02b7\7J\2\2\u02b7\u02bf"+
		"\3\2\2\2\u02b8\u02b9\7\26\2\2\u02b9\u02ba\7.\2\2\u02ba\u02bb\5x=\2\u02bb"+
		"\u02bc\7/\2\2\u02bc\u02bd\5h\65\2\u02bd\u02bf\3\2\2\2\u02be\u02aa\3\2"+
		"\2\2\u02be\u02b0\3\2\2\2\u02be\u02b8\3\2\2\2\u02bfw\3\2\2\2\u02c0\u02c5"+
		"\5z>\2\u02c1\u02c3\5(\25\2\u02c2\u02c1\3\2\2\2\u02c2\u02c3\3\2\2\2\u02c3"+
		"\u02c5\3\2\2\2\u02c4\u02c0\3\2\2\2\u02c4\u02c2\3\2\2\2\u02c5\u02c6\3\2"+
		"\2\2\u02c6\u02c8\7J\2\2\u02c7\u02c9\5|?\2\u02c8\u02c7\3\2\2\2\u02c8\u02c9"+
		"\3\2\2\2\u02c9\u02ca\3\2\2\2\u02ca\u02cc\7J\2\2\u02cb\u02cd\5|?\2\u02cc"+
		"\u02cb\3\2\2\2\u02cc\u02cd\3\2\2\2\u02cdy\3\2\2\2\u02ce\u02d0\5.\30\2"+
		"\u02cf\u02d1\5\64\33\2\u02d0\u02cf\3\2\2\2\u02d0\u02d1\3\2\2\2\u02d1{"+
		"\3\2\2\2\u02d2\u02d7\5$\23\2\u02d3\u02d4\7K\2\2\u02d4\u02d6\5$\23\2\u02d5"+
		"\u02d3\3\2\2\2\u02d6\u02d9\3\2\2\2\u02d7\u02d5\3\2\2\2\u02d7\u02d8\3\2"+
		"\2\2\u02d8}\3\2\2\2\u02d9\u02d7\3\2\2\2\u02da\u02e0\t\16\2\2\u02db\u02dd"+
		"\7\"\2\2\u02dc\u02de\5(\25\2\u02dd\u02dc\3\2\2\2\u02dd\u02de\3\2\2\2\u02de"+
		"\u02e0\3\2\2\2\u02df\u02da\3\2\2\2\u02df\u02db\3\2\2\2\u02e0\u02e1\3\2"+
		"\2\2\u02e1\u02e2\7J\2\2\u02e2\177\3\2\2\2\u02e3\u02e5\5\u0082B\2\u02e4"+
		"\u02e3\3\2\2\2\u02e4\u02e5\3\2\2\2\u02e5\u02e6\3\2\2\2\u02e6\u02e7\7\2"+
		"\2\3\u02e7\u0081\3\2\2\2\u02e8\u02ea\5\u0084C\2\u02e9\u02e8\3\2\2\2\u02ea"+
		"\u02eb\3\2\2\2\u02eb\u02e9\3\2\2\2\u02eb\u02ec\3\2\2\2\u02ec\u0083\3\2"+
		"\2\2\u02ed\u02f1\5\u0086D\2\u02ee\u02f1\5,\27\2\u02ef\u02f1\7J\2\2\u02f0"+
		"\u02ed\3\2\2\2\u02f0\u02ee\3\2\2\2\u02f0\u02ef\3\2\2\2\u02f1\u0085\3\2"+
		"\2\2\u02f2\u02f4\5.\30\2\u02f3\u02f2\3\2\2\2\u02f3\u02f4\3\2\2\2\u02f4"+
		"\u02f5\3\2\2\2\u02f5\u02f7\5B\"\2\u02f6\u02f8\5\u0088E\2\u02f7\u02f6\3"+
		"\2\2\2\u02f7\u02f8\3\2\2\2\u02f8\u02f9\3\2\2\2\u02f9\u02fa\5l\67\2\u02fa"+
		"\u0087\3\2\2\2\u02fb\u02fd\5,\27\2\u02fc\u02fb\3\2\2\2\u02fd\u02fe\3\2"+
		"\2\2\u02fe\u02fc\3\2\2\2\u02fe\u02ff\3\2\2\2\u02ff\u0089\3\2\2\2\u0300"+
		"\u0301\7\13\2\2\u0301\u0303\7\62\2\2\u0302\u0304\5n8\2\u0303\u0302\3\2"+
		"\2\2\u0303\u0304\3\2\2\2\u0304\u0305\3\2\2\2\u0305\u0306\7\63\2\2\u0306"+
		"\u008b\3\2\2\2c\u0091\u009c\u00a5\u00a9\u00b1\u00b7\u00b9\u00c1\u00c7"+
		"\u00d5\u00e0\u00e7\u00ef\u00f7\u00ff\u0107\u010f\u0117\u011f\u0127\u012f"+
		"\u0138\u0140\u0149\u0150\u0157\u015c\u0161\u0168\u016e\u0172\u0176\u0183"+
		"\u0189\u0191\u019d\u01a2\u01ad\u01b0\u01b2\u01ba\u01be\u01c8\u01cc\u01d0"+
		"\u01d6\u01d9\u01e0\u01e2\u01e8\u01ef\u01f7\u01f9\u0200\u0205\u020b\u0215"+
		"\u021a\u0222\u0228\u022b\u0230\u023a\u0240\u0243\u0245\u024c\u0250\u0253"+
		"\u0258\u025d\u0266\u026e\u0276\u0283\u0287\u028e\u0292\u0295\u02a0\u02a8"+
		"\u02be\u02c2\u02c4\u02c8\u02cc\u02d0\u02d7\u02dd\u02df\u02e4\u02eb\u02f0"+
		"\u02f3\u02f7\u02fe\u0303";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}