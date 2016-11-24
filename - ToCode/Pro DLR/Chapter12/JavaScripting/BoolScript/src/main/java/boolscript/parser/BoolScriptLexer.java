// $ANTLR 3.2 Sep 23, 2009 12:02:23 C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g 2010-11-19 23:12:45

  package boolscript.parser;


import org.antlr.runtime.*;
import java.util.Stack;
import java.util.List;
import java.util.ArrayList;

public class BoolScriptLexer extends Lexer {
    public static final int WS=7;
    public static final int T__12=12;
    public static final int T__11=11;
    public static final int T__14=14;
    public static final int LETTER=5;
    public static final int T__13=13;
    public static final int T__10=10;
    public static final int IDENT=4;
    public static final int DIGIT=6;
    public static final int EOF=-1;
    public static final int T__9=9;
    public static final int T__8=8;

    // delegates
    // delegators

    public BoolScriptLexer() {;} 
    public BoolScriptLexer(CharStream input) {
        this(input, new RecognizerSharedState());
    }
    public BoolScriptLexer(CharStream input, RecognizerSharedState state) {
        super(input,state);

    }
    public String getGrammarFileName() { return "C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g"; }

    // $ANTLR start "T__8"
    public final void mT__8() throws RecognitionException {
        try {
            int _type = T__8;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:11:6: ( ';' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:11:8: ';'
            {
            match(';'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__8"

    // $ANTLR start "T__9"
    public final void mT__9() throws RecognitionException {
        try {
            int _type = T__9;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:12:6: ( '&' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:12:8: '&'
            {
            match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__9"

    // $ANTLR start "T__10"
    public final void mT__10() throws RecognitionException {
        try {
            int _type = T__10;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:13:7: ( '|' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:13:9: '|'
            {
            match('|'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__10"

    // $ANTLR start "T__11"
    public final void mT__11() throws RecognitionException {
        try {
            int _type = T__11;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:14:7: ( '(' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:14:9: '('
            {
            match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__11"

    // $ANTLR start "T__12"
    public final void mT__12() throws RecognitionException {
        try {
            int _type = T__12;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:15:7: ( ')' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:15:9: ')'
            {
            match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__12"

    // $ANTLR start "T__13"
    public final void mT__13() throws RecognitionException {
        try {
            int _type = T__13;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:16:7: ( 'True' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:16:9: 'True'
            {
            match("True"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__13"

    // $ANTLR start "T__14"
    public final void mT__14() throws RecognitionException {
        try {
            int _type = T__14;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:17:7: ( 'False' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:17:9: 'False'
            {
            match("False"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "T__14"

    // $ANTLR start "LETTER"
    public final void mLETTER() throws RecognitionException {
        try {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:53:17: ( ( 'a' .. 'z' | 'A' .. 'Z' ) )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:53:19: ( 'a' .. 'z' | 'A' .. 'Z' )
            {
            if ( (input.LA(1)>='A' && input.LA(1)<='Z')||(input.LA(1)>='a' && input.LA(1)<='z') ) {
                input.consume();

            }
            else {
                MismatchedSetException mse = new MismatchedSetException(null,input);
                recover(mse);
                throw mse;}


            }

        }
        finally {
        }
    }
    // $ANTLR end "LETTER"

    // $ANTLR start "DIGIT"
    public final void mDIGIT() throws RecognitionException {
        try {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:54:16: ( '0' .. '9' )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:54:18: '0' .. '9'
            {
            matchRange('0','9'); 

            }

        }
        finally {
        }
    }
    // $ANTLR end "DIGIT"

    // $ANTLR start "IDENT"
    public final void mIDENT() throws RecognitionException {
        try {
            int _type = IDENT;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:55:7: ( LETTER ( LETTER | DIGIT )* )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:55:9: LETTER ( LETTER | DIGIT )*
            {
            mLETTER(); 
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:55:16: ( LETTER | DIGIT )*
            loop1:
            do {
                int alt1=2;
                int LA1_0 = input.LA(1);

                if ( ((LA1_0>='0' && LA1_0<='9')||(LA1_0>='A' && LA1_0<='Z')||(LA1_0>='a' && LA1_0<='z')) ) {
                    alt1=1;
                }


                switch (alt1) {
            	case 1 :
            	    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:
            	    {
            	    if ( (input.LA(1)>='0' && input.LA(1)<='9')||(input.LA(1)>='A' && input.LA(1)<='Z')||(input.LA(1)>='a' && input.LA(1)<='z') ) {
            	        input.consume();

            	    }
            	    else {
            	        MismatchedSetException mse = new MismatchedSetException(null,input);
            	        recover(mse);
            	        throw mse;}


            	    }
            	    break;

            	default :
            	    break loop1;
                }
            } while (true);


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "IDENT"

    // $ANTLR start "WS"
    public final void mWS() throws RecognitionException {
        try {
            int _type = WS;
            int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:56:4: ( ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+ )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:56:6: ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+
            {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:56:6: ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+
            int cnt2=0;
            loop2:
            do {
                int alt2=2;
                int LA2_0 = input.LA(1);

                if ( ((LA2_0>='\t' && LA2_0<='\n')||(LA2_0>='\f' && LA2_0<='\r')||LA2_0==' ') ) {
                    alt2=1;
                }


                switch (alt2) {
            	case 1 :
            	    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:
            	    {
            	    if ( (input.LA(1)>='\t' && input.LA(1)<='\n')||(input.LA(1)>='\f' && input.LA(1)<='\r')||input.LA(1)==' ' ) {
            	        input.consume();

            	    }
            	    else {
            	        MismatchedSetException mse = new MismatchedSetException(null,input);
            	        recover(mse);
            	        throw mse;}


            	    }
            	    break;

            	default :
            	    if ( cnt2 >= 1 ) break loop2;
                        EarlyExitException eee =
                            new EarlyExitException(2, input);
                        throw eee;
                }
                cnt2++;
            } while (true);

            _channel = HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally {
        }
    }
    // $ANTLR end "WS"

    public void mTokens() throws RecognitionException {
        // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:8: ( T__8 | T__9 | T__10 | T__11 | T__12 | T__13 | T__14 | IDENT | WS )
        int alt3=9;
        alt3 = dfa3.predict(input);
        switch (alt3) {
            case 1 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:10: T__8
                {
                mT__8(); 

                }
                break;
            case 2 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:15: T__9
                {
                mT__9(); 

                }
                break;
            case 3 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:20: T__10
                {
                mT__10(); 

                }
                break;
            case 4 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:26: T__11
                {
                mT__11(); 

                }
                break;
            case 5 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:32: T__12
                {
                mT__12(); 

                }
                break;
            case 6 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:38: T__13
                {
                mT__13(); 

                }
                break;
            case 7 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:44: T__14
                {
                mT__14(); 

                }
                break;
            case 8 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:50: IDENT
                {
                mIDENT(); 

                }
                break;
            case 9 :
                // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:1:56: WS
                {
                mWS(); 

                }
                break;

        }

    }


    protected DFA3 dfa3 = new DFA3(this);
    static final String DFA3_eotS =
        "\6\uffff\2\10\2\uffff\4\10\1\20\1\10\1\uffff\1\22\1\uffff";
    static final String DFA3_eofS =
        "\23\uffff";
    static final String DFA3_minS =
        "\1\11\5\uffff\1\162\1\141\2\uffff\1\165\1\154\1\145\1\163\1\60"+
        "\1\145\1\uffff\1\60\1\uffff";
    static final String DFA3_maxS =
        "\1\174\5\uffff\1\162\1\141\2\uffff\1\165\1\154\1\145\1\163\1\172"+
        "\1\145\1\uffff\1\172\1\uffff";
    static final String DFA3_acceptS =
        "\1\uffff\1\1\1\2\1\3\1\4\1\5\2\uffff\1\10\1\11\6\uffff\1\6\1\uffff"+
        "\1\7";
    static final String DFA3_specialS =
        "\23\uffff}>";
    static final String[] DFA3_transitionS = {
            "\2\11\1\uffff\2\11\22\uffff\1\11\5\uffff\1\2\1\uffff\1\4\1"+
            "\5\21\uffff\1\1\5\uffff\5\10\1\7\15\10\1\6\6\10\6\uffff\32\10"+
            "\1\uffff\1\3",
            "",
            "",
            "",
            "",
            "",
            "\1\12",
            "\1\13",
            "",
            "",
            "\1\14",
            "\1\15",
            "\1\16",
            "\1\17",
            "\12\10\7\uffff\32\10\6\uffff\32\10",
            "\1\21",
            "",
            "\12\10\7\uffff\32\10\6\uffff\32\10",
            ""
    };

    static final short[] DFA3_eot = DFA.unpackEncodedString(DFA3_eotS);
    static final short[] DFA3_eof = DFA.unpackEncodedString(DFA3_eofS);
    static final char[] DFA3_min = DFA.unpackEncodedStringToUnsignedChars(DFA3_minS);
    static final char[] DFA3_max = DFA.unpackEncodedStringToUnsignedChars(DFA3_maxS);
    static final short[] DFA3_accept = DFA.unpackEncodedString(DFA3_acceptS);
    static final short[] DFA3_special = DFA.unpackEncodedString(DFA3_specialS);
    static final short[][] DFA3_transition;

    static {
        int numStates = DFA3_transitionS.length;
        DFA3_transition = new short[numStates][];
        for (int i=0; i<numStates; i++) {
            DFA3_transition[i] = DFA.unpackEncodedString(DFA3_transitionS[i]);
        }
    }

    class DFA3 extends DFA {

        public DFA3(BaseRecognizer recognizer) {
            this.recognizer = recognizer;
            this.decisionNumber = 3;
            this.eot = DFA3_eot;
            this.eof = DFA3_eof;
            this.min = DFA3_min;
            this.max = DFA3_max;
            this.accept = DFA3_accept;
            this.special = DFA3_special;
            this.transition = DFA3_transition;
        }
        public String getDescription() {
            return "1:1: Tokens : ( T__8 | T__9 | T__10 | T__11 | T__12 | T__13 | T__14 | IDENT | WS );";
        }
    }
 

}