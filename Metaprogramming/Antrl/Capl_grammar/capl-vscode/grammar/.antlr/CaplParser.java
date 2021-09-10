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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, FLOAT=6, STRING=7, INT=8, ENUM=9, 
		FOR=10, ID=11, MUL=12, DIV=13, PLUS=14, MINUS=15, LINE_COMMENT=16, COMMENT=17, 
		LEFT_PAREN=18, RIGHT_PAREN=19, LEFT_BLOCK_PAREN=20, RIGHT_BLOCK_PAREN=21, 
		WS=22;
	public static final int
		RULE_compilationUnit = 0, RULE_variableSection = 1, RULE_assignmentSet = 2, 
		RULE_assignmentExpr = 3, RULE_type = 4, RULE_expr = 5;
	private static String[] makeRuleNames() {
		return new String[] {
			"compilationUnit", "variableSection", "assignmentSet", "assignmentExpr", 
			"type", "expr"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'variables'", "'='", "';'", "'int'", "'float'", null, null, null, 
			"'enum'", "'for'", null, "'*'", "'/'", "'+'", "'-'", null, null, "'('", 
			"')'", "'{'", "'}'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, "FLOAT", "STRING", "INT", "ENUM", 
			"FOR", "ID", "MUL", "DIV", "PLUS", "MINUS", "LINE_COMMENT", "COMMENT", 
			"LEFT_PAREN", "RIGHT_PAREN", "LEFT_BLOCK_PAREN", "RIGHT_BLOCK_PAREN", 
			"WS"
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

	public static class CompilationUnitContext extends ParserRuleContext {
		public List<VariableSectionContext> variableSection() {
			return getRuleContexts(VariableSectionContext.class);
		}
		public VariableSectionContext variableSection(int i) {
			return getRuleContext(VariableSectionContext.class,i);
		}
		public CompilationUnitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compilationUnit; }
	}

	public final CompilationUnitContext compilationUnit() throws RecognitionException {
		CompilationUnitContext _localctx = new CompilationUnitContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_compilationUnit);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(15);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(12);
				variableSection();
				}
				}
				setState(17);
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

	public static class VariableSectionContext extends ParserRuleContext {
		public TerminalNode LEFT_BLOCK_PAREN() { return getToken(CaplParser.LEFT_BLOCK_PAREN, 0); }
		public AssignmentSetContext assignmentSet() {
			return getRuleContext(AssignmentSetContext.class,0);
		}
		public TerminalNode RIGHT_BLOCK_PAREN() { return getToken(CaplParser.RIGHT_BLOCK_PAREN, 0); }
		public VariableSectionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variableSection; }
	}

	public final VariableSectionContext variableSection() throws RecognitionException {
		VariableSectionContext _localctx = new VariableSectionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_variableSection);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(18);
			match(T__0);
			setState(19);
			match(LEFT_BLOCK_PAREN);
			setState(20);
			assignmentSet();
			setState(21);
			match(RIGHT_BLOCK_PAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentSetContext extends ParserRuleContext {
		public List<AssignmentExprContext> assignmentExpr() {
			return getRuleContexts(AssignmentExprContext.class);
		}
		public AssignmentExprContext assignmentExpr(int i) {
			return getRuleContext(AssignmentExprContext.class,i);
		}
		public AssignmentSetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignmentSet; }
	}

	public final AssignmentSetContext assignmentSet() throws RecognitionException {
		AssignmentSetContext _localctx = new AssignmentSetContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_assignmentSet);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(26);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__3 || _la==T__4) {
				{
				{
				setState(23);
				assignmentExpr();
				}
				}
				setState(28);
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

	public static class AssignmentExprContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(CaplParser.ID, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public AssignmentExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignmentExpr; }
	}

	public final AssignmentExprContext assignmentExpr() throws RecognitionException {
		AssignmentExprContext _localctx = new AssignmentExprContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_assignmentExpr);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(29);
			type();
			setState(30);
			match(ID);
			setState(33);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__1) {
				{
				setState(31);
				match(T__1);
				setState(32);
				expr(0);
				}
			}

			setState(35);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(37);
			_la = _input.LA(1);
			if ( !(_la==T__3 || _la==T__4) ) {
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

	public static class ExprContext extends ParserRuleContext {
		public TerminalNode LEFT_PAREN() { return getToken(CaplParser.LEFT_PAREN, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public TerminalNode RIGHT_PAREN() { return getToken(CaplParser.RIGHT_PAREN, 0); }
		public TerminalNode ID() { return getToken(CaplParser.ID, 0); }
		public TerminalNode INT() { return getToken(CaplParser.INT, 0); }
		public TerminalNode FLOAT() { return getToken(CaplParser.FLOAT, 0); }
		public TerminalNode MUL() { return getToken(CaplParser.MUL, 0); }
		public TerminalNode DIV() { return getToken(CaplParser.DIV, 0); }
		public TerminalNode PLUS() { return getToken(CaplParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(CaplParser.MINUS, 0); }
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 10;
		enterRecursionRule(_localctx, 10, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(47);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LEFT_PAREN:
				{
				setState(40);
				match(LEFT_PAREN);
				setState(41);
				expr(0);
				setState(42);
				match(RIGHT_PAREN);
				}
				break;
			case ID:
				{
				setState(44);
				match(ID);
				}
				break;
			case INT:
				{
				setState(45);
				match(INT);
				}
				break;
			case FLOAT:
				{
				setState(46);
				match(FLOAT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(57);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(55);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
					case 1:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(49);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(50);
						_la = _input.LA(1);
						if ( !(_la==MUL || _la==DIV) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(51);
						expr(7);
						}
						break;
					case 2:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(52);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(53);
						_la = _input.LA(1);
						if ( !(_la==PLUS || _la==MINUS) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(54);
						expr(6);
						}
						break;
					}
					} 
				}
				setState(59);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
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

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 5:
			return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 6);
		case 1:
			return precpred(_ctx, 5);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\30?\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\3\2\7\2\20\n\2\f\2\16\2\23\13\2\3\3"+
		"\3\3\3\3\3\3\3\3\3\4\7\4\33\n\4\f\4\16\4\36\13\4\3\5\3\5\3\5\3\5\5\5$"+
		"\n\5\3\5\3\5\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7\62\n\7\3\7\3"+
		"\7\3\7\3\7\3\7\3\7\7\7:\n\7\f\7\16\7=\13\7\3\7\2\3\f\b\2\4\6\b\n\f\2\5"+
		"\3\2\6\7\3\2\16\17\3\2\20\21\2@\2\21\3\2\2\2\4\24\3\2\2\2\6\34\3\2\2\2"+
		"\b\37\3\2\2\2\n\'\3\2\2\2\f\61\3\2\2\2\16\20\5\4\3\2\17\16\3\2\2\2\20"+
		"\23\3\2\2\2\21\17\3\2\2\2\21\22\3\2\2\2\22\3\3\2\2\2\23\21\3\2\2\2\24"+
		"\25\7\3\2\2\25\26\7\26\2\2\26\27\5\6\4\2\27\30\7\27\2\2\30\5\3\2\2\2\31"+
		"\33\5\b\5\2\32\31\3\2\2\2\33\36\3\2\2\2\34\32\3\2\2\2\34\35\3\2\2\2\35"+
		"\7\3\2\2\2\36\34\3\2\2\2\37 \5\n\6\2 #\7\r\2\2!\"\7\4\2\2\"$\5\f\7\2#"+
		"!\3\2\2\2#$\3\2\2\2$%\3\2\2\2%&\7\5\2\2&\t\3\2\2\2\'(\t\2\2\2(\13\3\2"+
		"\2\2)*\b\7\1\2*+\7\24\2\2+,\5\f\7\2,-\7\25\2\2-\62\3\2\2\2.\62\7\r\2\2"+
		"/\62\7\n\2\2\60\62\7\b\2\2\61)\3\2\2\2\61.\3\2\2\2\61/\3\2\2\2\61\60\3"+
		"\2\2\2\62;\3\2\2\2\63\64\f\b\2\2\64\65\t\3\2\2\65:\5\f\7\t\66\67\f\7\2"+
		"\2\678\t\4\2\28:\5\f\7\b9\63\3\2\2\29\66\3\2\2\2:=\3\2\2\2;9\3\2\2\2;"+
		"<\3\2\2\2<\r\3\2\2\2=;\3\2\2\2\b\21\34#\619;";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}