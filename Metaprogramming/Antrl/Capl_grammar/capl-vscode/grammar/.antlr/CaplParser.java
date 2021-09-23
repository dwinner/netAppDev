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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, Break=9, 
		Case=10, Char=11, Byte=12, Continue=13, Default=14, Do=15, Double=16, 
		Else=17, Float=18, For=19, If=20, Inline=21, Int=22, Word=23, Dword=24, 
		Message=25, Timer=26, MsTimer=27, Long=28, Int64=29, Restrict=30, Return=31, 
		Switch=32, Void=33, While=34, Alignas=35, Alignof=36, Atomic=37, Bool=38, 
		Complex=39, Imaginary=40, Noreturn=41, ThreadLocal=42, LeftParen=43, RightParen=44, 
		LeftBracket=45, RightBracket=46, LeftBrace=47, RightBrace=48, Less=49, 
		LessEqual=50, Greater=51, GreaterEqual=52, LeftShift=53, RightShift=54, 
		Plus=55, PlusPlus=56, Minus=57, MinusMinus=58, Star=59, Div=60, Mod=61, 
		And=62, Or=63, AndAnd=64, OrOr=65, Caret=66, Not=67, Tilde=68, Question=69, 
		Colon=70, Semi=71, Comma=72, Assign=73, StarAssign=74, DivAssign=75, ModAssign=76, 
		PlusAssign=77, MinusAssign=78, LeftShiftAssign=79, RightShiftAssign=80, 
		AndAssign=81, XorAssign=82, OrAssign=83, Equal=84, NotEqual=85, Dot=86, 
		Ellipsis=87, Identifier=88, Constant=89, DigitSequence=90, StringLiteral=91, 
		AsmBlock=92, Whitespace=93, Newline=94, BlockComment=95, LineComment=96;
	public static final int
		RULE_primaryExpression = 0, RULE_genericAssocList = 1, RULE_genericAssociation = 2, 
		RULE_postfixExpression = 3, RULE_argumentExpressionList = 4, RULE_unaryExpression = 5, 
		RULE_unaryOperator = 6, RULE_castExpression = 7, RULE_multiplicativeExpression = 8, 
		RULE_additiveExpression = 9, RULE_shiftExpression = 10, RULE_relationalExpression = 11, 
		RULE_equalityExpression = 12, RULE_andExpression = 13, RULE_exclusiveOrExpression = 14, 
		RULE_inclusiveOrExpression = 15, RULE_logicalAndExpression = 16, RULE_logicalOrExpression = 17, 
		RULE_conditionalExpression = 18, RULE_assignmentExpression = 19, RULE_assignmentOperator = 20, 
		RULE_expression = 21, RULE_constantExpression = 22, RULE_declaration = 23, 
		RULE_declarationSpecifiers = 24, RULE_declarationSpecifiers2 = 25, RULE_declarationSpecifier = 26, 
		RULE_initDeclaratorList = 27, RULE_initDeclarator = 28, RULE_typeSpecifier = 29, 
		RULE_specifierQualifierList = 30, RULE_atomicTypeSpecifier = 31, RULE_functionSpecifier = 32, 
		RULE_alignmentSpecifier = 33, RULE_declarator = 34, RULE_directDeclarator = 35, 
		RULE_gccDeclaratorExtension = 36, RULE_gccAttributeSpecifier = 37, RULE_gccAttributeList = 38, 
		RULE_gccAttribute = 39, RULE_nestedParenthesesBlock = 40, RULE_parameterTypeList = 41, 
		RULE_parameterList = 42, RULE_parameterDeclaration = 43, RULE_identifierList = 44, 
		RULE_typeName = 45, RULE_abstractDeclarator = 46, RULE_directAbstractDeclarator = 47, 
		RULE_initializer = 48, RULE_initializerList = 49, RULE_designation = 50, 
		RULE_designatorList = 51, RULE_designator = 52, RULE_statement = 53, RULE_labeledStatement = 54, 
		RULE_compoundStatement = 55, RULE_blockItemList = 56, RULE_blockItem = 57, 
		RULE_expressionStatement = 58, RULE_selectionStatement = 59, RULE_iterationStatement = 60, 
		RULE_forCondition = 61, RULE_forDeclaration = 62, RULE_forExpression = 63, 
		RULE_jumpStatement = 64, RULE_compilationUnit = 65, RULE_translationUnit = 66, 
		RULE_externalDeclaration = 67, RULE_functionDefinition = 68, RULE_declarationList = 69;
	private static String[] makeRuleNames() {
		return new String[] {
			"primaryExpression", "genericAssocList", "genericAssociation", "postfixExpression", 
			"argumentExpressionList", "unaryExpression", "unaryOperator", "castExpression", 
			"multiplicativeExpression", "additiveExpression", "shiftExpression", 
			"relationalExpression", "equalityExpression", "andExpression", "exclusiveOrExpression", 
			"inclusiveOrExpression", "logicalAndExpression", "logicalOrExpression", 
			"conditionalExpression", "assignmentExpression", "assignmentOperator", 
			"expression", "constantExpression", "declaration", "declarationSpecifiers", 
			"declarationSpecifiers2", "declarationSpecifier", "initDeclaratorList", 
			"initDeclarator", "typeSpecifier", "specifierQualifierList", "atomicTypeSpecifier", 
			"functionSpecifier", "alignmentSpecifier", "declarator", "directDeclarator", 
			"gccDeclaratorExtension", "gccAttributeSpecifier", "gccAttributeList", 
			"gccAttribute", "nestedParenthesesBlock", "parameterTypeList", "parameterList", 
			"parameterDeclaration", "identifierList", "typeName", "abstractDeclarator", 
			"directAbstractDeclarator", "initializer", "initializerList", "designation", 
			"designatorList", "designator", "statement", "labeledStatement", "compoundStatement", 
			"blockItemList", "blockItem", "expressionStatement", "selectionStatement", 
			"iterationStatement", "forCondition", "forDeclaration", "forExpression", 
			"jumpStatement", "compilationUnit", "translationUnit", "externalDeclaration", 
			"functionDefinition", "declarationList"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'__m128'", "'__m128d'", "'__m128i'", "'__inline__'", "'__stdcall'", 
			"'__declspec'", "'__asm'", "'__attribute__'", "'break'", "'case'", "'char'", 
			"'byte'", "'continue'", "'default'", "'do'", "'double'", "'else'", "'float'", 
			"'for'", "'if'", "'inline'", "'int'", "'word'", "'dword'", "'message'", 
			"'timer'", "'msTimer'", "'long'", "'int64'", "'restrict'", "'return'", 
			"'switch'", "'void'", "'while'", "'_Alignas'", "'_Alignof'", "'_Atomic'", 
			"'_Bool'", "'_Complex'", "'_Imaginary'", "'_Noreturn'", "'_Thread_local'", 
			"'('", "')'", "'['", "']'", "'{'", "'}'", "'<'", "'<='", "'>'", "'>='", 
			"'<<'", "'>>'", "'+'", "'++'", "'-'", "'--'", "'*'", "'/'", "'%'", "'&'", 
			"'|'", "'&&'", "'||'", "'^'", "'!'", "'~'", "'?'", "':'", "';'", "','", 
			"'='", "'*='", "'/='", "'%='", "'+='", "'-='", "'<<='", "'>>='", "'&='", 
			"'^='", "'|='", "'=='", "'!='", "'.'", "'...'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, "Break", "Case", 
			"Char", "Byte", "Continue", "Default", "Do", "Double", "Else", "Float", 
			"For", "If", "Inline", "Int", "Word", "Dword", "Message", "Timer", "MsTimer", 
			"Long", "Int64", "Restrict", "Return", "Switch", "Void", "While", "Alignas", 
			"Alignof", "Atomic", "Bool", "Complex", "Imaginary", "Noreturn", "ThreadLocal", 
			"LeftParen", "RightParen", "LeftBracket", "RightBracket", "LeftBrace", 
			"RightBrace", "Less", "LessEqual", "Greater", "GreaterEqual", "LeftShift", 
			"RightShift", "Plus", "PlusPlus", "Minus", "MinusMinus", "Star", "Div", 
			"Mod", "And", "Or", "AndAnd", "OrOr", "Caret", "Not", "Tilde", "Question", 
			"Colon", "Semi", "Comma", "Assign", "StarAssign", "DivAssign", "ModAssign", 
			"PlusAssign", "MinusAssign", "LeftShiftAssign", "RightShiftAssign", "AndAssign", 
			"XorAssign", "OrAssign", "Equal", "NotEqual", "Dot", "Ellipsis", "Identifier", 
			"Constant", "DigitSequence", "StringLiteral", "AsmBlock", "Whitespace", 
			"Newline", "BlockComment", "LineComment"
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
			setState(155);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(140);
				match(Identifier);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(141);
				match(Constant);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(143); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(142);
					match(StringLiteral);
					}
					}
					setState(145); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==StringLiteral );
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(147);
				match(LeftParen);
				setState(148);
				expression();
				setState(149);
				match(RightParen);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(151);
				match(LeftParen);
				setState(152);
				compoundStatement();
				setState(153);
				match(RightParen);
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

	public static class GenericAssocListContext extends ParserRuleContext {
		public List<GenericAssociationContext> genericAssociation() {
			return getRuleContexts(GenericAssociationContext.class);
		}
		public GenericAssociationContext genericAssociation(int i) {
			return getRuleContext(GenericAssociationContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(CaplParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(CaplParser.Comma, i);
		}
		public GenericAssocListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_genericAssocList; }
	}

	public final GenericAssocListContext genericAssocList() throws RecognitionException {
		GenericAssocListContext _localctx = new GenericAssocListContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_genericAssocList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(157);
			genericAssociation();
			setState(162);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(158);
				match(Comma);
				setState(159);
				genericAssociation();
				}
				}
				setState(164);
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

	public static class GenericAssociationContext extends ParserRuleContext {
		public TerminalNode Colon() { return getToken(CaplParser.Colon, 0); }
		public AssignmentExpressionContext assignmentExpression() {
			return getRuleContext(AssignmentExpressionContext.class,0);
		}
		public TypeNameContext typeName() {
			return getRuleContext(TypeNameContext.class,0);
		}
		public TerminalNode Default() { return getToken(CaplParser.Default, 0); }
		public GenericAssociationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_genericAssociation; }
	}

	public final GenericAssociationContext genericAssociation() throws RecognitionException {
		GenericAssociationContext _localctx = new GenericAssociationContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_genericAssociation);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
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
				setState(165);
				typeName();
				}
				break;
			case Default:
				{
				setState(166);
				match(Default);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(169);
			match(Colon);
			setState(170);
			assignmentExpression();
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
		enterRule(_localctx, 6, RULE_postfixExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(183);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
			case 1:
				{
				setState(172);
				primaryExpression();
				}
				break;
			case 2:
				{
				setState(173);
				match(LeftParen);
				setState(174);
				typeName();
				setState(175);
				match(RightParen);
				setState(176);
				match(LeftBrace);
				setState(177);
				initializerList();
				setState(179);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(178);
					match(Comma);
					}
				}

				setState(181);
				match(RightBrace);
				}
				break;
			}
			setState(199);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 43)) & ~0x3f) == 0 && ((1L << (_la - 43)) & ((1L << (LeftParen - 43)) | (1L << (LeftBracket - 43)) | (1L << (PlusPlus - 43)) | (1L << (MinusMinus - 43)) | (1L << (Dot - 43)))) != 0)) {
				{
				setState(197);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LeftBracket:
					{
					setState(185);
					match(LeftBracket);
					setState(186);
					expression();
					setState(187);
					match(RightBracket);
					}
					break;
				case LeftParen:
					{
					setState(189);
					match(LeftParen);
					setState(191);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
						{
						setState(190);
						argumentExpressionList();
						}
					}

					setState(193);
					match(RightParen);
					}
					break;
				case Dot:
					{
					setState(194);
					match(Dot);
					setState(195);
					match(Identifier);
					}
					break;
				case PlusPlus:
				case MinusMinus:
					{
					setState(196);
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
				setState(201);
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
		enterRule(_localctx, 8, RULE_argumentExpressionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(202);
			assignmentExpression();
			setState(207);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(203);
				match(Comma);
				setState(204);
				assignmentExpression();
				}
				}
				setState(209);
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
		enterRule(_localctx, 10, RULE_unaryExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(213);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==PlusPlus || _la==MinusMinus) {
				{
				{
				setState(210);
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
				setState(215);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(227);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LeftParen:
			case Identifier:
			case Constant:
			case StringLiteral:
				{
				setState(216);
				postfixExpression();
				}
				break;
			case Plus:
			case Minus:
			case Not:
			case Tilde:
				{
				setState(217);
				unaryOperator();
				setState(218);
				castExpression();
				}
				break;
			case Alignof:
				{
				setState(220);
				match(Alignof);
				setState(221);
				match(LeftParen);
				setState(222);
				typeName();
				setState(223);
				match(RightParen);
				}
				break;
			case AndAnd:
				{
				setState(225);
				match(AndAnd);
				setState(226);
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
		enterRule(_localctx, 12, RULE_unaryOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(229);
			_la = _input.LA(1);
			if ( !(((((_la - 55)) & ~0x3f) == 0 && ((1L << (_la - 55)) & ((1L << (Plus - 55)) | (1L << (Minus - 55)) | (1L << (Not - 55)) | (1L << (Tilde - 55)))) != 0)) ) {
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
		enterRule(_localctx, 14, RULE_castExpression);
		try {
			setState(238);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(231);
				match(LeftParen);
				setState(232);
				typeName();
				setState(233);
				match(RightParen);
				setState(234);
				castExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(236);
				unaryExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(237);
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
		enterRule(_localctx, 16, RULE_multiplicativeExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(240);
			castExpression();
			setState(245);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) {
				{
				{
				setState(241);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Star) | (1L << Div) | (1L << Mod))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(242);
				castExpression();
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
		enterRule(_localctx, 18, RULE_additiveExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(248);
			multiplicativeExpression();
			setState(253);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Plus || _la==Minus) {
				{
				{
				setState(249);
				_la = _input.LA(1);
				if ( !(_la==Plus || _la==Minus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(250);
				multiplicativeExpression();
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
		enterRule(_localctx, 20, RULE_shiftExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(256);
			additiveExpression();
			setState(261);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==LeftShift || _la==RightShift) {
				{
				{
				setState(257);
				_la = _input.LA(1);
				if ( !(_la==LeftShift || _la==RightShift) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(258);
				additiveExpression();
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
		enterRule(_localctx, 22, RULE_relationalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(264);
			shiftExpression();
			setState(269);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) {
				{
				{
				setState(265);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(266);
				shiftExpression();
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
		enterRule(_localctx, 24, RULE_equalityExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(272);
			relationalExpression();
			setState(277);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Equal || _la==NotEqual) {
				{
				{
				setState(273);
				_la = _input.LA(1);
				if ( !(_la==Equal || _la==NotEqual) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(274);
				relationalExpression();
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
		enterRule(_localctx, 26, RULE_andExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(280);
			equalityExpression();
			setState(285);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==And) {
				{
				{
				setState(281);
				match(And);
				setState(282);
				equalityExpression();
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
		enterRule(_localctx, 28, RULE_exclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(288);
			andExpression();
			setState(293);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Caret) {
				{
				{
				setState(289);
				match(Caret);
				setState(290);
				andExpression();
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
		enterRule(_localctx, 30, RULE_inclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(296);
			exclusiveOrExpression();
			setState(301);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Or) {
				{
				{
				setState(297);
				match(Or);
				setState(298);
				exclusiveOrExpression();
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
		enterRule(_localctx, 32, RULE_logicalAndExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(304);
			inclusiveOrExpression();
			setState(309);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AndAnd) {
				{
				{
				setState(305);
				match(AndAnd);
				setState(306);
				inclusiveOrExpression();
				}
				}
				setState(311);
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
		enterRule(_localctx, 34, RULE_logicalOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(312);
			logicalAndExpression();
			setState(317);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OrOr) {
				{
				{
				setState(313);
				match(OrOr);
				setState(314);
				logicalAndExpression();
				}
				}
				setState(319);
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
		enterRule(_localctx, 36, RULE_conditionalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(320);
			logicalOrExpression();
			setState(326);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Question) {
				{
				setState(321);
				match(Question);
				setState(322);
				expression();
				setState(323);
				match(Colon);
				setState(324);
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
		enterRule(_localctx, 38, RULE_assignmentExpression);
		try {
			setState(334);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,24,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(328);
				conditionalExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(329);
				unaryExpression();
				setState(330);
				assignmentOperator();
				setState(331);
				assignmentExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(333);
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
		enterRule(_localctx, 40, RULE_assignmentOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(336);
			_la = _input.LA(1);
			if ( !(((((_la - 73)) & ~0x3f) == 0 && ((1L << (_la - 73)) & ((1L << (Assign - 73)) | (1L << (StarAssign - 73)) | (1L << (DivAssign - 73)) | (1L << (ModAssign - 73)) | (1L << (PlusAssign - 73)) | (1L << (MinusAssign - 73)) | (1L << (LeftShiftAssign - 73)) | (1L << (RightShiftAssign - 73)) | (1L << (AndAssign - 73)) | (1L << (XorAssign - 73)) | (1L << (OrAssign - 73)))) != 0)) ) {
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
		enterRule(_localctx, 42, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(338);
			assignmentExpression();
			setState(343);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(339);
				match(Comma);
				setState(340);
				assignmentExpression();
				}
				}
				setState(345);
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
		enterRule(_localctx, 44, RULE_constantExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(346);
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
		enterRule(_localctx, 46, RULE_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(348);
			declarationSpecifiers();
			setState(350);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(349);
				initDeclaratorList();
				}
			}

			setState(352);
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
		enterRule(_localctx, 48, RULE_declarationSpecifiers);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(355); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(354);
				declarationSpecifier();
				}
				}
				setState(357); 
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
		enterRule(_localctx, 50, RULE_declarationSpecifiers2);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(360); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(359);
				declarationSpecifier();
				}
				}
				setState(362); 
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
		enterRule(_localctx, 52, RULE_declarationSpecifier);
		try {
			setState(367);
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
				setState(364);
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
				setState(365);
				functionSpecifier();
				}
				break;
			case Alignas:
				enterOuterAlt(_localctx, 3);
				{
				setState(366);
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
		enterRule(_localctx, 54, RULE_initDeclaratorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(369);
			initDeclarator();
			setState(374);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(370);
				match(Comma);
				setState(371);
				initDeclarator();
				}
				}
				setState(376);
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
		enterRule(_localctx, 56, RULE_initDeclarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(377);
			declarator();
			setState(380);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Assign) {
				{
				setState(378);
				match(Assign);
				setState(379);
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
		enterRule(_localctx, 58, RULE_typeSpecifier);
		int _la;
		try {
			setState(384);
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
				setState(382);
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
				setState(383);
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
		enterRule(_localctx, 60, RULE_specifierQualifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(386);
			typeSpecifier();
			setState(388);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Atomic) | (1L << Bool) | (1L << Complex))) != 0)) {
				{
				setState(387);
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
		enterRule(_localctx, 62, RULE_atomicTypeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(390);
			match(Atomic);
			setState(391);
			match(LeftParen);
			setState(392);
			typeName();
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
		enterRule(_localctx, 64, RULE_functionSpecifier);
		int _la;
		try {
			setState(401);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__4:
			case Inline:
			case Noreturn:
				enterOuterAlt(_localctx, 1);
				{
				setState(395);
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
				setState(396);
				gccAttributeSpecifier();
				}
				break;
			case T__5:
				enterOuterAlt(_localctx, 3);
				{
				setState(397);
				match(T__5);
				setState(398);
				match(LeftParen);
				setState(399);
				match(Identifier);
				setState(400);
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
		enterRule(_localctx, 66, RULE_alignmentSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(403);
			match(Alignas);
			setState(404);
			match(LeftParen);
			setState(407);
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
				setState(405);
				typeName();
				}
				break;
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
				setState(406);
				constantExpression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(409);
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
		enterRule(_localctx, 68, RULE_declarator);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(411);
			directDeclarator(0);
			setState(415);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,36,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(412);
					gccDeclaratorExtension();
					}
					} 
				}
				setState(417);
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
		int _startState = 70;
		enterRecursionRule(_localctx, 70, RULE_directDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(427);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,37,_ctx) ) {
			case 1:
				{
				setState(419);
				match(Identifier);
				}
				break;
			case 2:
				{
				setState(420);
				match(LeftParen);
				setState(421);
				declarator();
				setState(422);
				match(RightParen);
				}
				break;
			case 3:
				{
				setState(424);
				match(Identifier);
				setState(425);
				match(Colon);
				setState(426);
				match(DigitSequence);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(448);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,41,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(446);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,40,_ctx) ) {
					case 1:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(429);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(430);
						match(LeftBracket);
						setState(432);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
							{
							setState(431);
							assignmentExpression();
							}
						}

						setState(434);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(435);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(436);
						match(LeftParen);
						setState(437);
						parameterTypeList();
						setState(438);
						match(RightParen);
						}
						break;
					case 3:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(440);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(441);
						match(LeftParen);
						setState(443);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==Identifier) {
							{
							setState(442);
							identifierList();
							}
						}

						setState(445);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(450);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,41,_ctx);
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
		enterRule(_localctx, 72, RULE_gccDeclaratorExtension);
		int _la;
		try {
			setState(460);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__6:
				enterOuterAlt(_localctx, 1);
				{
				setState(451);
				match(T__6);
				setState(452);
				match(LeftParen);
				setState(454); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(453);
					match(StringLiteral);
					}
					}
					setState(456); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==StringLiteral );
				setState(458);
				match(RightParen);
				}
				break;
			case T__7:
				enterOuterAlt(_localctx, 2);
				{
				setState(459);
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
		enterRule(_localctx, 74, RULE_gccAttributeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(462);
			match(T__7);
			setState(463);
			match(LeftParen);
			setState(464);
			match(LeftParen);
			setState(465);
			gccAttributeList();
			setState(466);
			match(RightParen);
			setState(467);
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
		enterRule(_localctx, 76, RULE_gccAttributeList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(470);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(469);
				gccAttribute();
				}
			}

			setState(478);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(472);
				match(Comma);
				setState(474);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
					{
					setState(473);
					gccAttribute();
					}
				}

				}
				}
				setState(480);
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
		enterRule(_localctx, 78, RULE_gccAttribute);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(481);
			_la = _input.LA(1);
			if ( _la <= 0 || (((((_la - 43)) & ~0x3f) == 0 && ((1L << (_la - 43)) & ((1L << (LeftParen - 43)) | (1L << (RightParen - 43)) | (1L << (Comma - 43)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(487);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen) {
				{
				setState(482);
				match(LeftParen);
				setState(484);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
					{
					setState(483);
					argumentExpressionList();
					}
				}

				setState(486);
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
		enterRule(_localctx, 80, RULE_nestedParenthesesBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(496);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Restrict) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Imaginary) | (1L << Noreturn) | (1L << ThreadLocal) | (1L << LeftParen) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Star) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (AndAnd - 64)) | (1L << (OrOr - 64)) | (1L << (Caret - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Question - 64)) | (1L << (Colon - 64)) | (1L << (Semi - 64)) | (1L << (Comma - 64)) | (1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Dot - 64)) | (1L << (Ellipsis - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (AsmBlock - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(494);
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
					setState(489);
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
					setState(490);
					match(LeftParen);
					setState(491);
					nestedParenthesesBlock();
					setState(492);
					match(RightParen);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(498);
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
		enterRule(_localctx, 82, RULE_parameterTypeList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(499);
			parameterList();
			setState(502);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Comma) {
				{
				setState(500);
				match(Comma);
				setState(501);
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
		enterRule(_localctx, 84, RULE_parameterList);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(504);
			parameterDeclaration();
			setState(509);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,52,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(505);
					match(Comma);
					setState(506);
					parameterDeclaration();
					}
					} 
				}
				setState(511);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,52,_ctx);
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
		enterRule(_localctx, 86, RULE_parameterDeclaration);
		int _la;
		try {
			setState(519);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,54,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(512);
				declarationSpecifiers();
				setState(513);
				declarator();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(515);
				declarationSpecifiers2();
				setState(517);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LeftParen || _la==LeftBracket) {
					{
					setState(516);
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
		enterRule(_localctx, 88, RULE_identifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(521);
			match(Identifier);
			setState(526);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(522);
				match(Comma);
				setState(523);
				match(Identifier);
				}
				}
				setState(528);
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
		enterRule(_localctx, 90, RULE_typeName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(529);
			specifierQualifierList();
			setState(531);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==LeftBracket) {
				{
				setState(530);
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
		enterRule(_localctx, 92, RULE_abstractDeclarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(533);
			directAbstractDeclarator(0);
			setState(537);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__6 || _la==T__7) {
				{
				{
				setState(534);
				gccDeclaratorExtension();
				}
				}
				setState(539);
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
		int _startState = 94;
		enterRecursionRule(_localctx, 94, RULE_directAbstractDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(569);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,62,_ctx) ) {
			case 1:
				{
				setState(541);
				match(LeftParen);
				setState(542);
				abstractDeclarator();
				setState(543);
				match(RightParen);
				setState(547);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,58,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(544);
						gccDeclaratorExtension();
						}
						} 
					}
					setState(549);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,58,_ctx);
				}
				}
				break;
			case 2:
				{
				setState(550);
				match(LeftBracket);
				setState(552);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
					{
					setState(551);
					assignmentExpression();
					}
				}

				setState(554);
				match(RightBracket);
				}
				break;
			case 3:
				{
				setState(555);
				match(LeftBracket);
				setState(556);
				match(Star);
				setState(557);
				match(RightBracket);
				}
				break;
			case 4:
				{
				setState(558);
				match(LeftParen);
				setState(560);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
					{
					setState(559);
					parameterTypeList();
					}
				}

				setState(562);
				match(RightParen);
				setState(566);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,61,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(563);
						gccDeclaratorExtension();
						}
						} 
					}
					setState(568);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,61,_ctx);
				}
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(595);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,67,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(593);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,66,_ctx) ) {
					case 1:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(571);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(572);
						match(LeftBracket);
						setState(574);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
							{
							setState(573);
							assignmentExpression();
							}
						}

						setState(576);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(577);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(578);
						match(LeftBracket);
						setState(579);
						match(Star);
						setState(580);
						match(RightBracket);
						}
						break;
					case 3:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(581);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(582);
						match(LeftParen);
						setState(584);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
							{
							setState(583);
							parameterTypeList();
							}
						}

						setState(586);
						match(RightParen);
						setState(590);
						_errHandler.sync(this);
						_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
						while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
							if ( _alt==1 ) {
								{
								{
								setState(587);
								gccDeclaratorExtension();
								}
								} 
							}
							setState(592);
							_errHandler.sync(this);
							_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
						}
						}
						break;
					}
					} 
				}
				setState(597);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,67,_ctx);
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
		enterRule(_localctx, 96, RULE_initializer);
		int _la;
		try {
			setState(606);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
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
				setState(598);
				assignmentExpression();
				}
				break;
			case LeftBrace:
				enterOuterAlt(_localctx, 2);
				{
				setState(599);
				match(LeftBrace);
				setState(600);
				initializerList();
				setState(602);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(601);
					match(Comma);
					}
				}

				setState(604);
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
		enterRule(_localctx, 98, RULE_initializerList);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(609);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftBracket || _la==Dot) {
				{
				setState(608);
				designation();
				}
			}

			setState(611);
			initializer();
			setState(619);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,72,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(612);
					match(Comma);
					setState(614);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==LeftBracket || _la==Dot) {
						{
						setState(613);
						designation();
						}
					}

					setState(616);
					initializer();
					}
					} 
				}
				setState(621);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,72,_ctx);
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
		enterRule(_localctx, 100, RULE_designation);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(622);
			designatorList();
			setState(623);
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
		enterRule(_localctx, 102, RULE_designatorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(626); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(625);
				designator();
				}
				}
				setState(628); 
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
		enterRule(_localctx, 104, RULE_designator);
		try {
			setState(636);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LeftBracket:
				enterOuterAlt(_localctx, 1);
				{
				setState(630);
				match(LeftBracket);
				setState(631);
				constantExpression();
				setState(632);
				match(RightBracket);
				}
				break;
			case Dot:
				enterOuterAlt(_localctx, 2);
				{
				setState(634);
				match(Dot);
				setState(635);
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
		enterRule(_localctx, 106, RULE_statement);
		try {
			setState(644);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,75,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(638);
				labeledStatement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(639);
				compoundStatement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(640);
				expressionStatement();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(641);
				selectionStatement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(642);
				iterationStatement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(643);
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
		enterRule(_localctx, 108, RULE_labeledStatement);
		try {
			setState(657);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				enterOuterAlt(_localctx, 1);
				{
				setState(646);
				match(Identifier);
				setState(647);
				match(Colon);
				setState(648);
				statement();
				}
				break;
			case Case:
				enterOuterAlt(_localctx, 2);
				{
				setState(649);
				match(Case);
				setState(650);
				constantExpression();
				setState(651);
				match(Colon);
				setState(652);
				statement();
				}
				break;
			case Default:
				enterOuterAlt(_localctx, 3);
				{
				setState(654);
				match(Default);
				setState(655);
				match(Colon);
				setState(656);
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
		enterRule(_localctx, 110, RULE_compoundStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(659);
			match(LeftBrace);
			setState(661);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (AndAnd - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Semi - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(660);
				blockItemList();
				}
			}

			setState(663);
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
		enterRule(_localctx, 112, RULE_blockItemList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(666); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(665);
				blockItem();
				}
				}
				setState(668); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << Alignas) | (1L << Alignof) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen) | (1L << LeftBrace) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (AndAnd - 64)) | (1L << (Not - 64)) | (1L << (Tilde - 64)) | (1L << (Semi - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0) );
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
		enterRule(_localctx, 114, RULE_blockItem);
		try {
			setState(672);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
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
				setState(670);
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
				setState(671);
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
		enterRule(_localctx, 116, RULE_expressionStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(675);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
				{
				setState(674);
				expression();
				}
			}

			setState(677);
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
		enterRule(_localctx, 118, RULE_selectionStatement);
		try {
			setState(694);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case If:
				enterOuterAlt(_localctx, 1);
				{
				setState(679);
				match(If);
				setState(680);
				match(LeftParen);
				setState(681);
				expression();
				setState(682);
				match(RightParen);
				setState(683);
				statement();
				setState(686);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,81,_ctx) ) {
				case 1:
					{
					setState(684);
					match(Else);
					setState(685);
					statement();
					}
					break;
				}
				}
				break;
			case Switch:
				enterOuterAlt(_localctx, 2);
				{
				setState(688);
				match(Switch);
				setState(689);
				match(LeftParen);
				setState(690);
				expression();
				setState(691);
				match(RightParen);
				setState(692);
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
		enterRule(_localctx, 120, RULE_iterationStatement);
		try {
			setState(716);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case While:
				enterOuterAlt(_localctx, 1);
				{
				setState(696);
				match(While);
				setState(697);
				match(LeftParen);
				setState(698);
				expression();
				setState(699);
				match(RightParen);
				setState(700);
				statement();
				}
				break;
			case Do:
				enterOuterAlt(_localctx, 2);
				{
				setState(702);
				match(Do);
				setState(703);
				statement();
				setState(704);
				match(While);
				setState(705);
				match(LeftParen);
				setState(706);
				expression();
				setState(707);
				match(RightParen);
				setState(708);
				match(Semi);
				}
				break;
			case For:
				enterOuterAlt(_localctx, 3);
				{
				setState(710);
				match(For);
				setState(711);
				match(LeftParen);
				setState(712);
				forCondition();
				setState(713);
				match(RightParen);
				setState(714);
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
		enterRule(_localctx, 122, RULE_forCondition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(722);
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
				setState(718);
				forDeclaration();
				}
				break;
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
				setState(720);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
					{
					setState(719);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(724);
			match(Semi);
			setState(726);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
				{
				setState(725);
				forExpression();
				}
			}

			setState(728);
			match(Semi);
			setState(730);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
				{
				setState(729);
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
		enterRule(_localctx, 124, RULE_forDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(732);
			declarationSpecifiers();
			setState(734);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(733);
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
		enterRule(_localctx, 126, RULE_forExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(736);
			assignmentExpression();
			setState(741);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(737);
				match(Comma);
				setState(738);
				assignmentExpression();
				}
				}
				setState(743);
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
		enterRule(_localctx, 128, RULE_jumpStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(749);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Break:
			case Continue:
				{
				setState(744);
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
				setState(745);
				match(Return);
				setState(747);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 36)) & ~0x3f) == 0 && ((1L << (_la - 36)) & ((1L << (Alignof - 36)) | (1L << (LeftParen - 36)) | (1L << (Plus - 36)) | (1L << (PlusPlus - 36)) | (1L << (Minus - 36)) | (1L << (MinusMinus - 36)) | (1L << (AndAnd - 36)) | (1L << (Not - 36)) | (1L << (Tilde - 36)) | (1L << (Identifier - 36)) | (1L << (Constant - 36)) | (1L << (DigitSequence - 36)) | (1L << (StringLiteral - 36)))) != 0)) {
					{
					setState(746);
					expression();
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(751);
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
		enterRule(_localctx, 130, RULE_compilationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(754);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn) | (1L << LeftParen))) != 0) || _la==Semi || _la==Identifier) {
				{
				setState(753);
				translationUnit();
				}
			}

			setState(756);
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
		enterRule(_localctx, 132, RULE_translationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(759); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(758);
				externalDeclaration();
				}
				}
				setState(761); 
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
		enterRule(_localctx, 134, RULE_externalDeclaration);
		try {
			setState(766);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,94,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(763);
				functionDefinition();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(764);
				declaration();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(765);
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
		enterRule(_localctx, 136, RULE_functionDefinition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(769);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
				{
				setState(768);
				declarationSpecifiers();
				}
			}

			setState(771);
			declarator();
			setState(773);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__7) | (1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Inline) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << Alignas) | (1L << Atomic) | (1L << Bool) | (1L << Complex) | (1L << Noreturn))) != 0)) {
				{
				setState(772);
				declarationList();
				}
			}

			setState(775);
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
		enterRule(_localctx, 138, RULE_declarationList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(778); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(777);
				declaration();
				}
				}
				setState(780); 
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

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 35:
			return directDeclarator_sempred((DirectDeclaratorContext)_localctx, predIndex);
		case 47:
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3b\u0311\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\3\2\3\2\3"+
		"\2\6\2\u0092\n\2\r\2\16\2\u0093\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\5\2\u009e"+
		"\n\2\3\3\3\3\3\3\7\3\u00a3\n\3\f\3\16\3\u00a6\13\3\3\4\3\4\5\4\u00aa\n"+
		"\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00b6\n\5\3\5\3\5\5\5\u00ba"+
		"\n\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00c2\n\5\3\5\3\5\3\5\3\5\7\5\u00c8\n"+
		"\5\f\5\16\5\u00cb\13\5\3\6\3\6\3\6\7\6\u00d0\n\6\f\6\16\6\u00d3\13\6\3"+
		"\7\7\7\u00d6\n\7\f\7\16\7\u00d9\13\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3"+
		"\7\3\7\3\7\5\7\u00e6\n\7\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t\3\t\5\t\u00f1"+
		"\n\t\3\n\3\n\3\n\7\n\u00f6\n\n\f\n\16\n\u00f9\13\n\3\13\3\13\3\13\7\13"+
		"\u00fe\n\13\f\13\16\13\u0101\13\13\3\f\3\f\3\f\7\f\u0106\n\f\f\f\16\f"+
		"\u0109\13\f\3\r\3\r\3\r\7\r\u010e\n\r\f\r\16\r\u0111\13\r\3\16\3\16\3"+
		"\16\7\16\u0116\n\16\f\16\16\16\u0119\13\16\3\17\3\17\3\17\7\17\u011e\n"+
		"\17\f\17\16\17\u0121\13\17\3\20\3\20\3\20\7\20\u0126\n\20\f\20\16\20\u0129"+
		"\13\20\3\21\3\21\3\21\7\21\u012e\n\21\f\21\16\21\u0131\13\21\3\22\3\22"+
		"\3\22\7\22\u0136\n\22\f\22\16\22\u0139\13\22\3\23\3\23\3\23\7\23\u013e"+
		"\n\23\f\23\16\23\u0141\13\23\3\24\3\24\3\24\3\24\3\24\3\24\5\24\u0149"+
		"\n\24\3\25\3\25\3\25\3\25\3\25\3\25\5\25\u0151\n\25\3\26\3\26\3\27\3\27"+
		"\3\27\7\27\u0158\n\27\f\27\16\27\u015b\13\27\3\30\3\30\3\31\3\31\5\31"+
		"\u0161\n\31\3\31\3\31\3\32\6\32\u0166\n\32\r\32\16\32\u0167\3\33\6\33"+
		"\u016b\n\33\r\33\16\33\u016c\3\34\3\34\3\34\5\34\u0172\n\34\3\35\3\35"+
		"\3\35\7\35\u0177\n\35\f\35\16\35\u017a\13\35\3\36\3\36\3\36\5\36\u017f"+
		"\n\36\3\37\3\37\5\37\u0183\n\37\3 \3 \5 \u0187\n \3!\3!\3!\3!\3!\3\"\3"+
		"\"\3\"\3\"\3\"\3\"\5\"\u0194\n\"\3#\3#\3#\3#\5#\u019a\n#\3#\3#\3$\3$\7"+
		"$\u01a0\n$\f$\16$\u01a3\13$\3%\3%\3%\3%\3%\3%\3%\3%\3%\5%\u01ae\n%\3%"+
		"\3%\3%\5%\u01b3\n%\3%\3%\3%\3%\3%\3%\3%\3%\3%\5%\u01be\n%\3%\7%\u01c1"+
		"\n%\f%\16%\u01c4\13%\3&\3&\3&\6&\u01c9\n&\r&\16&\u01ca\3&\3&\5&\u01cf"+
		"\n&\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3(\5(\u01d9\n(\3(\3(\5(\u01dd\n(\7(\u01df"+
		"\n(\f(\16(\u01e2\13(\3)\3)\3)\5)\u01e7\n)\3)\5)\u01ea\n)\3*\3*\3*\3*\3"+
		"*\7*\u01f1\n*\f*\16*\u01f4\13*\3+\3+\3+\5+\u01f9\n+\3,\3,\3,\7,\u01fe"+
		"\n,\f,\16,\u0201\13,\3-\3-\3-\3-\3-\5-\u0208\n-\5-\u020a\n-\3.\3.\3.\7"+
		".\u020f\n.\f.\16.\u0212\13.\3/\3/\5/\u0216\n/\3\60\3\60\7\60\u021a\n\60"+
		"\f\60\16\60\u021d\13\60\3\61\3\61\3\61\3\61\3\61\7\61\u0224\n\61\f\61"+
		"\16\61\u0227\13\61\3\61\3\61\5\61\u022b\n\61\3\61\3\61\3\61\3\61\3\61"+
		"\3\61\5\61\u0233\n\61\3\61\3\61\7\61\u0237\n\61\f\61\16\61\u023a\13\61"+
		"\5\61\u023c\n\61\3\61\3\61\3\61\5\61\u0241\n\61\3\61\3\61\3\61\3\61\3"+
		"\61\3\61\3\61\3\61\5\61\u024b\n\61\3\61\3\61\7\61\u024f\n\61\f\61\16\61"+
		"\u0252\13\61\7\61\u0254\n\61\f\61\16\61\u0257\13\61\3\62\3\62\3\62\3\62"+
		"\5\62\u025d\n\62\3\62\3\62\5\62\u0261\n\62\3\63\5\63\u0264\n\63\3\63\3"+
		"\63\3\63\5\63\u0269\n\63\3\63\7\63\u026c\n\63\f\63\16\63\u026f\13\63\3"+
		"\64\3\64\3\64\3\65\6\65\u0275\n\65\r\65\16\65\u0276\3\66\3\66\3\66\3\66"+
		"\3\66\3\66\5\66\u027f\n\66\3\67\3\67\3\67\3\67\3\67\3\67\5\67\u0287\n"+
		"\67\38\38\38\38\38\38\38\38\38\38\38\58\u0294\n8\39\39\59\u0298\n9\39"+
		"\39\3:\6:\u029d\n:\r:\16:\u029e\3;\3;\5;\u02a3\n;\3<\5<\u02a6\n<\3<\3"+
		"<\3=\3=\3=\3=\3=\3=\3=\5=\u02b1\n=\3=\3=\3=\3=\3=\3=\5=\u02b9\n=\3>\3"+
		">\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\5>\u02cf\n>\3"+
		"?\3?\5?\u02d3\n?\5?\u02d5\n?\3?\3?\5?\u02d9\n?\3?\3?\5?\u02dd\n?\3@\3"+
		"@\5@\u02e1\n@\3A\3A\3A\7A\u02e6\nA\fA\16A\u02e9\13A\3B\3B\3B\5B\u02ee"+
		"\nB\5B\u02f0\nB\3B\3B\3C\5C\u02f5\nC\3C\3C\3D\6D\u02fa\nD\rD\16D\u02fb"+
		"\3E\3E\3E\5E\u0301\nE\3F\5F\u0304\nF\3F\3F\5F\u0308\nF\3F\3F\3G\6G\u030d"+
		"\nG\rG\16G\u030e\3G\2\4H`H\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$"+
		"&(*,.\60\62\64\668:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|~\u0080\u0082\u0084"+
		"\u0086\u0088\u008a\u008c\2\17\4\2::<<\5\299;;EF\3\2=?\4\299;;\3\2\678"+
		"\3\2\63\66\3\2VW\3\2KU\t\2\3\5\r\16\22\22\24\24\30\37##()\5\2\6\7\27\27"+
		"++\4\2-.JJ\3\2-.\4\2\13\13\17\17\2\u0343\2\u009d\3\2\2\2\4\u009f\3\2\2"+
		"\2\6\u00a9\3\2\2\2\b\u00b9\3\2\2\2\n\u00cc\3\2\2\2\f\u00d7\3\2\2\2\16"+
		"\u00e7\3\2\2\2\20\u00f0\3\2\2\2\22\u00f2\3\2\2\2\24\u00fa\3\2\2\2\26\u0102"+
		"\3\2\2\2\30\u010a\3\2\2\2\32\u0112\3\2\2\2\34\u011a\3\2\2\2\36\u0122\3"+
		"\2\2\2 \u012a\3\2\2\2\"\u0132\3\2\2\2$\u013a\3\2\2\2&\u0142\3\2\2\2(\u0150"+
		"\3\2\2\2*\u0152\3\2\2\2,\u0154\3\2\2\2.\u015c\3\2\2\2\60\u015e\3\2\2\2"+
		"\62\u0165\3\2\2\2\64\u016a\3\2\2\2\66\u0171\3\2\2\28\u0173\3\2\2\2:\u017b"+
		"\3\2\2\2<\u0182\3\2\2\2>\u0184\3\2\2\2@\u0188\3\2\2\2B\u0193\3\2\2\2D"+
		"\u0195\3\2\2\2F\u019d\3\2\2\2H\u01ad\3\2\2\2J\u01ce\3\2\2\2L\u01d0\3\2"+
		"\2\2N\u01d8\3\2\2\2P\u01e3\3\2\2\2R\u01f2\3\2\2\2T\u01f5\3\2\2\2V\u01fa"+
		"\3\2\2\2X\u0209\3\2\2\2Z\u020b\3\2\2\2\\\u0213\3\2\2\2^\u0217\3\2\2\2"+
		"`\u023b\3\2\2\2b\u0260\3\2\2\2d\u0263\3\2\2\2f\u0270\3\2\2\2h\u0274\3"+
		"\2\2\2j\u027e\3\2\2\2l\u0286\3\2\2\2n\u0293\3\2\2\2p\u0295\3\2\2\2r\u029c"+
		"\3\2\2\2t\u02a2\3\2\2\2v\u02a5\3\2\2\2x\u02b8\3\2\2\2z\u02ce\3\2\2\2|"+
		"\u02d4\3\2\2\2~\u02de\3\2\2\2\u0080\u02e2\3\2\2\2\u0082\u02ef\3\2\2\2"+
		"\u0084\u02f4\3\2\2\2\u0086\u02f9\3\2\2\2\u0088\u0300\3\2\2\2\u008a\u0303"+
		"\3\2\2\2\u008c\u030c\3\2\2\2\u008e\u009e\7Z\2\2\u008f\u009e\7[\2\2\u0090"+
		"\u0092\7]\2\2\u0091\u0090\3\2\2\2\u0092\u0093\3\2\2\2\u0093\u0091\3\2"+
		"\2\2\u0093\u0094\3\2\2\2\u0094\u009e\3\2\2\2\u0095\u0096\7-\2\2\u0096"+
		"\u0097\5,\27\2\u0097\u0098\7.\2\2\u0098\u009e\3\2\2\2\u0099\u009a\7-\2"+
		"\2\u009a\u009b\5p9\2\u009b\u009c\7.\2\2\u009c\u009e\3\2\2\2\u009d\u008e"+
		"\3\2\2\2\u009d\u008f\3\2\2\2\u009d\u0091\3\2\2\2\u009d\u0095\3\2\2\2\u009d"+
		"\u0099\3\2\2\2\u009e\3\3\2\2\2\u009f\u00a4\5\6\4\2\u00a0\u00a1\7J\2\2"+
		"\u00a1\u00a3\5\6\4\2\u00a2\u00a0\3\2\2\2\u00a3\u00a6\3\2\2\2\u00a4\u00a2"+
		"\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5\5\3\2\2\2\u00a6\u00a4\3\2\2\2\u00a7"+
		"\u00aa\5\\/\2\u00a8\u00aa\7\20\2\2\u00a9\u00a7\3\2\2\2\u00a9\u00a8\3\2"+
		"\2\2\u00aa\u00ab\3\2\2\2\u00ab\u00ac\7H\2\2\u00ac\u00ad\5(\25\2\u00ad"+
		"\7\3\2\2\2\u00ae\u00ba\5\2\2\2\u00af\u00b0\7-\2\2\u00b0\u00b1\5\\/\2\u00b1"+
		"\u00b2\7.\2\2\u00b2\u00b3\7\61\2\2\u00b3\u00b5\5d\63\2\u00b4\u00b6\7J"+
		"\2\2\u00b5\u00b4\3\2\2\2\u00b5\u00b6\3\2\2\2\u00b6\u00b7\3\2\2\2\u00b7"+
		"\u00b8\7\62\2\2\u00b8\u00ba\3\2\2\2\u00b9\u00ae\3\2\2\2\u00b9\u00af\3"+
		"\2\2\2\u00ba\u00c9\3\2\2\2\u00bb\u00bc\7/\2\2\u00bc\u00bd\5,\27\2\u00bd"+
		"\u00be\7\60\2\2\u00be\u00c8\3\2\2\2\u00bf\u00c1\7-\2\2\u00c0\u00c2\5\n"+
		"\6\2\u00c1\u00c0\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2\u00c3\3\2\2\2\u00c3"+
		"\u00c8\7.\2\2\u00c4\u00c5\7X\2\2\u00c5\u00c8\7Z\2\2\u00c6\u00c8\t\2\2"+
		"\2\u00c7\u00bb\3\2\2\2\u00c7\u00bf\3\2\2\2\u00c7\u00c4\3\2\2\2\u00c7\u00c6"+
		"\3\2\2\2\u00c8\u00cb\3\2\2\2\u00c9\u00c7\3\2\2\2\u00c9\u00ca\3\2\2\2\u00ca"+
		"\t\3\2\2\2\u00cb\u00c9\3\2\2\2\u00cc\u00d1\5(\25\2\u00cd\u00ce\7J\2\2"+
		"\u00ce\u00d0\5(\25\2\u00cf\u00cd\3\2\2\2\u00d0\u00d3\3\2\2\2\u00d1\u00cf"+
		"\3\2\2\2\u00d1\u00d2\3\2\2\2\u00d2\13\3\2\2\2\u00d3\u00d1\3\2\2\2\u00d4"+
		"\u00d6\t\2\2\2\u00d5\u00d4\3\2\2\2\u00d6\u00d9\3\2\2\2\u00d7\u00d5\3\2"+
		"\2\2\u00d7\u00d8\3\2\2\2\u00d8\u00e5\3\2\2\2\u00d9\u00d7\3\2\2\2\u00da"+
		"\u00e6\5\b\5\2\u00db\u00dc\5\16\b\2\u00dc\u00dd\5\20\t\2\u00dd\u00e6\3"+
		"\2\2\2\u00de\u00df\7&\2\2\u00df\u00e0\7-\2\2\u00e0\u00e1\5\\/\2\u00e1"+
		"\u00e2\7.\2\2\u00e2\u00e6\3\2\2\2\u00e3\u00e4\7B\2\2\u00e4\u00e6\7Z\2"+
		"\2\u00e5\u00da\3\2\2\2\u00e5\u00db\3\2\2\2\u00e5\u00de\3\2\2\2\u00e5\u00e3"+
		"\3\2\2\2\u00e6\r\3\2\2\2\u00e7\u00e8\t\3\2\2\u00e8\17\3\2\2\2\u00e9\u00ea"+
		"\7-\2\2\u00ea\u00eb\5\\/\2\u00eb\u00ec\7.\2\2\u00ec\u00ed\5\20\t\2\u00ed"+
		"\u00f1\3\2\2\2\u00ee\u00f1\5\f\7\2\u00ef\u00f1\7\\\2\2\u00f0\u00e9\3\2"+
		"\2\2\u00f0\u00ee\3\2\2\2\u00f0\u00ef\3\2\2\2\u00f1\21\3\2\2\2\u00f2\u00f7"+
		"\5\20\t\2\u00f3\u00f4\t\4\2\2\u00f4\u00f6\5\20\t\2\u00f5\u00f3\3\2\2\2"+
		"\u00f6\u00f9\3\2\2\2\u00f7\u00f5\3\2\2\2\u00f7\u00f8\3\2\2\2\u00f8\23"+
		"\3\2\2\2\u00f9\u00f7\3\2\2\2\u00fa\u00ff\5\22\n\2\u00fb\u00fc\t\5\2\2"+
		"\u00fc\u00fe\5\22\n\2\u00fd\u00fb\3\2\2\2\u00fe\u0101\3\2\2\2\u00ff\u00fd"+
		"\3\2\2\2\u00ff\u0100\3\2\2\2\u0100\25\3\2\2\2\u0101\u00ff\3\2\2\2\u0102"+
		"\u0107\5\24\13\2\u0103\u0104\t\6\2\2\u0104\u0106\5\24\13\2\u0105\u0103"+
		"\3\2\2\2\u0106\u0109\3\2\2\2\u0107\u0105\3\2\2\2\u0107\u0108\3\2\2\2\u0108"+
		"\27\3\2\2\2\u0109\u0107\3\2\2\2\u010a\u010f\5\26\f\2\u010b\u010c\t\7\2"+
		"\2\u010c\u010e\5\26\f\2\u010d\u010b\3\2\2\2\u010e\u0111\3\2\2\2\u010f"+
		"\u010d\3\2\2\2\u010f\u0110\3\2\2\2\u0110\31\3\2\2\2\u0111\u010f\3\2\2"+
		"\2\u0112\u0117\5\30\r\2\u0113\u0114\t\b\2\2\u0114\u0116\5\30\r\2\u0115"+
		"\u0113\3\2\2\2\u0116\u0119\3\2\2\2\u0117\u0115\3\2\2\2\u0117\u0118\3\2"+
		"\2\2\u0118\33\3\2\2\2\u0119\u0117\3\2\2\2\u011a\u011f\5\32\16\2\u011b"+
		"\u011c\7@\2\2\u011c\u011e\5\32\16\2\u011d\u011b\3\2\2\2\u011e\u0121\3"+
		"\2\2\2\u011f\u011d\3\2\2\2\u011f\u0120\3\2\2\2\u0120\35\3\2\2\2\u0121"+
		"\u011f\3\2\2\2\u0122\u0127\5\34\17\2\u0123\u0124\7D\2\2\u0124\u0126\5"+
		"\34\17\2\u0125\u0123\3\2\2\2\u0126\u0129\3\2\2\2\u0127\u0125\3\2\2\2\u0127"+
		"\u0128\3\2\2\2\u0128\37\3\2\2\2\u0129\u0127\3\2\2\2\u012a\u012f\5\36\20"+
		"\2\u012b\u012c\7A\2\2\u012c\u012e\5\36\20\2\u012d\u012b\3\2\2\2\u012e"+
		"\u0131\3\2\2\2\u012f\u012d\3\2\2\2\u012f\u0130\3\2\2\2\u0130!\3\2\2\2"+
		"\u0131\u012f\3\2\2\2\u0132\u0137\5 \21\2\u0133\u0134\7B\2\2\u0134\u0136"+
		"\5 \21\2\u0135\u0133\3\2\2\2\u0136\u0139\3\2\2\2\u0137\u0135\3\2\2\2\u0137"+
		"\u0138\3\2\2\2\u0138#\3\2\2\2\u0139\u0137\3\2\2\2\u013a\u013f\5\"\22\2"+
		"\u013b\u013c\7C\2\2\u013c\u013e\5\"\22\2\u013d\u013b\3\2\2\2\u013e\u0141"+
		"\3\2\2\2\u013f\u013d\3\2\2\2\u013f\u0140\3\2\2\2\u0140%\3\2\2\2\u0141"+
		"\u013f\3\2\2\2\u0142\u0148\5$\23\2\u0143\u0144\7G\2\2\u0144\u0145\5,\27"+
		"\2\u0145\u0146\7H\2\2\u0146\u0147\5&\24\2\u0147\u0149\3\2\2\2\u0148\u0143"+
		"\3\2\2\2\u0148\u0149\3\2\2\2\u0149\'\3\2\2\2\u014a\u0151\5&\24\2\u014b"+
		"\u014c\5\f\7\2\u014c\u014d\5*\26\2\u014d\u014e\5(\25\2\u014e\u0151\3\2"+
		"\2\2\u014f\u0151\7\\\2\2\u0150\u014a\3\2\2\2\u0150\u014b\3\2\2\2\u0150"+
		"\u014f\3\2\2\2\u0151)\3\2\2\2\u0152\u0153\t\t\2\2\u0153+\3\2\2\2\u0154"+
		"\u0159\5(\25\2\u0155\u0156\7J\2\2\u0156\u0158\5(\25\2\u0157\u0155\3\2"+
		"\2\2\u0158\u015b\3\2\2\2\u0159\u0157\3\2\2\2\u0159\u015a\3\2\2\2\u015a"+
		"-\3\2\2\2\u015b\u0159\3\2\2\2\u015c\u015d\5&\24\2\u015d/\3\2\2\2\u015e"+
		"\u0160\5\62\32\2\u015f\u0161\58\35\2\u0160\u015f\3\2\2\2\u0160\u0161\3"+
		"\2\2\2\u0161\u0162\3\2\2\2\u0162\u0163\7I\2\2\u0163\61\3\2\2\2\u0164\u0166"+
		"\5\66\34\2\u0165\u0164\3\2\2\2\u0166\u0167\3\2\2\2\u0167\u0165\3\2\2\2"+
		"\u0167\u0168\3\2\2\2\u0168\63\3\2\2\2\u0169\u016b\5\66\34\2\u016a\u0169"+
		"\3\2\2\2\u016b\u016c\3\2\2\2\u016c\u016a\3\2\2\2\u016c\u016d\3\2\2\2\u016d"+
		"\65\3\2\2\2\u016e\u0172\5<\37\2\u016f\u0172\5B\"\2\u0170\u0172\5D#\2\u0171"+
		"\u016e\3\2\2\2\u0171\u016f\3\2\2\2\u0171\u0170\3\2\2\2\u0172\67\3\2\2"+
		"\2\u0173\u0178\5:\36\2\u0174\u0175\7J\2\2\u0175\u0177\5:\36\2\u0176\u0174"+
		"\3\2\2\2\u0177\u017a\3\2\2\2\u0178\u0176\3\2\2\2\u0178\u0179\3\2\2\2\u0179"+
		"9\3\2\2\2\u017a\u0178\3\2\2\2\u017b\u017e\5F$\2\u017c\u017d\7K\2\2\u017d"+
		"\u017f\5b\62\2\u017e\u017c\3\2\2\2\u017e\u017f\3\2\2\2\u017f;\3\2\2\2"+
		"\u0180\u0183\t\n\2\2\u0181\u0183\5@!\2\u0182\u0180\3\2\2\2\u0182\u0181"+
		"\3\2\2\2\u0183=\3\2\2\2\u0184\u0186\5<\37\2\u0185\u0187\5> \2\u0186\u0185"+
		"\3\2\2\2\u0186\u0187\3\2\2\2\u0187?\3\2\2\2\u0188\u0189\7\'\2\2\u0189"+
		"\u018a\7-\2\2\u018a\u018b\5\\/\2\u018b\u018c\7.\2\2\u018cA\3\2\2\2\u018d"+
		"\u0194\t\13\2\2\u018e\u0194\5L\'\2\u018f\u0190\7\b\2\2\u0190\u0191\7-"+
		"\2\2\u0191\u0192\7Z\2\2\u0192\u0194\7.\2\2\u0193\u018d\3\2\2\2\u0193\u018e"+
		"\3\2\2\2\u0193\u018f\3\2\2\2\u0194C\3\2\2\2\u0195\u0196\7%\2\2\u0196\u0199"+
		"\7-\2\2\u0197\u019a\5\\/\2\u0198\u019a\5.\30\2\u0199\u0197\3\2\2\2\u0199"+
		"\u0198\3\2\2\2\u019a\u019b\3\2\2\2\u019b\u019c\7.\2\2\u019cE\3\2\2\2\u019d"+
		"\u01a1\5H%\2\u019e\u01a0\5J&\2\u019f\u019e\3\2\2\2\u01a0\u01a3\3\2\2\2"+
		"\u01a1\u019f\3\2\2\2\u01a1\u01a2\3\2\2\2\u01a2G\3\2\2\2\u01a3\u01a1\3"+
		"\2\2\2\u01a4\u01a5\b%\1\2\u01a5\u01ae\7Z\2\2\u01a6\u01a7\7-\2\2\u01a7"+
		"\u01a8\5F$\2\u01a8\u01a9\7.\2\2\u01a9\u01ae\3\2\2\2\u01aa\u01ab\7Z\2\2"+
		"\u01ab\u01ac\7H\2\2\u01ac\u01ae\7\\\2\2\u01ad\u01a4\3\2\2\2\u01ad\u01a6"+
		"\3\2\2\2\u01ad\u01aa\3\2\2\2\u01ae\u01c2\3\2\2\2\u01af\u01b0\f\6\2\2\u01b0"+
		"\u01b2\7/\2\2\u01b1\u01b3\5(\25\2\u01b2\u01b1\3\2\2\2\u01b2\u01b3\3\2"+
		"\2\2\u01b3\u01b4\3\2\2\2\u01b4\u01c1\7\60\2\2\u01b5\u01b6\f\5\2\2\u01b6"+
		"\u01b7\7-\2\2\u01b7\u01b8\5T+\2\u01b8\u01b9\7.\2\2\u01b9\u01c1\3\2\2\2"+
		"\u01ba\u01bb\f\4\2\2\u01bb\u01bd\7-\2\2\u01bc\u01be\5Z.\2\u01bd\u01bc"+
		"\3\2\2\2\u01bd\u01be\3\2\2\2\u01be\u01bf\3\2\2\2\u01bf\u01c1\7.\2\2\u01c0"+
		"\u01af\3\2\2\2\u01c0\u01b5\3\2\2\2\u01c0\u01ba\3\2\2\2\u01c1\u01c4\3\2"+
		"\2\2\u01c2\u01c0\3\2\2\2\u01c2\u01c3\3\2\2\2\u01c3I\3\2\2\2\u01c4\u01c2"+
		"\3\2\2\2\u01c5\u01c6\7\t\2\2\u01c6\u01c8\7-\2\2\u01c7\u01c9\7]\2\2\u01c8"+
		"\u01c7\3\2\2\2\u01c9\u01ca\3\2\2\2\u01ca\u01c8\3\2\2\2\u01ca\u01cb\3\2"+
		"\2\2\u01cb\u01cc\3\2\2\2\u01cc\u01cf\7.\2\2\u01cd\u01cf\5L\'\2\u01ce\u01c5"+
		"\3\2\2\2\u01ce\u01cd\3\2\2\2\u01cfK\3\2\2\2\u01d0\u01d1\7\n\2\2\u01d1"+
		"\u01d2\7-\2\2\u01d2\u01d3\7-\2\2\u01d3\u01d4\5N(\2\u01d4\u01d5\7.\2\2"+
		"\u01d5\u01d6\7.\2\2\u01d6M\3\2\2\2\u01d7\u01d9\5P)\2\u01d8\u01d7\3\2\2"+
		"\2\u01d8\u01d9\3\2\2\2\u01d9\u01e0\3\2\2\2\u01da\u01dc\7J\2\2\u01db\u01dd"+
		"\5P)\2\u01dc\u01db\3\2\2\2\u01dc\u01dd\3\2\2\2\u01dd\u01df\3\2\2\2\u01de"+
		"\u01da\3\2\2\2\u01df\u01e2\3\2\2\2\u01e0\u01de\3\2\2\2\u01e0\u01e1\3\2"+
		"\2\2\u01e1O\3\2\2\2\u01e2\u01e0\3\2\2\2\u01e3\u01e9\n\f\2\2\u01e4\u01e6"+
		"\7-\2\2\u01e5\u01e7\5\n\6\2\u01e6\u01e5\3\2\2\2\u01e6\u01e7\3\2\2\2\u01e7"+
		"\u01e8\3\2\2\2\u01e8\u01ea\7.\2\2\u01e9\u01e4\3\2\2\2\u01e9\u01ea\3\2"+
		"\2\2\u01eaQ\3\2\2\2\u01eb\u01f1\n\r\2\2\u01ec\u01ed\7-\2\2\u01ed\u01ee"+
		"\5R*\2\u01ee\u01ef\7.\2\2\u01ef\u01f1\3\2\2\2\u01f0\u01eb\3\2\2\2\u01f0"+
		"\u01ec\3\2\2\2\u01f1\u01f4\3\2\2\2\u01f2\u01f0\3\2\2\2\u01f2\u01f3\3\2"+
		"\2\2\u01f3S\3\2\2\2\u01f4\u01f2\3\2\2\2\u01f5\u01f8\5V,\2\u01f6\u01f7"+
		"\7J\2\2\u01f7\u01f9\7Y\2\2\u01f8\u01f6\3\2\2\2\u01f8\u01f9\3\2\2\2\u01f9"+
		"U\3\2\2\2\u01fa\u01ff\5X-\2\u01fb\u01fc\7J\2\2\u01fc\u01fe\5X-\2\u01fd"+
		"\u01fb\3\2\2\2\u01fe\u0201\3\2\2\2\u01ff\u01fd\3\2\2\2\u01ff\u0200\3\2"+
		"\2\2\u0200W\3\2\2\2\u0201\u01ff\3\2\2\2\u0202\u0203\5\62\32\2\u0203\u0204"+
		"\5F$\2\u0204\u020a\3\2\2\2\u0205\u0207\5\64\33\2\u0206\u0208\5^\60\2\u0207"+
		"\u0206\3\2\2\2\u0207\u0208\3\2\2\2\u0208\u020a\3\2\2\2\u0209\u0202\3\2"+
		"\2\2\u0209\u0205\3\2\2\2\u020aY\3\2\2\2\u020b\u0210\7Z\2\2\u020c\u020d"+
		"\7J\2\2\u020d\u020f\7Z\2\2\u020e\u020c\3\2\2\2\u020f\u0212\3\2\2\2\u0210"+
		"\u020e\3\2\2\2\u0210\u0211\3\2\2\2\u0211[\3\2\2\2\u0212\u0210\3\2\2\2"+
		"\u0213\u0215\5> \2\u0214\u0216\5^\60\2\u0215\u0214\3\2\2\2\u0215\u0216"+
		"\3\2\2\2\u0216]\3\2\2\2\u0217\u021b\5`\61\2\u0218\u021a\5J&\2\u0219\u0218"+
		"\3\2\2\2\u021a\u021d\3\2\2\2\u021b\u0219\3\2\2\2\u021b\u021c\3\2\2\2\u021c"+
		"_\3\2\2\2\u021d\u021b\3\2\2\2\u021e\u021f\b\61\1\2\u021f\u0220\7-\2\2"+
		"\u0220\u0221\5^\60\2\u0221\u0225\7.\2\2\u0222\u0224\5J&\2\u0223\u0222"+
		"\3\2\2\2\u0224\u0227\3\2\2\2\u0225\u0223\3\2\2\2\u0225\u0226\3\2\2\2\u0226"+
		"\u023c\3\2\2\2\u0227\u0225\3\2\2\2\u0228\u022a\7/\2\2\u0229\u022b\5(\25"+
		"\2\u022a\u0229\3\2\2\2\u022a\u022b\3\2\2\2\u022b\u022c\3\2\2\2\u022c\u023c"+
		"\7\60\2\2\u022d\u022e\7/\2\2\u022e\u022f\7=\2\2\u022f\u023c\7\60\2\2\u0230"+
		"\u0232\7-\2\2\u0231\u0233\5T+\2\u0232\u0231\3\2\2\2\u0232\u0233\3\2\2"+
		"\2\u0233\u0234\3\2\2\2\u0234\u0238\7.\2\2\u0235\u0237\5J&\2\u0236\u0235"+
		"\3\2\2\2\u0237\u023a\3\2\2\2\u0238\u0236\3\2\2\2\u0238\u0239\3\2\2\2\u0239"+
		"\u023c\3\2\2\2\u023a\u0238\3\2\2\2\u023b\u021e\3\2\2\2\u023b\u0228\3\2"+
		"\2\2\u023b\u022d\3\2\2\2\u023b\u0230\3\2\2\2\u023c\u0255\3\2\2\2\u023d"+
		"\u023e\f\5\2\2\u023e\u0240\7/\2\2\u023f\u0241\5(\25\2\u0240\u023f\3\2"+
		"\2\2\u0240\u0241\3\2\2\2\u0241\u0242\3\2\2\2\u0242\u0254\7\60\2\2\u0243"+
		"\u0244\f\4\2\2\u0244\u0245\7/\2\2\u0245\u0246\7=\2\2\u0246\u0254\7\60"+
		"\2\2\u0247\u0248\f\3\2\2\u0248\u024a\7-\2\2\u0249\u024b\5T+\2\u024a\u0249"+
		"\3\2\2\2\u024a\u024b\3\2\2\2\u024b\u024c\3\2\2\2\u024c\u0250\7.\2\2\u024d"+
		"\u024f\5J&\2\u024e\u024d\3\2\2\2\u024f\u0252\3\2\2\2\u0250\u024e\3\2\2"+
		"\2\u0250\u0251\3\2\2\2\u0251\u0254\3\2\2\2\u0252\u0250\3\2\2\2\u0253\u023d"+
		"\3\2\2\2\u0253\u0243\3\2\2\2\u0253\u0247\3\2\2\2\u0254\u0257\3\2\2\2\u0255"+
		"\u0253\3\2\2\2\u0255\u0256\3\2\2\2\u0256a\3\2\2\2\u0257\u0255\3\2\2\2"+
		"\u0258\u0261\5(\25\2\u0259\u025a\7\61\2\2\u025a\u025c\5d\63\2\u025b\u025d"+
		"\7J\2\2\u025c\u025b\3\2\2\2\u025c\u025d\3\2\2\2\u025d\u025e\3\2\2\2\u025e"+
		"\u025f\7\62\2\2\u025f\u0261\3\2\2\2\u0260\u0258\3\2\2\2\u0260\u0259\3"+
		"\2\2\2\u0261c\3\2\2\2\u0262\u0264\5f\64\2\u0263\u0262\3\2\2\2\u0263\u0264"+
		"\3\2\2\2\u0264\u0265\3\2\2\2\u0265\u026d\5b\62\2\u0266\u0268\7J\2\2\u0267"+
		"\u0269\5f\64\2\u0268\u0267\3\2\2\2\u0268\u0269\3\2\2\2\u0269\u026a\3\2"+
		"\2\2\u026a\u026c\5b\62\2\u026b\u0266\3\2\2\2\u026c\u026f\3\2\2\2\u026d"+
		"\u026b\3\2\2\2\u026d\u026e\3\2\2\2\u026ee\3\2\2\2\u026f\u026d\3\2\2\2"+
		"\u0270\u0271\5h\65\2\u0271\u0272\7K\2\2\u0272g\3\2\2\2\u0273\u0275\5j"+
		"\66\2\u0274\u0273\3\2\2\2\u0275\u0276\3\2\2\2\u0276\u0274\3\2\2\2\u0276"+
		"\u0277\3\2\2\2\u0277i\3\2\2\2\u0278\u0279\7/\2\2\u0279\u027a\5.\30\2\u027a"+
		"\u027b\7\60\2\2\u027b\u027f\3\2\2\2\u027c\u027d\7X\2\2\u027d\u027f\7Z"+
		"\2\2\u027e\u0278\3\2\2\2\u027e\u027c\3\2\2\2\u027fk\3\2\2\2\u0280\u0287"+
		"\5n8\2\u0281\u0287\5p9\2\u0282\u0287\5v<\2\u0283\u0287\5x=\2\u0284\u0287"+
		"\5z>\2\u0285\u0287\5\u0082B\2\u0286\u0280\3\2\2\2\u0286\u0281\3\2\2\2"+
		"\u0286\u0282\3\2\2\2\u0286\u0283\3\2\2\2\u0286\u0284\3\2\2\2\u0286\u0285"+
		"\3\2\2\2\u0287m\3\2\2\2\u0288\u0289\7Z\2\2\u0289\u028a\7H\2\2\u028a\u0294"+
		"\5l\67\2\u028b\u028c\7\f\2\2\u028c\u028d\5.\30\2\u028d\u028e\7H\2\2\u028e"+
		"\u028f\5l\67\2\u028f\u0294\3\2\2\2\u0290\u0291\7\20\2\2\u0291\u0292\7"+
		"H\2\2\u0292\u0294\5l\67\2\u0293\u0288\3\2\2\2\u0293\u028b\3\2\2\2\u0293"+
		"\u0290\3\2\2\2\u0294o\3\2\2\2\u0295\u0297\7\61\2\2\u0296\u0298\5r:\2\u0297"+
		"\u0296\3\2\2\2\u0297\u0298\3\2\2\2\u0298\u0299\3\2\2\2\u0299\u029a\7\62"+
		"\2\2\u029aq\3\2\2\2\u029b\u029d\5t;\2\u029c\u029b\3\2\2\2\u029d\u029e"+
		"\3\2\2\2\u029e\u029c\3\2\2\2\u029e\u029f\3\2\2\2\u029fs\3\2\2\2\u02a0"+
		"\u02a3\5l\67\2\u02a1\u02a3\5\60\31\2\u02a2\u02a0\3\2\2\2\u02a2\u02a1\3"+
		"\2\2\2\u02a3u\3\2\2\2\u02a4\u02a6\5,\27\2\u02a5\u02a4\3\2\2\2\u02a5\u02a6"+
		"\3\2\2\2\u02a6\u02a7\3\2\2\2\u02a7\u02a8\7I\2\2\u02a8w\3\2\2\2\u02a9\u02aa"+
		"\7\26\2\2\u02aa\u02ab\7-\2\2\u02ab\u02ac\5,\27\2\u02ac\u02ad\7.\2\2\u02ad"+
		"\u02b0\5l\67\2\u02ae\u02af\7\23\2\2\u02af\u02b1\5l\67\2\u02b0\u02ae\3"+
		"\2\2\2\u02b0\u02b1\3\2\2\2\u02b1\u02b9\3\2\2\2\u02b2\u02b3\7\"\2\2\u02b3"+
		"\u02b4\7-\2\2\u02b4\u02b5\5,\27\2\u02b5\u02b6\7.\2\2\u02b6\u02b7\5l\67"+
		"\2\u02b7\u02b9\3\2\2\2\u02b8\u02a9\3\2\2\2\u02b8\u02b2\3\2\2\2\u02b9y"+
		"\3\2\2\2\u02ba\u02bb\7$\2\2\u02bb\u02bc\7-\2\2\u02bc\u02bd\5,\27\2\u02bd"+
		"\u02be\7.\2\2\u02be\u02bf\5l\67\2\u02bf\u02cf\3\2\2\2\u02c0\u02c1\7\21"+
		"\2\2\u02c1\u02c2\5l\67\2\u02c2\u02c3\7$\2\2\u02c3\u02c4\7-\2\2\u02c4\u02c5"+
		"\5,\27\2\u02c5\u02c6\7.\2\2\u02c6\u02c7\7I\2\2\u02c7\u02cf\3\2\2\2\u02c8"+
		"\u02c9\7\25\2\2\u02c9\u02ca\7-\2\2\u02ca\u02cb\5|?\2\u02cb\u02cc\7.\2"+
		"\2\u02cc\u02cd\5l\67\2\u02cd\u02cf\3\2\2\2\u02ce\u02ba\3\2\2\2\u02ce\u02c0"+
		"\3\2\2\2\u02ce\u02c8\3\2\2\2\u02cf{\3\2\2\2\u02d0\u02d5\5~@\2\u02d1\u02d3"+
		"\5,\27\2\u02d2\u02d1\3\2\2\2\u02d2\u02d3\3\2\2\2\u02d3\u02d5\3\2\2\2\u02d4"+
		"\u02d0\3\2\2\2\u02d4\u02d2\3\2\2\2\u02d5\u02d6\3\2\2\2\u02d6\u02d8\7I"+
		"\2\2\u02d7\u02d9\5\u0080A\2\u02d8\u02d7\3\2\2\2\u02d8\u02d9\3\2\2\2\u02d9"+
		"\u02da\3\2\2\2\u02da\u02dc\7I\2\2\u02db\u02dd\5\u0080A\2\u02dc\u02db\3"+
		"\2\2\2\u02dc\u02dd\3\2\2\2\u02dd}\3\2\2\2\u02de\u02e0\5\62\32\2\u02df"+
		"\u02e1\58\35\2\u02e0\u02df\3\2\2\2\u02e0\u02e1\3\2\2\2\u02e1\177\3\2\2"+
		"\2\u02e2\u02e7\5(\25\2\u02e3\u02e4\7J\2\2\u02e4\u02e6\5(\25\2\u02e5\u02e3"+
		"\3\2\2\2\u02e6\u02e9\3\2\2\2\u02e7\u02e5\3\2\2\2\u02e7\u02e8\3\2\2\2\u02e8"+
		"\u0081\3\2\2\2\u02e9\u02e7\3\2\2\2\u02ea\u02f0\t\16\2\2\u02eb\u02ed\7"+
		"!\2\2\u02ec\u02ee\5,\27\2\u02ed\u02ec\3\2\2\2\u02ed\u02ee\3\2\2\2\u02ee"+
		"\u02f0\3\2\2\2\u02ef\u02ea\3\2\2\2\u02ef\u02eb\3\2\2\2\u02f0\u02f1\3\2"+
		"\2\2\u02f1\u02f2\7I\2\2\u02f2\u0083\3\2\2\2\u02f3\u02f5\5\u0086D\2\u02f4"+
		"\u02f3\3\2\2\2\u02f4\u02f5\3\2\2\2\u02f5\u02f6\3\2\2\2\u02f6\u02f7\7\2"+
		"\2\3\u02f7\u0085\3\2\2\2\u02f8\u02fa\5\u0088E\2\u02f9\u02f8\3\2\2\2\u02fa"+
		"\u02fb\3\2\2\2\u02fb\u02f9\3\2\2\2\u02fb\u02fc\3\2\2\2\u02fc\u0087\3\2"+
		"\2\2\u02fd\u0301\5\u008aF\2\u02fe\u0301\5\60\31\2\u02ff\u0301\7I\2\2\u0300"+
		"\u02fd\3\2\2\2\u0300\u02fe\3\2\2\2\u0300\u02ff\3\2\2\2\u0301\u0089\3\2"+
		"\2\2\u0302\u0304\5\62\32\2\u0303\u0302\3\2\2\2\u0303\u0304\3\2\2\2\u0304"+
		"\u0305\3\2\2\2\u0305\u0307\5F$\2\u0306\u0308\5\u008cG\2\u0307\u0306\3"+
		"\2\2\2\u0307\u0308\3\2\2\2\u0308\u0309\3\2\2\2\u0309\u030a\5p9\2\u030a"+
		"\u008b\3\2\2\2\u030b\u030d\5\60\31\2\u030c\u030b\3\2\2\2\u030d\u030e\3"+
		"\2\2\2\u030e\u030c\3\2\2\2\u030e\u030f\3\2\2\2\u030f\u008d\3\2\2\2d\u0093"+
		"\u009d\u00a4\u00a9\u00b5\u00b9\u00c1\u00c7\u00c9\u00d1\u00d7\u00e5\u00f0"+
		"\u00f7\u00ff\u0107\u010f\u0117\u011f\u0127\u012f\u0137\u013f\u0148\u0150"+
		"\u0159\u0160\u0167\u016c\u0171\u0178\u017e\u0182\u0186\u0193\u0199\u01a1"+
		"\u01ad\u01b2\u01bd\u01c0\u01c2\u01ca\u01ce\u01d8\u01dc\u01e0\u01e6\u01e9"+
		"\u01f0\u01f2\u01f8\u01ff\u0207\u0209\u0210\u0215\u021b\u0225\u022a\u0232"+
		"\u0238\u023b\u0240\u024a\u0250\u0253\u0255\u025c\u0260\u0263\u0268\u026d"+
		"\u0276\u027e\u0286\u0293\u0297\u029e\u02a2\u02a5\u02b0\u02b8\u02ce\u02d2"+
		"\u02d4\u02d8\u02dc\u02e0\u02e7\u02ed\u02ef\u02f4\u02fb\u0300\u0303\u0307"+
		"\u030e";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}