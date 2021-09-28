// Generated from d:\Projects\dotNET\appDev-NET\Metaprogramming\Antrl\Capl_grammar\capl-g4\grammar\Capl.g4 by ANTLR 4.8
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
		T__0=1, T__1=2, T__2=3, StopMeasurement=4, Start=5, ErrorFrame=6, Key=7, 
		On=8, Variables=9, Break=10, Case=11, Char=12, Byte=13, Continue=14, Default=15, 
		Do=16, Double=17, Else=18, Float=19, For=20, If=21, Int=22, Word=23, Dword=24, 
		Message=25, EnvVar=26, Timer=27, MsTimer=28, Long=29, Int64=30, Return=31, 
		Switch=32, Void=33, While=34, LeftParen=35, RightParen=36, LeftBracket=37, 
		RightBracket=38, LeftBrace=39, RightBrace=40, Less=41, LessEqual=42, Greater=43, 
		GreaterEqual=44, LeftShift=45, RightShift=46, Plus=47, PlusPlus=48, Minus=49, 
		MinusMinus=50, Div=51, Mod=52, And=53, Or=54, AndAnd=55, OrOr=56, Caret=57, 
		Not=58, Tilde=59, Question=60, Colon=61, Semi=62, Comma=63, Assign=64, 
		StarAssign=65, DivAssign=66, ModAssign=67, PlusAssign=68, MinusAssign=69, 
		LeftShiftAssign=70, RightShiftAssign=71, AndAssign=72, XorAssign=73, OrAssign=74, 
		Star=75, Equal=76, NotEqual=77, Ellipsis=78, KeyIdentifier=79, Quote=80, 
		Identifier=81, This=82, Dot=83, Constant=84, DigitSequence=85, StringLiteral=86, 
		Whitespace=87, Newline=88, BlockComment=89, LineComment=90;
	public static final int
		RULE_primaryExpression = 0, RULE_startBlock = 1, RULE_variableBlock = 2, 
		RULE_eventBlock = 3, RULE_timerBlock = 4, RULE_errorFrame = 5, RULE_messageBlock = 6, 
		RULE_stopMeasurement = 7, RULE_envBlock = 8, RULE_postfixExpression = 9, 
		RULE_argumentExpressionList = 10, RULE_unaryExpression = 11, RULE_unaryOperator = 12, 
		RULE_castExpression = 13, RULE_multiplicativeExpression = 14, RULE_additiveExpression = 15, 
		RULE_shiftExpression = 16, RULE_relationalExpression = 17, RULE_equalityExpression = 18, 
		RULE_andExpression = 19, RULE_exclusiveOrExpression = 20, RULE_inclusiveOrExpression = 21, 
		RULE_logicalAndExpression = 22, RULE_logicalOrExpression = 23, RULE_conditionalExpression = 24, 
		RULE_assignmentExpression = 25, RULE_assignmentOperator = 26, RULE_expression = 27, 
		RULE_constantExpression = 28, RULE_declaration = 29, RULE_declarationSpecifiers = 30, 
		RULE_declarationSpecifiers2 = 31, RULE_declarationSpecifier = 32, RULE_initDeclaratorList = 33, 
		RULE_initDeclarator = 34, RULE_typeSpecifier = 35, RULE_messageType = 36, 
		RULE_specifierQualifierList = 37, RULE_declarator = 38, RULE_directDeclarator = 39, 
		RULE_nestedParenthesesBlock = 40, RULE_parameterTypeList = 41, RULE_parameterList = 42, 
		RULE_parameterDeclaration = 43, RULE_identifierList = 44, RULE_typeName = 45, 
		RULE_abstractDeclarator = 46, RULE_directAbstractDeclarator = 47, RULE_initializer = 48, 
		RULE_initializerList = 49, RULE_designation = 50, RULE_designatorList = 51, 
		RULE_designator = 52, RULE_statement = 53, RULE_labeledStatement = 54, 
		RULE_compoundStatement = 55, RULE_blockItemList = 56, RULE_blockItem = 57, 
		RULE_expressionStatement = 58, RULE_selectionStatement = 59, RULE_iterationStatement = 60, 
		RULE_forCondition = 61, RULE_forDeclaration = 62, RULE_forExpression = 63, 
		RULE_jumpStatement = 64, RULE_compilationUnit = 65, RULE_translationUnit = 66, 
		RULE_externalDeclaration = 67, RULE_functionDefinition = 68, RULE_declarationList = 69;
	private static String[] makeRuleNames() {
		return new String[] {
			"primaryExpression", "startBlock", "variableBlock", "eventBlock", "timerBlock", 
			"errorFrame", "messageBlock", "stopMeasurement", "envBlock", "postfixExpression", 
			"argumentExpressionList", "unaryExpression", "unaryOperator", "castExpression", 
			"multiplicativeExpression", "additiveExpression", "shiftExpression", 
			"relationalExpression", "equalityExpression", "andExpression", "exclusiveOrExpression", 
			"inclusiveOrExpression", "logicalAndExpression", "logicalOrExpression", 
			"conditionalExpression", "assignmentExpression", "assignmentOperator", 
			"expression", "constantExpression", "declaration", "declarationSpecifiers", 
			"declarationSpecifiers2", "declarationSpecifier", "initDeclaratorList", 
			"initDeclarator", "typeSpecifier", "messageType", "specifierQualifierList", 
			"declarator", "directDeclarator", "nestedParenthesesBlock", "parameterTypeList", 
			"parameterList", "parameterDeclaration", "identifierList", "typeName", 
			"abstractDeclarator", "directAbstractDeclarator", "initializer", "initializerList", 
			"designation", "designatorList", "designator", "statement", "labeledStatement", 
			"compoundStatement", "blockItemList", "blockItem", "expressionStatement", 
			"selectionStatement", "iterationStatement", "forCondition", "forDeclaration", 
			"forExpression", "jumpStatement", "compilationUnit", "translationUnit", 
			"externalDeclaration", "functionDefinition", "declarationList"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'100'", "'wake-up'", "'0x555'", "'stopMeasurement'", "'start'", 
			"'errorFrame'", "'key'", "'on'", "'variables'", "'break'", "'case'", 
			"'char'", "'byte'", "'continue'", "'default'", "'do'", "'double'", "'else'", 
			"'float'", "'for'", "'if'", "'int'", "'word'", "'dword'", "'message'", 
			"'envVar'", "'timer'", "'msTimer'", "'long'", "'int64'", "'return'", 
			"'switch'", "'void'", "'while'", "'('", "')'", "'['", "']'", "'{'", "'}'", 
			"'<'", "'<='", "'>'", "'>='", "'<<'", "'>>'", "'+'", "'++'", "'-'", "'--'", 
			"'/'", "'%'", "'&'", "'|'", "'&&'", "'||'", "'^'", "'!'", "'~'", "'?'", 
			"':'", "';'", "','", "'='", "'*='", "'/='", "'%='", "'+='", "'-='", "'<<='", 
			"'>>='", "'&='", "'^='", "'|='", "'*'", "'=='", "'!='", "'...'", null, 
			"'''", null, "'this'", "'.'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, "StopMeasurement", "Start", "ErrorFrame", "Key", 
			"On", "Variables", "Break", "Case", "Char", "Byte", "Continue", "Default", 
			"Do", "Double", "Else", "Float", "For", "If", "Int", "Word", "Dword", 
			"Message", "EnvVar", "Timer", "MsTimer", "Long", "Int64", "Return", "Switch", 
			"Void", "While", "LeftParen", "RightParen", "LeftBracket", "RightBracket", 
			"LeftBrace", "RightBrace", "Less", "LessEqual", "Greater", "GreaterEqual", 
			"LeftShift", "RightShift", "Plus", "PlusPlus", "Minus", "MinusMinus", 
			"Div", "Mod", "And", "Or", "AndAnd", "OrOr", "Caret", "Not", "Tilde", 
			"Question", "Colon", "Semi", "Comma", "Assign", "StarAssign", "DivAssign", 
			"ModAssign", "PlusAssign", "MinusAssign", "LeftShiftAssign", "RightShiftAssign", 
			"AndAssign", "XorAssign", "OrAssign", "Star", "Equal", "NotEqual", "Ellipsis", 
			"KeyIdentifier", "Quote", "Identifier", "This", "Dot", "Constant", "DigitSequence", 
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
		public List<VariableBlockContext> variableBlock() {
			return getRuleContexts(VariableBlockContext.class);
		}
		public VariableBlockContext variableBlock(int i) {
			return getRuleContext(VariableBlockContext.class,i);
		}
		public List<EventBlockContext> eventBlock() {
			return getRuleContexts(EventBlockContext.class);
		}
		public EventBlockContext eventBlock(int i) {
			return getRuleContext(EventBlockContext.class,i);
		}
		public List<TimerBlockContext> timerBlock() {
			return getRuleContexts(TimerBlockContext.class);
		}
		public TimerBlockContext timerBlock(int i) {
			return getRuleContext(TimerBlockContext.class,i);
		}
		public List<ErrorFrameContext> errorFrame() {
			return getRuleContexts(ErrorFrameContext.class);
		}
		public ErrorFrameContext errorFrame(int i) {
			return getRuleContext(ErrorFrameContext.class,i);
		}
		public List<EnvBlockContext> envBlock() {
			return getRuleContexts(EnvBlockContext.class);
		}
		public EnvBlockContext envBlock(int i) {
			return getRuleContext(EnvBlockContext.class,i);
		}
		public List<FunctionDefinitionContext> functionDefinition() {
			return getRuleContexts(FunctionDefinitionContext.class);
		}
		public FunctionDefinitionContext functionDefinition(int i) {
			return getRuleContext(FunctionDefinitionContext.class,i);
		}
		public List<StartBlockContext> startBlock() {
			return getRuleContexts(StartBlockContext.class);
		}
		public StartBlockContext startBlock(int i) {
			return getRuleContext(StartBlockContext.class,i);
		}
		public List<MessageBlockContext> messageBlock() {
			return getRuleContexts(MessageBlockContext.class);
		}
		public MessageBlockContext messageBlock(int i) {
			return getRuleContext(MessageBlockContext.class,i);
		}
		public List<StopMeasurementContext> stopMeasurement() {
			return getRuleContexts(StopMeasurementContext.class);
		}
		public StopMeasurementContext stopMeasurement(int i) {
			return getRuleContext(StopMeasurementContext.class,i);
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
			int _alt;
			setState(169);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
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
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(166);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						setState(164);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
						case 1:
							{
							setState(155);
							variableBlock();
							}
							break;
						case 2:
							{
							setState(156);
							eventBlock();
							}
							break;
						case 3:
							{
							setState(157);
							timerBlock();
							}
							break;
						case 4:
							{
							setState(158);
							errorFrame();
							}
							break;
						case 5:
							{
							setState(159);
							envBlock();
							}
							break;
						case 6:
							{
							setState(160);
							functionDefinition();
							}
							break;
						case 7:
							{
							setState(161);
							startBlock();
							}
							break;
						case 8:
							{
							setState(162);
							messageBlock();
							}
							break;
						case 9:
							{
							setState(163);
							stopMeasurement();
							}
							break;
						}
						} 
					}
					setState(168);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
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

	public static class StartBlockContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode Start() { return getToken(CaplParser.Start, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public StartBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_startBlock; }
	}

	public final StartBlockContext startBlock() throws RecognitionException {
		StartBlockContext _localctx = new StartBlockContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_startBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(171);
			match(On);
			setState(172);
			match(Start);
			setState(173);
			match(LeftBrace);
			setState(175);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(174);
				blockItemList();
				}
			}

			setState(177);
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

	public static class VariableBlockContext extends ParserRuleContext {
		public TerminalNode Variables() { return getToken(CaplParser.Variables, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public VariableBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variableBlock; }
	}

	public final VariableBlockContext variableBlock() throws RecognitionException {
		VariableBlockContext _localctx = new VariableBlockContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_variableBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(179);
			match(Variables);
			setState(180);
			match(LeftBrace);
			setState(182);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(181);
				blockItemList();
				}
			}

			setState(184);
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

	public static class EventBlockContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode Key() { return getToken(CaplParser.Key, 0); }
		public TerminalNode KeyIdentifier() { return getToken(CaplParser.KeyIdentifier, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public EventBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_eventBlock; }
	}

	public final EventBlockContext eventBlock() throws RecognitionException {
		EventBlockContext _localctx = new EventBlockContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_eventBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(186);
			match(On);
			setState(187);
			match(Key);
			setState(188);
			match(KeyIdentifier);
			setState(189);
			match(LeftBrace);
			setState(191);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(190);
				blockItemList();
				}
			}

			setState(193);
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

	public static class TimerBlockContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode Timer() { return getToken(CaplParser.Timer, 0); }
		public List<TerminalNode> Identifier() { return getTokens(CaplParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(CaplParser.Identifier, i);
		}
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public TerminalNode Dot() { return getToken(CaplParser.Dot, 0); }
		public TerminalNode Star() { return getToken(CaplParser.Star, 0); }
		public TimerBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_timerBlock; }
	}

	public final TimerBlockContext timerBlock() throws RecognitionException {
		TimerBlockContext _localctx = new TimerBlockContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_timerBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(195);
			match(On);
			setState(196);
			match(Timer);
			setState(197);
			match(Identifier);
			setState(200);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Dot) {
				{
				setState(198);
				match(Dot);
				setState(199);
				_la = _input.LA(1);
				if ( !(_la==Star || _la==Identifier) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
			}

			setState(202);
			match(LeftBrace);
			setState(203);
			blockItemList();
			setState(204);
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

	public static class ErrorFrameContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public TerminalNode ErrorFrame() { return getToken(CaplParser.ErrorFrame, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public ErrorFrameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_errorFrame; }
	}

	public final ErrorFrameContext errorFrame() throws RecognitionException {
		ErrorFrameContext _localctx = new ErrorFrameContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_errorFrame);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(206);
			match(On);
			{
			setState(207);
			match(ErrorFrame);
			}
			setState(208);
			match(LeftBrace);
			setState(210);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(209);
				blockItemList();
				}
			}

			setState(212);
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

	public static class MessageBlockContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode Message() { return getToken(CaplParser.Message, 0); }
		public List<TerminalNode> Identifier() { return getTokens(CaplParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(CaplParser.Identifier, i);
		}
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public TerminalNode Dot() { return getToken(CaplParser.Dot, 0); }
		public TerminalNode Star() { return getToken(CaplParser.Star, 0); }
		public MessageBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_messageBlock; }
	}

	public final MessageBlockContext messageBlock() throws RecognitionException {
		MessageBlockContext _localctx = new MessageBlockContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_messageBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(214);
			match(On);
			setState(215);
			match(Message);
			setState(216);
			match(Identifier);
			setState(219);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Dot) {
				{
				setState(217);
				match(Dot);
				setState(218);
				_la = _input.LA(1);
				if ( !(_la==Star || _la==Identifier) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
			}

			setState(221);
			match(LeftBrace);
			setState(222);
			blockItemList();
			setState(223);
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

	public static class StopMeasurementContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode StopMeasurement() { return getToken(CaplParser.StopMeasurement, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public StopMeasurementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stopMeasurement; }
	}

	public final StopMeasurementContext stopMeasurement() throws RecognitionException {
		StopMeasurementContext _localctx = new StopMeasurementContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_stopMeasurement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(225);
			match(On);
			setState(226);
			match(StopMeasurement);
			setState(227);
			match(LeftBrace);
			setState(228);
			blockItemList();
			setState(229);
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

	public static class EnvBlockContext extends ParserRuleContext {
		public TerminalNode On() { return getToken(CaplParser.On, 0); }
		public TerminalNode EnvVar() { return getToken(CaplParser.EnvVar, 0); }
		public TerminalNode Identifier() { return getToken(CaplParser.Identifier, 0); }
		public TerminalNode LeftBrace() { return getToken(CaplParser.LeftBrace, 0); }
		public BlockItemListContext blockItemList() {
			return getRuleContext(BlockItemListContext.class,0);
		}
		public TerminalNode RightBrace() { return getToken(CaplParser.RightBrace, 0); }
		public EnvBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_envBlock; }
	}

	public final EnvBlockContext envBlock() throws RecognitionException {
		EnvBlockContext _localctx = new EnvBlockContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_envBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(231);
			match(On);
			setState(232);
			match(EnvVar);
			setState(233);
			match(Identifier);
			setState(234);
			match(LeftBrace);
			setState(235);
			blockItemList();
			setState(236);
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
		enterRule(_localctx, 18, RULE_postfixExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(249);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
			case 1:
				{
				setState(238);
				primaryExpression();
				}
				break;
			case 2:
				{
				setState(239);
				match(LeftParen);
				setState(240);
				typeName();
				setState(241);
				match(RightParen);
				setState(242);
				match(LeftBrace);
				setState(243);
				initializerList();
				setState(245);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(244);
					match(Comma);
					}
				}

				setState(247);
				match(RightBrace);
				}
				break;
			}
			setState(263);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LeftParen) | (1L << LeftBracket) | (1L << PlusPlus) | (1L << MinusMinus))) != 0)) {
				{
				setState(261);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LeftBracket:
					{
					setState(251);
					match(LeftBracket);
					setState(252);
					expression();
					setState(253);
					match(RightBracket);
					}
					break;
				case LeftParen:
					{
					setState(255);
					match(LeftParen);
					setState(257);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
					case 1:
						{
						setState(256);
						argumentExpressionList();
						}
						break;
					}
					setState(259);
					match(RightParen);
					}
					break;
				case PlusPlus:
				case MinusMinus:
					{
					setState(260);
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
				setState(265);
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
		enterRule(_localctx, 20, RULE_argumentExpressionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(266);
			assignmentExpression();
			setState(271);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(267);
				match(Comma);
				setState(268);
				assignmentExpression();
				}
				}
				setState(273);
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
		enterRule(_localctx, 22, RULE_unaryExpression);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(277);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,16,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(274);
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
				}
				setState(279);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,16,_ctx);
			}
			setState(284);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
			case 1:
				{
				setState(280);
				postfixExpression();
				}
				break;
			case 2:
				{
				setState(281);
				unaryOperator();
				setState(282);
				castExpression();
				}
				break;
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
		enterRule(_localctx, 24, RULE_unaryOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(286);
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
		enterRule(_localctx, 26, RULE_castExpression);
		try {
			setState(295);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,18,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(288);
				match(LeftParen);
				setState(289);
				typeName();
				setState(290);
				match(RightParen);
				setState(291);
				castExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(293);
				unaryExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(294);
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
		enterRule(_localctx, 28, RULE_multiplicativeExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(297);
			castExpression();
			setState(302);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (Div - 51)) | (1L << (Mod - 51)) | (1L << (Star - 51)))) != 0)) {
				{
				{
				setState(298);
				_la = _input.LA(1);
				if ( !(((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (Div - 51)) | (1L << (Mod - 51)) | (1L << (Star - 51)))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(299);
				castExpression();
				}
				}
				setState(304);
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
		enterRule(_localctx, 30, RULE_additiveExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(305);
			multiplicativeExpression();
			setState(310);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Plus || _la==Minus) {
				{
				{
				setState(306);
				_la = _input.LA(1);
				if ( !(_la==Plus || _la==Minus) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(307);
				multiplicativeExpression();
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
		enterRule(_localctx, 32, RULE_shiftExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(313);
			additiveExpression();
			setState(318);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==LeftShift || _la==RightShift) {
				{
				{
				setState(314);
				_la = _input.LA(1);
				if ( !(_la==LeftShift || _la==RightShift) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(315);
				additiveExpression();
				}
				}
				setState(320);
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
		enterRule(_localctx, 34, RULE_relationalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(321);
			shiftExpression();
			setState(326);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) {
				{
				{
				setState(322);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(323);
				shiftExpression();
				}
				}
				setState(328);
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
		enterRule(_localctx, 36, RULE_equalityExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(329);
			relationalExpression();
			setState(334);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Equal || _la==NotEqual) {
				{
				{
				setState(330);
				_la = _input.LA(1);
				if ( !(_la==Equal || _la==NotEqual) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(331);
				relationalExpression();
				}
				}
				setState(336);
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
		enterRule(_localctx, 38, RULE_andExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(337);
			equalityExpression();
			setState(342);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==And) {
				{
				{
				setState(338);
				match(And);
				setState(339);
				equalityExpression();
				}
				}
				setState(344);
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
		enterRule(_localctx, 40, RULE_exclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(345);
			andExpression();
			setState(350);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Caret) {
				{
				{
				setState(346);
				match(Caret);
				setState(347);
				andExpression();
				}
				}
				setState(352);
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
		enterRule(_localctx, 42, RULE_inclusiveOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(353);
			exclusiveOrExpression();
			setState(358);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Or) {
				{
				{
				setState(354);
				match(Or);
				setState(355);
				exclusiveOrExpression();
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
		enterRule(_localctx, 44, RULE_logicalAndExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(361);
			inclusiveOrExpression();
			setState(366);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AndAnd) {
				{
				{
				setState(362);
				match(AndAnd);
				setState(363);
				inclusiveOrExpression();
				}
				}
				setState(368);
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
		enterRule(_localctx, 46, RULE_logicalOrExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(369);
			logicalAndExpression();
			setState(374);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OrOr) {
				{
				{
				setState(370);
				match(OrOr);
				setState(371);
				logicalAndExpression();
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
		enterRule(_localctx, 48, RULE_conditionalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(377);
			logicalOrExpression();
			setState(383);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Question) {
				{
				setState(378);
				match(Question);
				setState(379);
				expression();
				setState(380);
				match(Colon);
				setState(381);
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
		enterRule(_localctx, 50, RULE_assignmentExpression);
		try {
			setState(391);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,30,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(385);
				conditionalExpression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(386);
				unaryExpression();
				setState(387);
				assignmentOperator();
				setState(388);
				assignmentExpression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(390);
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
		enterRule(_localctx, 52, RULE_assignmentOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(393);
			_la = _input.LA(1);
			if ( !(((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)))) != 0)) ) {
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
		enterRule(_localctx, 54, RULE_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(395);
			assignmentExpression();
			setState(400);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(396);
				match(Comma);
				setState(397);
				assignmentExpression();
				}
				}
				setState(402);
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
		enterRule(_localctx, 56, RULE_constantExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(403);
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
		enterRule(_localctx, 58, RULE_declaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(405);
			declarationSpecifiers();
			setState(407);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(406);
				initDeclaratorList();
				}
			}

			setState(409);
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
		enterRule(_localctx, 60, RULE_declarationSpecifiers);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(412); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(411);
				declarationSpecifier();
				}
				}
				setState(414); 
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
		enterRule(_localctx, 62, RULE_declarationSpecifiers2);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(417); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(416);
				declarationSpecifier();
				}
				}
				setState(419); 
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
		enterRule(_localctx, 64, RULE_declarationSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(421);
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
		enterRule(_localctx, 66, RULE_initDeclaratorList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(423);
			initDeclarator();
			setState(428);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(424);
				match(Comma);
				setState(425);
				initDeclarator();
				}
				}
				setState(430);
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
		enterRule(_localctx, 68, RULE_initDeclarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(431);
			declarator();
			setState(434);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Assign) {
				{
				setState(432);
				match(Assign);
				setState(433);
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
		enterRule(_localctx, 70, RULE_typeSpecifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(449);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Void:
				{
				setState(436);
				match(Void);
				}
				break;
			case Char:
				{
				setState(437);
				match(Char);
				}
				break;
			case Byte:
				{
				setState(438);
				match(Byte);
				}
				break;
			case Int:
				{
				setState(439);
				match(Int);
				}
				break;
			case Long:
				{
				setState(440);
				match(Long);
				}
				break;
			case Int64:
				{
				setState(441);
				match(Int64);
				}
				break;
			case Float:
				{
				setState(442);
				match(Float);
				}
				break;
			case Double:
				{
				setState(443);
				match(Double);
				}
				break;
			case Word:
				{
				setState(444);
				match(Word);
				}
				break;
			case Dword:
				{
				setState(445);
				match(Dword);
				}
				break;
			case Timer:
				{
				setState(446);
				match(Timer);
				}
				break;
			case MsTimer:
				{
				setState(447);
				match(MsTimer);
				}
				break;
			case Message:
				{
				setState(448);
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
		public List<TerminalNode> Identifier() { return getTokens(CaplParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(CaplParser.Identifier, i);
		}
		public TerminalNode Dot() { return getToken(CaplParser.Dot, 0); }
		public TerminalNode Star() { return getToken(CaplParser.Star, 0); }
		public MessageTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_messageType; }
	}

	public final MessageTypeContext messageType() throws RecognitionException {
		MessageTypeContext _localctx = new MessageTypeContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_messageType);
		int _la;
		try {
			setState(465);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,39,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(451);
				match(Message);
				setState(452);
				match(Identifier);
				setState(455);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Dot) {
					{
					setState(453);
					match(Dot);
					setState(454);
					_la = _input.LA(1);
					if ( !(_la==Star || _la==Identifier) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(457);
				match(Message);
				setState(458);
				match(Star);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(459);
				match(Message);
				setState(460);
				match(T__0);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(461);
				match(Message);
				setState(462);
				match(T__1);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(463);
				match(Message);
				setState(464);
				match(T__2);
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
		enterRule(_localctx, 74, RULE_specifierQualifierList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(467);
			typeSpecifier();
			setState(469);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(468);
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
		enterRule(_localctx, 76, RULE_declarator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(471);
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
		int _startState = 78;
		enterRecursionRule(_localctx, 78, RULE_directDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(479);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				{
				setState(474);
				match(Identifier);
				}
				break;
			case LeftParen:
				{
				setState(475);
				match(LeftParen);
				setState(476);
				declarator();
				setState(477);
				match(RightParen);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(500);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,45,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(498);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,44,_ctx) ) {
					case 1:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(481);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(482);
						match(LeftBracket);
						setState(484);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,42,_ctx) ) {
						case 1:
							{
							setState(483);
							assignmentExpression();
							}
							break;
						}
						setState(486);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(487);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(488);
						match(LeftParen);
						setState(489);
						parameterTypeList();
						setState(490);
						match(RightParen);
						}
						break;
					case 3:
						{
						_localctx = new DirectDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directDeclarator);
						setState(492);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(493);
						match(LeftParen);
						setState(495);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==Identifier) {
							{
							setState(494);
							identifierList();
							}
						}

						setState(497);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(502);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,45,_ctx);
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
		enterRule(_localctx, 80, RULE_nestedParenthesesBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(510);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2) | (1L << StopMeasurement) | (1L << Start) | (1L << ErrorFrame) | (1L << Key) | (1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Else) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << EnvVar) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << RightBracket) | (1L << LeftBrace) | (1L << RightBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Colon) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Ellipsis - 64)) | (1L << (KeyIdentifier - 64)) | (1L << (Quote - 64)) | (1L << (Identifier - 64)) | (1L << (This - 64)) | (1L << (Dot - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)) | (1L << (Whitespace - 64)) | (1L << (Newline - 64)) | (1L << (BlockComment - 64)) | (1L << (LineComment - 64)))) != 0)) {
				{
				setState(508);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case T__0:
				case T__1:
				case T__2:
				case StopMeasurement:
				case Start:
				case ErrorFrame:
				case Key:
				case On:
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
				case EnvVar:
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
				case Star:
				case Equal:
				case NotEqual:
				case Ellipsis:
				case KeyIdentifier:
				case Quote:
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
					setState(503);
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
					setState(504);
					match(LeftParen);
					setState(505);
					nestedParenthesesBlock();
					setState(506);
					match(RightParen);
					}
					break;
				default:
					throw new NoViableAltException(this);
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
			setState(513);
			parameterList();
			setState(516);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Comma) {
				{
				setState(514);
				match(Comma);
				setState(515);
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
			setState(518);
			parameterDeclaration();
			setState(523);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,49,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(519);
					match(Comma);
					setState(520);
					parameterDeclaration();
					}
					} 
				}
				setState(525);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,49,_ctx);
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
			setState(533);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(526);
				declarationSpecifiers();
				setState(527);
				declarator();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(529);
				declarationSpecifiers2();
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
			setState(535);
			match(Identifier);
			setState(540);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(536);
				match(Comma);
				setState(537);
				match(Identifier);
				}
				}
				setState(542);
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
			setState(543);
			specifierQualifierList();
			setState(545);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==LeftBracket) {
				{
				setState(544);
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
		enterRule(_localctx, 92, RULE_abstractDeclarator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(547);
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
		int _startState = 94;
		enterRecursionRule(_localctx, 94, RULE_directAbstractDeclarator, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(567);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,56,_ctx) ) {
			case 1:
				{
				setState(550);
				match(LeftParen);
				setState(551);
				abstractDeclarator();
				setState(552);
				match(RightParen);
				}
				break;
			case 2:
				{
				setState(554);
				match(LeftBracket);
				setState(556);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,54,_ctx) ) {
				case 1:
					{
					setState(555);
					assignmentExpression();
					}
					break;
				}
				setState(558);
				match(RightBracket);
				}
				break;
			case 3:
				{
				setState(559);
				match(LeftBracket);
				setState(560);
				match(Star);
				setState(561);
				match(RightBracket);
				}
				break;
			case 4:
				{
				setState(562);
				match(LeftParen);
				setState(564);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
					{
					setState(563);
					parameterTypeList();
					}
				}

				setState(566);
				match(RightParen);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(587);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(585);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,59,_ctx) ) {
					case 1:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(569);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(570);
						match(LeftBracket);
						setState(572);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,57,_ctx) ) {
						case 1:
							{
							setState(571);
							assignmentExpression();
							}
							break;
						}
						setState(574);
						match(RightBracket);
						}
						break;
					case 2:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(575);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(576);
						match(LeftBracket);
						setState(577);
						match(Star);
						setState(578);
						match(RightBracket);
						}
						break;
					case 3:
						{
						_localctx = new DirectAbstractDeclaratorContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_directAbstractDeclarator);
						setState(579);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(580);
						match(LeftParen);
						setState(582);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
							{
							setState(581);
							parameterTypeList();
							}
						}

						setState(584);
						match(RightParen);
						}
						break;
					}
					} 
				}
				setState(589);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
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
			setState(598);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case On:
			case Variables:
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
			case LeftParen:
			case LeftBracket:
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
			case Star:
			case Equal:
			case NotEqual:
			case Identifier:
			case Constant:
			case DigitSequence:
			case StringLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(590);
				assignmentExpression();
				}
				break;
			case LeftBrace:
				enterOuterAlt(_localctx, 2);
				{
				setState(591);
				match(LeftBrace);
				setState(592);
				initializerList();
				setState(594);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==Comma) {
					{
					setState(593);
					match(Comma);
					}
				}

				setState(596);
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
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(601);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,63,_ctx) ) {
			case 1:
				{
				setState(600);
				designation();
				}
				break;
			}
			setState(603);
			initializer();
			setState(611);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(604);
					match(Comma);
					setState(606);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,64,_ctx) ) {
					case 1:
						{
						setState(605);
						designation();
						}
						break;
					}
					setState(608);
					initializer();
					}
					} 
				}
				setState(613);
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
			setState(614);
			designatorList();
			setState(615);
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
			setState(618); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(617);
				designator();
				}
				}
				setState(620); 
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
		enterRule(_localctx, 104, RULE_designator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(622);
			match(LeftBracket);
			setState(623);
			constantExpression();
			setState(624);
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
		enterRule(_localctx, 106, RULE_statement);
		try {
			setState(632);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,67,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(626);
				labeledStatement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(627);
				compoundStatement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(628);
				expressionStatement();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(629);
				selectionStatement();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(630);
				iterationStatement();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(631);
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
			setState(645);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				enterOuterAlt(_localctx, 1);
				{
				setState(634);
				match(Identifier);
				setState(635);
				match(Colon);
				setState(636);
				statement();
				}
				break;
			case Case:
				enterOuterAlt(_localctx, 2);
				{
				setState(637);
				match(Case);
				{
				setState(638);
				constantExpression();
				}
				setState(639);
				match(Colon);
				setState(640);
				statement();
				}
				break;
			case Default:
				enterOuterAlt(_localctx, 3);
				{
				setState(642);
				match(Default);
				setState(643);
				match(Colon);
				setState(644);
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
			setState(647);
			match(LeftBrace);
			setState(649);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0)) {
				{
				setState(648);
				blockItemList();
				}
			}

			setState(651);
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
			setState(654); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(653);
				blockItem();
				}
				}
				setState(656); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << On) | (1L << Variables) | (1L << Break) | (1L << Case) | (1L << Char) | (1L << Byte) | (1L << Continue) | (1L << Default) | (1L << Do) | (1L << Double) | (1L << Float) | (1L << For) | (1L << If) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Return) | (1L << Switch) | (1L << Void) | (1L << While) | (1L << LeftParen) | (1L << LeftBracket) | (1L << LeftBrace) | (1L << Less) | (1L << LessEqual) | (1L << Greater) | (1L << GreaterEqual) | (1L << LeftShift) | (1L << RightShift) | (1L << Plus) | (1L << PlusPlus) | (1L << Minus) | (1L << MinusMinus) | (1L << Div) | (1L << Mod) | (1L << And) | (1L << Or) | (1L << AndAnd) | (1L << OrOr) | (1L << Caret) | (1L << Not) | (1L << Tilde) | (1L << Question) | (1L << Semi) | (1L << Comma))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (Assign - 64)) | (1L << (StarAssign - 64)) | (1L << (DivAssign - 64)) | (1L << (ModAssign - 64)) | (1L << (PlusAssign - 64)) | (1L << (MinusAssign - 64)) | (1L << (LeftShiftAssign - 64)) | (1L << (RightShiftAssign - 64)) | (1L << (AndAssign - 64)) | (1L << (XorAssign - 64)) | (1L << (OrAssign - 64)) | (1L << (Star - 64)) | (1L << (Equal - 64)) | (1L << (NotEqual - 64)) | (1L << (Identifier - 64)) | (1L << (Constant - 64)) | (1L << (DigitSequence - 64)) | (1L << (StringLiteral - 64)))) != 0) );
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
			setState(660);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,71,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(658);
				statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(659);
				declaration();
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
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(663);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,72,_ctx) ) {
			case 1:
				{
				setState(662);
				expression();
				}
				break;
			}
			setState(665);
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
			setState(682);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case If:
				enterOuterAlt(_localctx, 1);
				{
				setState(667);
				match(If);
				setState(668);
				match(LeftParen);
				setState(669);
				expression();
				setState(670);
				match(RightParen);
				setState(671);
				statement();
				setState(674);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,73,_ctx) ) {
				case 1:
					{
					setState(672);
					match(Else);
					setState(673);
					statement();
					}
					break;
				}
				}
				break;
			case Switch:
				enterOuterAlt(_localctx, 2);
				{
				setState(676);
				match(Switch);
				setState(677);
				match(LeftParen);
				setState(678);
				expression();
				setState(679);
				match(RightParen);
				setState(680);
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
			setState(704);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case While:
				enterOuterAlt(_localctx, 1);
				{
				setState(684);
				match(While);
				setState(685);
				match(LeftParen);
				setState(686);
				expression();
				setState(687);
				match(RightParen);
				setState(688);
				statement();
				}
				break;
			case Do:
				enterOuterAlt(_localctx, 2);
				{
				setState(690);
				match(Do);
				setState(691);
				statement();
				setState(692);
				match(While);
				setState(693);
				match(LeftParen);
				setState(694);
				expression();
				setState(695);
				match(RightParen);
				setState(696);
				match(Semi);
				}
				break;
			case For:
				enterOuterAlt(_localctx, 3);
				{
				setState(698);
				match(For);
				setState(699);
				match(LeftParen);
				setState(700);
				forCondition();
				setState(701);
				match(RightParen);
				setState(702);
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
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(710);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,77,_ctx) ) {
			case 1:
				{
				setState(706);
				forDeclaration();
				}
				break;
			case 2:
				{
				setState(708);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,76,_ctx) ) {
				case 1:
					{
					setState(707);
					expression();
					}
					break;
				}
				}
				break;
			}
			setState(712);
			match(Semi);
			setState(714);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,78,_ctx) ) {
			case 1:
				{
				setState(713);
				forExpression();
				}
				break;
			}
			setState(716);
			match(Semi);
			setState(718);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,79,_ctx) ) {
			case 1:
				{
				setState(717);
				forExpression();
				}
				break;
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
			setState(720);
			declarationSpecifiers();
			setState(722);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LeftParen || _la==Identifier) {
				{
				setState(721);
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
			setState(724);
			assignmentExpression();
			setState(729);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(725);
				match(Comma);
				setState(726);
				assignmentExpression();
				}
				}
				setState(731);
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
			setState(737);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Break:
			case Continue:
				{
				setState(732);
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
				setState(733);
				match(Return);
				setState(735);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,82,_ctx) ) {
				case 1:
					{
					setState(734);
					expression();
					}
					break;
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(739);
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
			setState(742);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void) | (1L << LeftParen) | (1L << Semi))) != 0) || _la==Identifier) {
				{
				setState(741);
				translationUnit();
				}
			}

			setState(744);
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
			setState(747); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(746);
				externalDeclaration();
				}
				}
				setState(749); 
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
		enterRule(_localctx, 134, RULE_externalDeclaration);
		try {
			setState(754);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,86,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(751);
				functionDefinition();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(752);
				declaration();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(753);
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
			setState(757);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(756);
				declarationSpecifiers();
				}
			}

			setState(759);
			declarator();
			setState(761);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Char) | (1L << Byte) | (1L << Double) | (1L << Float) | (1L << Int) | (1L << Word) | (1L << Dword) | (1L << Message) | (1L << Timer) | (1L << MsTimer) | (1L << Long) | (1L << Int64) | (1L << Void))) != 0)) {
				{
				setState(760);
				declarationList();
				}
			}

			setState(763);
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
			setState(766); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(765);
				declaration();
				}
				}
				setState(768); 
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

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 39:
			return directDeclarator_sempred((DirectDeclaratorContext)_localctx, predIndex);
		case 47:
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\\\u0305\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\3\2\3\2\3"+
		"\2\6\2\u0092\n\2\r\2\16\2\u0093\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3"+
		"\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\7\2\u00a7\n\2\f\2\16\2\u00aa\13\2\5\2\u00ac"+
		"\n\2\3\3\3\3\3\3\3\3\5\3\u00b2\n\3\3\3\3\3\3\4\3\4\3\4\5\4\u00b9\n\4\3"+
		"\4\3\4\3\5\3\5\3\5\3\5\3\5\5\5\u00c2\n\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\5"+
		"\6\u00cb\n\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\5\7\u00d5\n\7\3\7\3\7\3\b"+
		"\3\b\3\b\3\b\3\b\5\b\u00de\n\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t"+
		"\3\n\3\n\3\n\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\13\3\13\5\13\u00f8"+
		"\n\13\3\13\3\13\5\13\u00fc\n\13\3\13\3\13\3\13\3\13\3\13\3\13\5\13\u0104"+
		"\n\13\3\13\3\13\7\13\u0108\n\13\f\13\16\13\u010b\13\13\3\f\3\f\3\f\7\f"+
		"\u0110\n\f\f\f\16\f\u0113\13\f\3\r\7\r\u0116\n\r\f\r\16\r\u0119\13\r\3"+
		"\r\3\r\3\r\3\r\5\r\u011f\n\r\3\16\3\16\3\17\3\17\3\17\3\17\3\17\3\17\3"+
		"\17\5\17\u012a\n\17\3\20\3\20\3\20\7\20\u012f\n\20\f\20\16\20\u0132\13"+
		"\20\3\21\3\21\3\21\7\21\u0137\n\21\f\21\16\21\u013a\13\21\3\22\3\22\3"+
		"\22\7\22\u013f\n\22\f\22\16\22\u0142\13\22\3\23\3\23\3\23\7\23\u0147\n"+
		"\23\f\23\16\23\u014a\13\23\3\24\3\24\3\24\7\24\u014f\n\24\f\24\16\24\u0152"+
		"\13\24\3\25\3\25\3\25\7\25\u0157\n\25\f\25\16\25\u015a\13\25\3\26\3\26"+
		"\3\26\7\26\u015f\n\26\f\26\16\26\u0162\13\26\3\27\3\27\3\27\7\27\u0167"+
		"\n\27\f\27\16\27\u016a\13\27\3\30\3\30\3\30\7\30\u016f\n\30\f\30\16\30"+
		"\u0172\13\30\3\31\3\31\3\31\7\31\u0177\n\31\f\31\16\31\u017a\13\31\3\32"+
		"\3\32\3\32\3\32\3\32\3\32\5\32\u0182\n\32\3\33\3\33\3\33\3\33\3\33\3\33"+
		"\5\33\u018a\n\33\3\34\3\34\3\35\3\35\3\35\7\35\u0191\n\35\f\35\16\35\u0194"+
		"\13\35\3\36\3\36\3\37\3\37\5\37\u019a\n\37\3\37\3\37\3 \6 \u019f\n \r"+
		" \16 \u01a0\3!\6!\u01a4\n!\r!\16!\u01a5\3\"\3\"\3#\3#\3#\7#\u01ad\n#\f"+
		"#\16#\u01b0\13#\3$\3$\3$\5$\u01b5\n$\3%\3%\3%\3%\3%\3%\3%\3%\3%\3%\3%"+
		"\3%\3%\5%\u01c4\n%\3&\3&\3&\3&\5&\u01ca\n&\3&\3&\3&\3&\3&\3&\3&\3&\5&"+
		"\u01d4\n&\3\'\3\'\5\'\u01d8\n\'\3(\3(\3)\3)\3)\3)\3)\3)\5)\u01e2\n)\3"+
		")\3)\3)\5)\u01e7\n)\3)\3)\3)\3)\3)\3)\3)\3)\3)\5)\u01f2\n)\3)\7)\u01f5"+
		"\n)\f)\16)\u01f8\13)\3*\3*\3*\3*\3*\7*\u01ff\n*\f*\16*\u0202\13*\3+\3"+
		"+\3+\5+\u0207\n+\3,\3,\3,\7,\u020c\n,\f,\16,\u020f\13,\3-\3-\3-\3-\3-"+
		"\5-\u0216\n-\5-\u0218\n-\3.\3.\3.\7.\u021d\n.\f.\16.\u0220\13.\3/\3/\5"+
		"/\u0224\n/\3\60\3\60\3\61\3\61\3\61\3\61\3\61\3\61\3\61\5\61\u022f\n\61"+
		"\3\61\3\61\3\61\3\61\3\61\3\61\5\61\u0237\n\61\3\61\5\61\u023a\n\61\3"+
		"\61\3\61\3\61\5\61\u023f\n\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61"+
		"\5\61\u0249\n\61\3\61\7\61\u024c\n\61\f\61\16\61\u024f\13\61\3\62\3\62"+
		"\3\62\3\62\5\62\u0255\n\62\3\62\3\62\5\62\u0259\n\62\3\63\5\63\u025c\n"+
		"\63\3\63\3\63\3\63\5\63\u0261\n\63\3\63\7\63\u0264\n\63\f\63\16\63\u0267"+
		"\13\63\3\64\3\64\3\64\3\65\6\65\u026d\n\65\r\65\16\65\u026e\3\66\3\66"+
		"\3\66\3\66\3\67\3\67\3\67\3\67\3\67\3\67\5\67\u027b\n\67\38\38\38\38\3"+
		"8\38\38\38\38\38\38\58\u0288\n8\39\39\59\u028c\n9\39\39\3:\6:\u0291\n"+
		":\r:\16:\u0292\3;\3;\5;\u0297\n;\3<\5<\u029a\n<\3<\3<\3=\3=\3=\3=\3=\3"+
		"=\3=\5=\u02a5\n=\3=\3=\3=\3=\3=\3=\5=\u02ad\n=\3>\3>\3>\3>\3>\3>\3>\3"+
		">\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\3>\5>\u02c3\n>\3?\3?\5?\u02c7\n?\5"+
		"?\u02c9\n?\3?\3?\5?\u02cd\n?\3?\3?\5?\u02d1\n?\3@\3@\5@\u02d5\n@\3A\3"+
		"A\3A\7A\u02da\nA\fA\16A\u02dd\13A\3B\3B\3B\5B\u02e2\nB\5B\u02e4\nB\3B"+
		"\3B\3C\5C\u02e9\nC\3C\3C\3D\6D\u02ee\nD\rD\16D\u02ef\3E\3E\3E\5E\u02f5"+
		"\nE\3F\5F\u02f8\nF\3F\3F\5F\u02fc\nF\3F\3F\3G\6G\u0301\nG\rG\16G\u0302"+
		"\3G\2\4P`H\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\64\66"+
		"8:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|~\u0080\u0082\u0084\u0086\u0088\u008a"+
		"\u008c\2\r\4\2MMSS\4\2\62\62\64\64\5\2\61\61\63\63<=\4\2\65\66MM\4\2\61"+
		"\61\63\63\3\2/\60\3\2+.\3\2NO\3\2BL\3\2%&\4\2\f\f\20\20\2\u033f\2\u00ab"+
		"\3\2\2\2\4\u00ad\3\2\2\2\6\u00b5\3\2\2\2\b\u00bc\3\2\2\2\n\u00c5\3\2\2"+
		"\2\f\u00d0\3\2\2\2\16\u00d8\3\2\2\2\20\u00e3\3\2\2\2\22\u00e9\3\2\2\2"+
		"\24\u00fb\3\2\2\2\26\u010c\3\2\2\2\30\u0117\3\2\2\2\32\u0120\3\2\2\2\34"+
		"\u0129\3\2\2\2\36\u012b\3\2\2\2 \u0133\3\2\2\2\"\u013b\3\2\2\2$\u0143"+
		"\3\2\2\2&\u014b\3\2\2\2(\u0153\3\2\2\2*\u015b\3\2\2\2,\u0163\3\2\2\2."+
		"\u016b\3\2\2\2\60\u0173\3\2\2\2\62\u017b\3\2\2\2\64\u0189\3\2\2\2\66\u018b"+
		"\3\2\2\28\u018d\3\2\2\2:\u0195\3\2\2\2<\u0197\3\2\2\2>\u019e\3\2\2\2@"+
		"\u01a3\3\2\2\2B\u01a7\3\2\2\2D\u01a9\3\2\2\2F\u01b1\3\2\2\2H\u01c3\3\2"+
		"\2\2J\u01d3\3\2\2\2L\u01d5\3\2\2\2N\u01d9\3\2\2\2P\u01e1\3\2\2\2R\u0200"+
		"\3\2\2\2T\u0203\3\2\2\2V\u0208\3\2\2\2X\u0217\3\2\2\2Z\u0219\3\2\2\2\\"+
		"\u0221\3\2\2\2^\u0225\3\2\2\2`\u0239\3\2\2\2b\u0258\3\2\2\2d\u025b\3\2"+
		"\2\2f\u0268\3\2\2\2h\u026c\3\2\2\2j\u0270\3\2\2\2l\u027a\3\2\2\2n\u0287"+
		"\3\2\2\2p\u0289\3\2\2\2r\u0290\3\2\2\2t\u0296\3\2\2\2v\u0299\3\2\2\2x"+
		"\u02ac\3\2\2\2z\u02c2\3\2\2\2|\u02c8\3\2\2\2~\u02d2\3\2\2\2\u0080\u02d6"+
		"\3\2\2\2\u0082\u02e3\3\2\2\2\u0084\u02e8\3\2\2\2\u0086\u02ed\3\2\2\2\u0088"+
		"\u02f4\3\2\2\2\u008a\u02f7\3\2\2\2\u008c\u0300\3\2\2\2\u008e\u00ac\7S"+
		"\2\2\u008f\u00ac\7V\2\2\u0090\u0092\7X\2\2\u0091\u0090\3\2\2\2\u0092\u0093"+
		"\3\2\2\2\u0093\u0091\3\2\2\2\u0093\u0094\3\2\2\2\u0094\u00ac\3\2\2\2\u0095"+
		"\u0096\7%\2\2\u0096\u0097\58\35\2\u0097\u0098\7&\2\2\u0098\u00ac\3\2\2"+
		"\2\u0099\u009a\7%\2\2\u009a\u009b\5p9\2\u009b\u009c\7&\2\2\u009c\u00ac"+
		"\3\2\2\2\u009d\u00a7\5\6\4\2\u009e\u00a7\5\b\5\2\u009f\u00a7\5\n\6\2\u00a0"+
		"\u00a7\5\f\7\2\u00a1\u00a7\5\22\n\2\u00a2\u00a7\5\u008aF\2\u00a3\u00a7"+
		"\5\4\3\2\u00a4\u00a7\5\16\b\2\u00a5\u00a7\5\20\t\2\u00a6\u009d\3\2\2\2"+
		"\u00a6\u009e\3\2\2\2\u00a6\u009f\3\2\2\2\u00a6\u00a0\3\2\2\2\u00a6\u00a1"+
		"\3\2\2\2\u00a6\u00a2\3\2\2\2\u00a6\u00a3\3\2\2\2\u00a6\u00a4\3\2\2\2\u00a6"+
		"\u00a5\3\2\2\2\u00a7\u00aa\3\2\2\2\u00a8\u00a6\3\2\2\2\u00a8\u00a9\3\2"+
		"\2\2\u00a9\u00ac\3\2\2\2\u00aa\u00a8\3\2\2\2\u00ab\u008e\3\2\2\2\u00ab"+
		"\u008f\3\2\2\2\u00ab\u0091\3\2\2\2\u00ab\u0095\3\2\2\2\u00ab\u0099\3\2"+
		"\2\2\u00ab\u00a8\3\2\2\2\u00ac\3\3\2\2\2\u00ad\u00ae\7\n\2\2\u00ae\u00af"+
		"\7\7\2\2\u00af\u00b1\7)\2\2\u00b0\u00b2\5r:\2\u00b1\u00b0\3\2\2\2\u00b1"+
		"\u00b2\3\2\2\2\u00b2\u00b3\3\2\2\2\u00b3\u00b4\7*\2\2\u00b4\5\3\2\2\2"+
		"\u00b5\u00b6\7\13\2\2\u00b6\u00b8\7)\2\2\u00b7\u00b9\5r:\2\u00b8\u00b7"+
		"\3\2\2\2\u00b8\u00b9\3\2\2\2\u00b9\u00ba\3\2\2\2\u00ba\u00bb\7*\2\2\u00bb"+
		"\7\3\2\2\2\u00bc\u00bd\7\n\2\2\u00bd\u00be\7\t\2\2\u00be\u00bf\7Q\2\2"+
		"\u00bf\u00c1\7)\2\2\u00c0\u00c2\5r:\2\u00c1\u00c0\3\2\2\2\u00c1\u00c2"+
		"\3\2\2\2\u00c2\u00c3\3\2\2\2\u00c3\u00c4\7*\2\2\u00c4\t\3\2\2\2\u00c5"+
		"\u00c6\7\n\2\2\u00c6\u00c7\7\35\2\2\u00c7\u00ca\7S\2\2\u00c8\u00c9\7U"+
		"\2\2\u00c9\u00cb\t\2\2\2\u00ca\u00c8\3\2\2\2\u00ca\u00cb\3\2\2\2\u00cb"+
		"\u00cc\3\2\2\2\u00cc\u00cd\7)\2\2\u00cd\u00ce\5r:\2\u00ce\u00cf\7*\2\2"+
		"\u00cf\13\3\2\2\2\u00d0\u00d1\7\n\2\2\u00d1\u00d2\7\b\2\2\u00d2\u00d4"+
		"\7)\2\2\u00d3\u00d5\5r:\2\u00d4\u00d3\3\2\2\2\u00d4\u00d5\3\2\2\2\u00d5"+
		"\u00d6\3\2\2\2\u00d6\u00d7\7*\2\2\u00d7\r\3\2\2\2\u00d8\u00d9\7\n\2\2"+
		"\u00d9\u00da\7\33\2\2\u00da\u00dd\7S\2\2\u00db\u00dc\7U\2\2\u00dc\u00de"+
		"\t\2\2\2\u00dd\u00db\3\2\2\2\u00dd\u00de\3\2\2\2\u00de\u00df\3\2\2\2\u00df"+
		"\u00e0\7)\2\2\u00e0\u00e1\5r:\2\u00e1\u00e2\7*\2\2\u00e2\17\3\2\2\2\u00e3"+
		"\u00e4\7\n\2\2\u00e4\u00e5\7\6\2\2\u00e5\u00e6\7)\2\2\u00e6\u00e7\5r:"+
		"\2\u00e7\u00e8\7*\2\2\u00e8\21\3\2\2\2\u00e9\u00ea\7\n\2\2\u00ea\u00eb"+
		"\7\34\2\2\u00eb\u00ec\7S\2\2\u00ec\u00ed\7)\2\2\u00ed\u00ee\5r:\2\u00ee"+
		"\u00ef\7*\2\2\u00ef\23\3\2\2\2\u00f0\u00fc\5\2\2\2\u00f1\u00f2\7%\2\2"+
		"\u00f2\u00f3\5\\/\2\u00f3\u00f4\7&\2\2\u00f4\u00f5\7)\2\2\u00f5\u00f7"+
		"\5d\63\2\u00f6\u00f8\7A\2\2\u00f7\u00f6\3\2\2\2\u00f7\u00f8\3\2\2\2\u00f8"+
		"\u00f9\3\2\2\2\u00f9\u00fa\7*\2\2\u00fa\u00fc\3\2\2\2\u00fb\u00f0\3\2"+
		"\2\2\u00fb\u00f1\3\2\2\2\u00fc\u0109\3\2\2\2\u00fd\u00fe\7\'\2\2\u00fe"+
		"\u00ff\58\35\2\u00ff\u0100\7(\2\2\u0100\u0108\3\2\2\2\u0101\u0103\7%\2"+
		"\2\u0102\u0104\5\26\f\2\u0103\u0102\3\2\2\2\u0103\u0104\3\2\2\2\u0104"+
		"\u0105\3\2\2\2\u0105\u0108\7&\2\2\u0106\u0108\t\3\2\2\u0107\u00fd\3\2"+
		"\2\2\u0107\u0101\3\2\2\2\u0107\u0106\3\2\2\2\u0108\u010b\3\2\2\2\u0109"+
		"\u0107\3\2\2\2\u0109\u010a\3\2\2\2\u010a\25\3\2\2\2\u010b\u0109\3\2\2"+
		"\2\u010c\u0111\5\64\33\2\u010d\u010e\7A\2\2\u010e\u0110\5\64\33\2\u010f"+
		"\u010d\3\2\2\2\u0110\u0113\3\2\2\2\u0111\u010f\3\2\2\2\u0111\u0112\3\2"+
		"\2\2\u0112\27\3\2\2\2\u0113\u0111\3\2\2\2\u0114\u0116\t\3\2\2\u0115\u0114"+
		"\3\2\2\2\u0116\u0119\3\2\2\2\u0117\u0115\3\2\2\2\u0117\u0118\3\2\2\2\u0118"+
		"\u011e\3\2\2\2\u0119\u0117\3\2\2\2\u011a\u011f\5\24\13\2\u011b\u011c\5"+
		"\32\16\2\u011c\u011d\5\34\17\2\u011d\u011f\3\2\2\2\u011e\u011a\3\2\2\2"+
		"\u011e\u011b\3\2\2\2\u011f\31\3\2\2\2\u0120\u0121\t\4\2\2\u0121\33\3\2"+
		"\2\2\u0122\u0123\7%\2\2\u0123\u0124\5\\/\2\u0124\u0125\7&\2\2\u0125\u0126"+
		"\5\34\17\2\u0126\u012a\3\2\2\2\u0127\u012a\5\30\r\2\u0128\u012a\7W\2\2"+
		"\u0129\u0122\3\2\2\2\u0129\u0127\3\2\2\2\u0129\u0128\3\2\2\2\u012a\35"+
		"\3\2\2\2\u012b\u0130\5\34\17\2\u012c\u012d\t\5\2\2\u012d\u012f\5\34\17"+
		"\2\u012e\u012c\3\2\2\2\u012f\u0132\3\2\2\2\u0130\u012e\3\2\2\2\u0130\u0131"+
		"\3\2\2\2\u0131\37\3\2\2\2\u0132\u0130\3\2\2\2\u0133\u0138\5\36\20\2\u0134"+
		"\u0135\t\6\2\2\u0135\u0137\5\36\20\2\u0136\u0134\3\2\2\2\u0137\u013a\3"+
		"\2\2\2\u0138\u0136\3\2\2\2\u0138\u0139\3\2\2\2\u0139!\3\2\2\2\u013a\u0138"+
		"\3\2\2\2\u013b\u0140\5 \21\2\u013c\u013d\t\7\2\2\u013d\u013f\5 \21\2\u013e"+
		"\u013c\3\2\2\2\u013f\u0142\3\2\2\2\u0140\u013e\3\2\2\2\u0140\u0141\3\2"+
		"\2\2\u0141#\3\2\2\2\u0142\u0140\3\2\2\2\u0143\u0148\5\"\22\2\u0144\u0145"+
		"\t\b\2\2\u0145\u0147\5\"\22\2\u0146\u0144\3\2\2\2\u0147\u014a\3\2\2\2"+
		"\u0148\u0146\3\2\2\2\u0148\u0149\3\2\2\2\u0149%\3\2\2\2\u014a\u0148\3"+
		"\2\2\2\u014b\u0150\5$\23\2\u014c\u014d\t\t\2\2\u014d\u014f\5$\23\2\u014e"+
		"\u014c\3\2\2\2\u014f\u0152\3\2\2\2\u0150\u014e\3\2\2\2\u0150\u0151\3\2"+
		"\2\2\u0151\'\3\2\2\2\u0152\u0150\3\2\2\2\u0153\u0158\5&\24\2\u0154\u0155"+
		"\7\67\2\2\u0155\u0157\5&\24\2\u0156\u0154\3\2\2\2\u0157\u015a\3\2\2\2"+
		"\u0158\u0156\3\2\2\2\u0158\u0159\3\2\2\2\u0159)\3\2\2\2\u015a\u0158\3"+
		"\2\2\2\u015b\u0160\5(\25\2\u015c\u015d\7;\2\2\u015d\u015f\5(\25\2\u015e"+
		"\u015c\3\2\2\2\u015f\u0162\3\2\2\2\u0160\u015e\3\2\2\2\u0160\u0161\3\2"+
		"\2\2\u0161+\3\2\2\2\u0162\u0160\3\2\2\2\u0163\u0168\5*\26\2\u0164\u0165"+
		"\78\2\2\u0165\u0167\5*\26\2\u0166\u0164\3\2\2\2\u0167\u016a\3\2\2\2\u0168"+
		"\u0166\3\2\2\2\u0168\u0169\3\2\2\2\u0169-\3\2\2\2\u016a\u0168\3\2\2\2"+
		"\u016b\u0170\5,\27\2\u016c\u016d\79\2\2\u016d\u016f\5,\27\2\u016e\u016c"+
		"\3\2\2\2\u016f\u0172\3\2\2\2\u0170\u016e\3\2\2\2\u0170\u0171\3\2\2\2\u0171"+
		"/\3\2\2\2\u0172\u0170\3\2\2\2\u0173\u0178\5.\30\2\u0174\u0175\7:\2\2\u0175"+
		"\u0177\5.\30\2\u0176\u0174\3\2\2\2\u0177\u017a\3\2\2\2\u0178\u0176\3\2"+
		"\2\2\u0178\u0179\3\2\2\2\u0179\61\3\2\2\2\u017a\u0178\3\2\2\2\u017b\u0181"+
		"\5\60\31\2\u017c\u017d\7>\2\2\u017d\u017e\58\35\2\u017e\u017f\7?\2\2\u017f"+
		"\u0180\5\62\32\2\u0180\u0182\3\2\2\2\u0181\u017c\3\2\2\2\u0181\u0182\3"+
		"\2\2\2\u0182\63\3\2\2\2\u0183\u018a\5\62\32\2\u0184\u0185\5\30\r\2\u0185"+
		"\u0186\5\66\34\2\u0186\u0187\5\64\33\2\u0187\u018a\3\2\2\2\u0188\u018a"+
		"\7W\2\2\u0189\u0183\3\2\2\2\u0189\u0184\3\2\2\2\u0189\u0188\3\2\2\2\u018a"+
		"\65\3\2\2\2\u018b\u018c\t\n\2\2\u018c\67\3\2\2\2\u018d\u0192\5\64\33\2"+
		"\u018e\u018f\7A\2\2\u018f\u0191\5\64\33\2\u0190\u018e\3\2\2\2\u0191\u0194"+
		"\3\2\2\2\u0192\u0190\3\2\2\2\u0192\u0193\3\2\2\2\u01939\3\2\2\2\u0194"+
		"\u0192\3\2\2\2\u0195\u0196\5\62\32\2\u0196;\3\2\2\2\u0197\u0199\5> \2"+
		"\u0198\u019a\5D#\2\u0199\u0198\3\2\2\2\u0199\u019a\3\2\2\2\u019a\u019b"+
		"\3\2\2\2\u019b\u019c\7@\2\2\u019c=\3\2\2\2\u019d\u019f\5B\"\2\u019e\u019d"+
		"\3\2\2\2\u019f\u01a0\3\2\2\2\u01a0\u019e\3\2\2\2\u01a0\u01a1\3\2\2\2\u01a1"+
		"?\3\2\2\2\u01a2\u01a4\5B\"\2\u01a3\u01a2\3\2\2\2\u01a4\u01a5\3\2\2\2\u01a5"+
		"\u01a3\3\2\2\2\u01a5\u01a6\3\2\2\2\u01a6A\3\2\2\2\u01a7\u01a8\5H%\2\u01a8"+
		"C\3\2\2\2\u01a9\u01ae\5F$\2\u01aa\u01ab\7A\2\2\u01ab\u01ad\5F$\2\u01ac"+
		"\u01aa\3\2\2\2\u01ad\u01b0\3\2\2\2\u01ae\u01ac\3\2\2\2\u01ae\u01af\3\2"+
		"\2\2\u01afE\3\2\2\2\u01b0\u01ae\3\2\2\2\u01b1\u01b4\5N(\2\u01b2\u01b3"+
		"\7B\2\2\u01b3\u01b5\5b\62\2\u01b4\u01b2\3\2\2\2\u01b4\u01b5\3\2\2\2\u01b5"+
		"G\3\2\2\2\u01b6\u01c4\7#\2\2\u01b7\u01c4\7\16\2\2\u01b8\u01c4\7\17\2\2"+
		"\u01b9\u01c4\7\30\2\2\u01ba\u01c4\7\37\2\2\u01bb\u01c4\7 \2\2\u01bc\u01c4"+
		"\7\25\2\2\u01bd\u01c4\7\23\2\2\u01be\u01c4\7\31\2\2\u01bf\u01c4\7\32\2"+
		"\2\u01c0\u01c4\7\35\2\2\u01c1\u01c4\7\36\2\2\u01c2\u01c4\5J&\2\u01c3\u01b6"+
		"\3\2\2\2\u01c3\u01b7\3\2\2\2\u01c3\u01b8\3\2\2\2\u01c3\u01b9\3\2\2\2\u01c3"+
		"\u01ba\3\2\2\2\u01c3\u01bb\3\2\2\2\u01c3\u01bc\3\2\2\2\u01c3\u01bd\3\2"+
		"\2\2\u01c3\u01be\3\2\2\2\u01c3\u01bf\3\2\2\2\u01c3\u01c0\3\2\2\2\u01c3"+
		"\u01c1\3\2\2\2\u01c3\u01c2\3\2\2\2\u01c4I\3\2\2\2\u01c5\u01c6\7\33\2\2"+
		"\u01c6\u01c9\7S\2\2\u01c7\u01c8\7U\2\2\u01c8\u01ca\t\2\2\2\u01c9\u01c7"+
		"\3\2\2\2\u01c9\u01ca\3\2\2\2\u01ca\u01d4\3\2\2\2\u01cb\u01cc\7\33\2\2"+
		"\u01cc\u01d4\7M\2\2\u01cd\u01ce\7\33\2\2\u01ce\u01d4\7\3\2\2\u01cf\u01d0"+
		"\7\33\2\2\u01d0\u01d4\7\4\2\2\u01d1\u01d2\7\33\2\2\u01d2\u01d4\7\5\2\2"+
		"\u01d3\u01c5\3\2\2\2\u01d3\u01cb\3\2\2\2\u01d3\u01cd\3\2\2\2\u01d3\u01cf"+
		"\3\2\2\2\u01d3\u01d1\3\2\2\2\u01d4K\3\2\2\2\u01d5\u01d7\5H%\2\u01d6\u01d8"+
		"\5L\'\2\u01d7\u01d6\3\2\2\2\u01d7\u01d8\3\2\2\2\u01d8M\3\2\2\2\u01d9\u01da"+
		"\5P)\2\u01daO\3\2\2\2\u01db\u01dc\b)\1\2\u01dc\u01e2\7S\2\2\u01dd\u01de"+
		"\7%\2\2\u01de\u01df\5N(\2\u01df\u01e0\7&\2\2\u01e0\u01e2\3\2\2\2\u01e1"+
		"\u01db\3\2\2\2\u01e1\u01dd\3\2\2\2\u01e2\u01f6\3\2\2\2\u01e3\u01e4\f\5"+
		"\2\2\u01e4\u01e6\7\'\2\2\u01e5\u01e7\5\64\33\2\u01e6\u01e5\3\2\2\2\u01e6"+
		"\u01e7\3\2\2\2\u01e7\u01e8\3\2\2\2\u01e8\u01f5\7(\2\2\u01e9\u01ea\f\4"+
		"\2\2\u01ea\u01eb\7%\2\2\u01eb\u01ec\5T+\2\u01ec\u01ed\7&\2\2\u01ed\u01f5"+
		"\3\2\2\2\u01ee\u01ef\f\3\2\2\u01ef\u01f1\7%\2\2\u01f0\u01f2\5Z.\2\u01f1"+
		"\u01f0\3\2\2\2\u01f1\u01f2\3\2\2\2\u01f2\u01f3\3\2\2\2\u01f3\u01f5\7&"+
		"\2\2\u01f4\u01e3\3\2\2\2\u01f4\u01e9\3\2\2\2\u01f4\u01ee\3\2\2\2\u01f5"+
		"\u01f8\3\2\2\2\u01f6\u01f4\3\2\2\2\u01f6\u01f7\3\2\2\2\u01f7Q\3\2\2\2"+
		"\u01f8\u01f6\3\2\2\2\u01f9\u01ff\n\13\2\2\u01fa\u01fb\7%\2\2\u01fb\u01fc"+
		"\5R*\2\u01fc\u01fd\7&\2\2\u01fd\u01ff\3\2\2\2\u01fe\u01f9\3\2\2\2\u01fe"+
		"\u01fa\3\2\2\2\u01ff\u0202\3\2\2\2\u0200\u01fe\3\2\2\2\u0200\u0201\3\2"+
		"\2\2\u0201S\3\2\2\2\u0202\u0200\3\2\2\2\u0203\u0206\5V,\2\u0204\u0205"+
		"\7A\2\2\u0205\u0207\7P\2\2\u0206\u0204\3\2\2\2\u0206\u0207\3\2\2\2\u0207"+
		"U\3\2\2\2\u0208\u020d\5X-\2\u0209\u020a\7A\2\2\u020a\u020c\5X-\2\u020b"+
		"\u0209\3\2\2\2\u020c\u020f\3\2\2\2\u020d\u020b\3\2\2\2\u020d\u020e\3\2"+
		"\2\2\u020eW\3\2\2\2\u020f\u020d\3\2\2\2\u0210\u0211\5> \2\u0211\u0212"+
		"\5N(\2\u0212\u0218\3\2\2\2\u0213\u0215\5@!\2\u0214\u0216\5^\60\2\u0215"+
		"\u0214\3\2\2\2\u0215\u0216\3\2\2\2\u0216\u0218\3\2\2\2\u0217\u0210\3\2"+
		"\2\2\u0217\u0213\3\2\2\2\u0218Y\3\2\2\2\u0219\u021e\7S\2\2\u021a\u021b"+
		"\7A\2\2\u021b\u021d\7S\2\2\u021c\u021a\3\2\2\2\u021d\u0220\3\2\2\2\u021e"+
		"\u021c\3\2\2\2\u021e\u021f\3\2\2\2\u021f[\3\2\2\2\u0220\u021e\3\2\2\2"+
		"\u0221\u0223\5L\'\2\u0222\u0224\5^\60\2\u0223\u0222\3\2\2\2\u0223\u0224"+
		"\3\2\2\2\u0224]\3\2\2\2\u0225\u0226\5`\61\2\u0226_\3\2\2\2\u0227\u0228"+
		"\b\61\1\2\u0228\u0229\7%\2\2\u0229\u022a\5^\60\2\u022a\u022b\7&\2\2\u022b"+
		"\u023a\3\2\2\2\u022c\u022e\7\'\2\2\u022d\u022f\5\64\33\2\u022e\u022d\3"+
		"\2\2\2\u022e\u022f\3\2\2\2\u022f\u0230\3\2\2\2\u0230\u023a\7(\2\2\u0231"+
		"\u0232\7\'\2\2\u0232\u0233\7M\2\2\u0233\u023a\7(\2\2\u0234\u0236\7%\2"+
		"\2\u0235\u0237\5T+\2\u0236\u0235\3\2\2\2\u0236\u0237\3\2\2\2\u0237\u0238"+
		"\3\2\2\2\u0238\u023a\7&\2\2\u0239\u0227\3\2\2\2\u0239\u022c\3\2\2\2\u0239"+
		"\u0231\3\2\2\2\u0239\u0234\3\2\2\2\u023a\u024d\3\2\2\2\u023b\u023c\f\5"+
		"\2\2\u023c\u023e\7\'\2\2\u023d\u023f\5\64\33\2\u023e\u023d\3\2\2\2\u023e"+
		"\u023f\3\2\2\2\u023f\u0240\3\2\2\2\u0240\u024c\7(\2\2\u0241\u0242\f\4"+
		"\2\2\u0242\u0243\7\'\2\2\u0243\u0244\7M\2\2\u0244\u024c\7(\2\2\u0245\u0246"+
		"\f\3\2\2\u0246\u0248\7%\2\2\u0247\u0249\5T+\2\u0248\u0247\3\2\2\2\u0248"+
		"\u0249\3\2\2\2\u0249\u024a\3\2\2\2\u024a\u024c\7&\2\2\u024b\u023b\3\2"+
		"\2\2\u024b\u0241\3\2\2\2\u024b\u0245\3\2\2\2\u024c\u024f\3\2\2\2\u024d"+
		"\u024b\3\2\2\2\u024d\u024e\3\2\2\2\u024ea\3\2\2\2\u024f\u024d\3\2\2\2"+
		"\u0250\u0259\5\64\33\2\u0251\u0252\7)\2\2\u0252\u0254\5d\63\2\u0253\u0255"+
		"\7A\2\2\u0254\u0253\3\2\2\2\u0254\u0255\3\2\2\2\u0255\u0256\3\2\2\2\u0256"+
		"\u0257\7*\2\2\u0257\u0259\3\2\2\2\u0258\u0250\3\2\2\2\u0258\u0251\3\2"+
		"\2\2\u0259c\3\2\2\2\u025a\u025c\5f\64\2\u025b\u025a\3\2\2\2\u025b\u025c"+
		"\3\2\2\2\u025c\u025d\3\2\2\2\u025d\u0265\5b\62\2\u025e\u0260\7A\2\2\u025f"+
		"\u0261\5f\64\2\u0260\u025f\3\2\2\2\u0260\u0261\3\2\2\2\u0261\u0262\3\2"+
		"\2\2\u0262\u0264\5b\62\2\u0263\u025e\3\2\2\2\u0264\u0267\3\2\2\2\u0265"+
		"\u0263\3\2\2\2\u0265\u0266\3\2\2\2\u0266e\3\2\2\2\u0267\u0265\3\2\2\2"+
		"\u0268\u0269\5h\65\2\u0269\u026a\7B\2\2\u026ag\3\2\2\2\u026b\u026d\5j"+
		"\66\2\u026c\u026b\3\2\2\2\u026d\u026e\3\2\2\2\u026e\u026c\3\2\2\2\u026e"+
		"\u026f\3\2\2\2\u026fi\3\2\2\2\u0270\u0271\7\'\2\2\u0271\u0272\5:\36\2"+
		"\u0272\u0273\7(\2\2\u0273k\3\2\2\2\u0274\u027b\5n8\2\u0275\u027b\5p9\2"+
		"\u0276\u027b\5v<\2\u0277\u027b\5x=\2\u0278\u027b\5z>\2\u0279\u027b\5\u0082"+
		"B\2\u027a\u0274\3\2\2\2\u027a\u0275\3\2\2\2\u027a\u0276\3\2\2\2\u027a"+
		"\u0277\3\2\2\2\u027a\u0278\3\2\2\2\u027a\u0279\3\2\2\2\u027bm\3\2\2\2"+
		"\u027c\u027d\7S\2\2\u027d\u027e\7?\2\2\u027e\u0288\5l\67\2\u027f\u0280"+
		"\7\r\2\2\u0280\u0281\5:\36\2\u0281\u0282\7?\2\2\u0282\u0283\5l\67\2\u0283"+
		"\u0288\3\2\2\2\u0284\u0285\7\21\2\2\u0285\u0286\7?\2\2\u0286\u0288\5l"+
		"\67\2\u0287\u027c\3\2\2\2\u0287\u027f\3\2\2\2\u0287\u0284\3\2\2\2\u0288"+
		"o\3\2\2\2\u0289\u028b\7)\2\2\u028a\u028c\5r:\2\u028b\u028a\3\2\2\2\u028b"+
		"\u028c\3\2\2\2\u028c\u028d\3\2\2\2\u028d\u028e\7*\2\2\u028eq\3\2\2\2\u028f"+
		"\u0291\5t;\2\u0290\u028f\3\2\2\2\u0291\u0292\3\2\2\2\u0292\u0290\3\2\2"+
		"\2\u0292\u0293\3\2\2\2\u0293s\3\2\2\2\u0294\u0297\5l\67\2\u0295\u0297"+
		"\5<\37\2\u0296\u0294\3\2\2\2\u0296\u0295\3\2\2\2\u0297u\3\2\2\2\u0298"+
		"\u029a\58\35\2\u0299\u0298\3\2\2\2\u0299\u029a\3\2\2\2\u029a\u029b\3\2"+
		"\2\2\u029b\u029c\7@\2\2\u029cw\3\2\2\2\u029d\u029e\7\27\2\2\u029e\u029f"+
		"\7%\2\2\u029f\u02a0\58\35\2\u02a0\u02a1\7&\2\2\u02a1\u02a4\5l\67\2\u02a2"+
		"\u02a3\7\24\2\2\u02a3\u02a5\5l\67\2\u02a4\u02a2\3\2\2\2\u02a4\u02a5\3"+
		"\2\2\2\u02a5\u02ad\3\2\2\2\u02a6\u02a7\7\"\2\2\u02a7\u02a8\7%\2\2\u02a8"+
		"\u02a9\58\35\2\u02a9\u02aa\7&\2\2\u02aa\u02ab\5l\67\2\u02ab\u02ad\3\2"+
		"\2\2\u02ac\u029d\3\2\2\2\u02ac\u02a6\3\2\2\2\u02ady\3\2\2\2\u02ae\u02af"+
		"\7$\2\2\u02af\u02b0\7%\2\2\u02b0\u02b1\58\35\2\u02b1\u02b2\7&\2\2\u02b2"+
		"\u02b3\5l\67\2\u02b3\u02c3\3\2\2\2\u02b4\u02b5\7\22\2\2\u02b5\u02b6\5"+
		"l\67\2\u02b6\u02b7\7$\2\2\u02b7\u02b8\7%\2\2\u02b8\u02b9\58\35\2\u02b9"+
		"\u02ba\7&\2\2\u02ba\u02bb\7@\2\2\u02bb\u02c3\3\2\2\2\u02bc\u02bd\7\26"+
		"\2\2\u02bd\u02be\7%\2\2\u02be\u02bf\5|?\2\u02bf\u02c0\7&\2\2\u02c0\u02c1"+
		"\5l\67\2\u02c1\u02c3\3\2\2\2\u02c2\u02ae\3\2\2\2\u02c2\u02b4\3\2\2\2\u02c2"+
		"\u02bc\3\2\2\2\u02c3{\3\2\2\2\u02c4\u02c9\5~@\2\u02c5\u02c7\58\35\2\u02c6"+
		"\u02c5\3\2\2\2\u02c6\u02c7\3\2\2\2\u02c7\u02c9\3\2\2\2\u02c8\u02c4\3\2"+
		"\2\2\u02c8\u02c6\3\2\2\2\u02c9\u02ca\3\2\2\2\u02ca\u02cc\7@\2\2\u02cb"+
		"\u02cd\5\u0080A\2\u02cc\u02cb\3\2\2\2\u02cc\u02cd\3\2\2\2\u02cd\u02ce"+
		"\3\2\2\2\u02ce\u02d0\7@\2\2\u02cf\u02d1\5\u0080A\2\u02d0\u02cf\3\2\2\2"+
		"\u02d0\u02d1\3\2\2\2\u02d1}\3\2\2\2\u02d2\u02d4\5> \2\u02d3\u02d5\5D#"+
		"\2\u02d4\u02d3\3\2\2\2\u02d4\u02d5\3\2\2\2\u02d5\177\3\2\2\2\u02d6\u02db"+
		"\5\64\33\2\u02d7\u02d8\7A\2\2\u02d8\u02da\5\64\33\2\u02d9\u02d7\3\2\2"+
		"\2\u02da\u02dd\3\2\2\2\u02db\u02d9\3\2\2\2\u02db\u02dc\3\2\2\2\u02dc\u0081"+
		"\3\2\2\2\u02dd\u02db\3\2\2\2\u02de\u02e4\t\f\2\2\u02df\u02e1\7!\2\2\u02e0"+
		"\u02e2\58\35\2\u02e1\u02e0\3\2\2\2\u02e1\u02e2\3\2\2\2\u02e2\u02e4\3\2"+
		"\2\2\u02e3\u02de\3\2\2\2\u02e3\u02df\3\2\2\2\u02e4\u02e5\3\2\2\2\u02e5"+
		"\u02e6\7@\2\2\u02e6\u0083\3\2\2\2\u02e7\u02e9\5\u0086D\2\u02e8\u02e7\3"+
		"\2\2\2\u02e8\u02e9\3\2\2\2\u02e9\u02ea\3\2\2\2\u02ea\u02eb\7\2\2\3\u02eb"+
		"\u0085\3\2\2\2\u02ec\u02ee\5\u0088E\2\u02ed\u02ec\3\2\2\2\u02ee\u02ef"+
		"\3\2\2\2\u02ef\u02ed\3\2\2\2\u02ef\u02f0\3\2\2\2\u02f0\u0087\3\2\2\2\u02f1"+
		"\u02f5\5\u008aF\2\u02f2\u02f5\5<\37\2\u02f3\u02f5\7@\2\2\u02f4\u02f1\3"+
		"\2\2\2\u02f4\u02f2\3\2\2\2\u02f4\u02f3\3\2\2\2\u02f5\u0089\3\2\2\2\u02f6"+
		"\u02f8\5> \2\u02f7\u02f6\3\2\2\2\u02f7\u02f8\3\2\2\2\u02f8\u02f9\3\2\2"+
		"\2\u02f9\u02fb\5N(\2\u02fa\u02fc\5\u008cG\2\u02fb\u02fa\3\2\2\2\u02fb"+
		"\u02fc\3\2\2\2\u02fc\u02fd\3\2\2\2\u02fd\u02fe\5p9\2\u02fe\u008b\3\2\2"+
		"\2\u02ff\u0301\5<\37\2\u0300\u02ff\3\2\2\2\u0301\u0302\3\2\2\2\u0302\u0300"+
		"\3\2\2\2\u0302\u0303\3\2\2\2\u0303\u008d\3\2\2\2\\\u0093\u00a6\u00a8\u00ab"+
		"\u00b1\u00b8\u00c1\u00ca\u00d4\u00dd\u00f7\u00fb\u0103\u0107\u0109\u0111"+
		"\u0117\u011e\u0129\u0130\u0138\u0140\u0148\u0150\u0158\u0160\u0168\u0170"+
		"\u0178\u0181\u0189\u0192\u0199\u01a0\u01a5\u01ae\u01b4\u01c3\u01c9\u01d3"+
		"\u01d7\u01e1\u01e6\u01f1\u01f4\u01f6\u01fe\u0200\u0206\u020d\u0215\u0217"+
		"\u021e\u0223\u022e\u0236\u0239\u023e\u0248\u024b\u024d\u0254\u0258\u025b"+
		"\u0260\u0265\u026e\u027a\u0287\u028b\u0292\u0296\u0299\u02a4\u02ac\u02c2"+
		"\u02c6\u02c8\u02cc\u02d0\u02d4\u02db\u02e1\u02e3\u02e8\u02ef\u02f4\u02f7"+
		"\u02fb\u0302";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}