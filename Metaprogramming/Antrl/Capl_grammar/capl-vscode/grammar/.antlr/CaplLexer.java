// Generated from d:\Projects\dotNET\appDev-NET\Metaprogramming\Antrl\Capl_grammar\capl-vscode\grammar\Capl.g4 by ANTLR 4.8
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class CaplLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, FLOAT=6, STRING=7, INT=8, ENUM=9, 
		FOR=10, ID=11, MUL=12, DIV=13, PLUS=14, MINUS=15, LINE_COMMENT=16, COMMENT=17, 
		LEFT_PAREN=18, RIGHT_PAREN=19, LEFT_BLOCK_PAREN=20, RIGHT_BLOCK_PAREN=21, 
		WS=22;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "FLOAT", "STRING", "ESC", "INT", 
			"DIGIT", "ENUM", "FOR", "ID", "ID_LETTER", "MUL", "DIV", "PLUS", "MINUS", 
			"LINE_COMMENT", "COMMENT", "LEFT_PAREN", "RIGHT_PAREN", "LEFT_BLOCK_PAREN", 
			"RIGHT_BLOCK_PAREN", "WS"
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


	public CaplLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Capl.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\30\u00b7\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3\4\3"+
		"\4\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\6\7O\n\7\r\7\16\7P\3\7"+
		"\3\7\7\7U\n\7\f\7\16\7X\13\7\5\7Z\n\7\3\b\3\b\3\b\7\b_\n\b\f\b\16\bb\13"+
		"\b\3\b\3\b\3\t\3\t\3\t\3\n\6\nj\n\n\r\n\16\nk\3\13\3\13\3\f\3\f\3\f\3"+
		"\f\3\f\3\r\3\r\3\r\3\r\3\16\3\16\3\16\7\16|\n\16\f\16\16\16\177\13\16"+
		"\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\23\3\23\3\24\3\24\3\24\3\24"+
		"\7\24\u008f\n\24\f\24\16\24\u0092\13\24\3\24\5\24\u0095\n\24\3\24\3\24"+
		"\3\24\3\24\3\25\3\25\3\25\3\25\7\25\u009f\n\25\f\25\16\25\u00a2\13\25"+
		"\3\25\3\25\3\25\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3\32"+
		"\6\32\u00b2\n\32\r\32\16\32\u00b3\3\32\3\32\5`\u0090\u00a0\2\33\3\3\5"+
		"\4\7\5\t\6\13\7\r\b\17\t\21\2\23\n\25\2\27\13\31\f\33\r\35\2\37\16!\17"+
		"#\20%\21\'\22)\23+\24-\25/\26\61\27\63\30\3\2\6\b\2$$^^ddppttvv\3\2\62"+
		";\5\2C\\aac|\5\2\13\f\17\17\"\"\2\u00bf\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3"+
		"\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\23\3\2\2\2"+
		"\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2"+
		"\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2"+
		"\2\2\61\3\2\2\2\2\63\3\2\2\2\3\65\3\2\2\2\5?\3\2\2\2\7A\3\2\2\2\tC\3\2"+
		"\2\2\13G\3\2\2\2\rN\3\2\2\2\17[\3\2\2\2\21e\3\2\2\2\23i\3\2\2\2\25m\3"+
		"\2\2\2\27o\3\2\2\2\31t\3\2\2\2\33x\3\2\2\2\35\u0080\3\2\2\2\37\u0082\3"+
		"\2\2\2!\u0084\3\2\2\2#\u0086\3\2\2\2%\u0088\3\2\2\2\'\u008a\3\2\2\2)\u009a"+
		"\3\2\2\2+\u00a8\3\2\2\2-\u00aa\3\2\2\2/\u00ac\3\2\2\2\61\u00ae\3\2\2\2"+
		"\63\u00b1\3\2\2\2\65\66\7x\2\2\66\67\7c\2\2\678\7t\2\289\7k\2\29:\7c\2"+
		"\2:;\7d\2\2;<\7n\2\2<=\7g\2\2=>\7u\2\2>\4\3\2\2\2?@\7?\2\2@\6\3\2\2\2"+
		"AB\7=\2\2B\b\3\2\2\2CD\7k\2\2DE\7p\2\2EF\7v\2\2F\n\3\2\2\2GH\7h\2\2HI"+
		"\7n\2\2IJ\7q\2\2JK\7c\2\2KL\7v\2\2L\f\3\2\2\2MO\5\25\13\2NM\3\2\2\2OP"+
		"\3\2\2\2PN\3\2\2\2PQ\3\2\2\2QY\3\2\2\2RV\7\60\2\2SU\5\25\13\2TS\3\2\2"+
		"\2UX\3\2\2\2VT\3\2\2\2VW\3\2\2\2WZ\3\2\2\2XV\3\2\2\2YR\3\2\2\2YZ\3\2\2"+
		"\2Z\16\3\2\2\2[`\7$\2\2\\_\5\21\t\2]_\13\2\2\2^\\\3\2\2\2^]\3\2\2\2_b"+
		"\3\2\2\2`a\3\2\2\2`^\3\2\2\2ac\3\2\2\2b`\3\2\2\2cd\7$\2\2d\20\3\2\2\2"+
		"ef\7^\2\2fg\t\2\2\2g\22\3\2\2\2hj\5\25\13\2ih\3\2\2\2jk\3\2\2\2ki\3\2"+
		"\2\2kl\3\2\2\2l\24\3\2\2\2mn\t\3\2\2n\26\3\2\2\2op\7g\2\2pq\7p\2\2qr\7"+
		"w\2\2rs\7o\2\2s\30\3\2\2\2tu\7h\2\2uv\7q\2\2vw\7t\2\2w\32\3\2\2\2x}\5"+
		"\35\17\2y|\5\35\17\2z|\5\25\13\2{y\3\2\2\2{z\3\2\2\2|\177\3\2\2\2}{\3"+
		"\2\2\2}~\3\2\2\2~\34\3\2\2\2\177}\3\2\2\2\u0080\u0081\t\4\2\2\u0081\36"+
		"\3\2\2\2\u0082\u0083\7,\2\2\u0083 \3\2\2\2\u0084\u0085\7\61\2\2\u0085"+
		"\"\3\2\2\2\u0086\u0087\7-\2\2\u0087$\3\2\2\2\u0088\u0089\7/\2\2\u0089"+
		"&\3\2\2\2\u008a\u008b\7\61\2\2\u008b\u008c\7\61\2\2\u008c\u0090\3\2\2"+
		"\2\u008d\u008f\13\2\2\2\u008e\u008d\3\2\2\2\u008f\u0092\3\2\2\2\u0090"+
		"\u0091\3\2\2\2\u0090\u008e\3\2\2\2\u0091\u0094\3\2\2\2\u0092\u0090\3\2"+
		"\2\2\u0093\u0095\7\17\2\2\u0094\u0093\3\2\2\2\u0094\u0095\3\2\2\2\u0095"+
		"\u0096\3\2\2\2\u0096\u0097\7\f\2\2\u0097\u0098\3\2\2\2\u0098\u0099\b\24"+
		"\2\2\u0099(\3\2\2\2\u009a\u009b\7\61\2\2\u009b\u009c\7,\2\2\u009c\u00a0"+
		"\3\2\2\2\u009d\u009f\13\2\2\2\u009e\u009d\3\2\2\2\u009f\u00a2\3\2\2\2"+
		"\u00a0\u00a1\3\2\2\2\u00a0\u009e\3\2\2\2\u00a1\u00a3\3\2\2\2\u00a2\u00a0"+
		"\3\2\2\2\u00a3\u00a4\7,\2\2\u00a4\u00a5\7\61\2\2\u00a5\u00a6\3\2\2\2\u00a6"+
		"\u00a7\b\25\2\2\u00a7*\3\2\2\2\u00a8\u00a9\7*\2\2\u00a9,\3\2\2\2\u00aa"+
		"\u00ab\7+\2\2\u00ab.\3\2\2\2\u00ac\u00ad\7}\2\2\u00ad\60\3\2\2\2\u00ae"+
		"\u00af\7\177\2\2\u00af\62\3\2\2\2\u00b0\u00b2\t\5\2\2\u00b1\u00b0\3\2"+
		"\2\2\u00b2\u00b3\3\2\2\2\u00b3\u00b1\3\2\2\2\u00b3\u00b4\3\2\2\2\u00b4"+
		"\u00b5\3\2\2\2\u00b5\u00b6\b\32\2\2\u00b6\64\3\2\2\2\17\2PVY^`k{}\u0090"+
		"\u0094\u00a0\u00b3\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}