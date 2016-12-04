// $ANTLR 3.1.2 C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g 2010-08-04 14:06:49

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class StitchLexer : Lexer {
    public const int WS = 9;
    public const int T__16 = 16;
    public const int T__15 = 15;
    public const int T__12 = 12;
    public const int T__11 = 11;
    public const int T__14 = 14;
    public const int LETTER = 7;
    public const int T__13 = 13;
    public const int T__10 = 10;
    public const int FILEPATH = 6;
    public const int IDENT = 4;
    public const int DIGIT = 8;
    public const int EOF = -1;
    public const int CODEBLOCK = 5;

    // delegates
    // delegators

    public StitchLexer() 
    {
		InitializeCyclicDFAs();
    }
    public StitchLexer(ICharStream input)
		: this(input, null) {
    }
    public StitchLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g";} 
    }

    // $ANTLR start "T__10"
    public void mT__10() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__10;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:7:7: ( '<' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:7:9: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__10"

    // $ANTLR start "T__11"
    public void mT__11() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__11;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:8:7: ( '(' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:8:9: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__11"

    // $ANTLR start "T__12"
    public void mT__12() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__12;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:9:7: ( ')' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:9:9: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__12"

    // $ANTLR start "T__13"
    public void mT__13() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__13;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:10:7: ( 'return(' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:10:9: 'return('
            {
            	Match("return("); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__13"

    // $ANTLR start "T__14"
    public void mT__14() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__14;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:11:7: ( ')>' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:11:9: ')>'
            {
            	Match(")>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__14"

    // $ANTLR start "T__15"
    public void mT__15() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__15;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:12:7: ( ',' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:12:9: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__15"

    // $ANTLR start "T__16"
    public void mT__16() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__16;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:13:7: ( 'include' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:13:9: 'include'
            {
            	Match("include"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__16"

    // $ANTLR start "CODEBLOCK"
    public void mCODEBLOCK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CODEBLOCK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:69:11: ( '>' ( . )* '<' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:69:13: '>' ( . )* '<'
            {
            	Match('>'); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:69:17: ( . )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == '<') )
            	    {
            	        alt1 = 2;
            	    }
            	    else if ( ((LA1_0 >= '\u0000' && LA1_0 <= ';') || (LA1_0 >= '=' && LA1_0 <= '\uFFFF')) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:69:17: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements

            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CODEBLOCK"

    // $ANTLR start "LETTER"
    public void mLETTER() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:71:17: ( ( 'a' .. 'z' | 'A' .. 'Z' ) )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:71:19: ( 'a' .. 'z' | 'A' .. 'Z' )
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LETTER"

    // $ANTLR start "DIGIT"
    public void mDIGIT() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:72:16: ( '0' .. '9' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:72:18: '0' .. '9'
            {
            	MatchRange('0','9'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIGIT"

    // $ANTLR start "FILEPATH"
    public void mFILEPATH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FILEPATH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:74:10: ( ( LETTER | DIGIT ) ( LETTER | DIGIT | '.' | '\\\\' | '/' )* '.' ( LETTER | DIGIT )+ )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:74:12: ( LETTER | DIGIT ) ( LETTER | DIGIT | '.' | '\\\\' | '/' )* '.' ( LETTER | DIGIT )+
            {
            	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:74:29: ( LETTER | DIGIT | '.' | '\\\\' | '/' )*
            	do 
            	{
            	    int alt2 = 2;
            	    alt2 = dfa2.Predict(input);
            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:
            			    {
            			    	if ( (input.LA(1) >= '.' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '\\' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop2;
            	    }
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements

            	Match('.'); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:74:70: ( LETTER | DIGIT )+
            	int cnt3 = 0;
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);

            	    if ( ((LA3_0 >= '0' && LA3_0 <= '9') || (LA3_0 >= 'A' && LA3_0 <= 'Z') || (LA3_0 >= 'a' && LA3_0 <= 'z')) )
            	    {
            	        alt3 = 1;
            	    }


            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt3 >= 1 ) goto loop3;
            		            EarlyExitException eee3 =
            		                new EarlyExitException(3, input);
            		            throw eee3;
            	    }
            	    cnt3++;
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whinging that label 'loop3' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FILEPATH"

    // $ANTLR start "IDENT"
    public void mIDENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IDENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:75:7: ( LETTER ( LETTER | DIGIT )* )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:75:9: LETTER ( LETTER | DIGIT )*
            {
            	mLETTER(); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:75:16: ( LETTER | DIGIT )*
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( ((LA4_0 >= '0' && LA4_0 <= '9') || (LA4_0 >= 'A' && LA4_0 <= 'Z') || (LA4_0 >= 'a' && LA4_0 <= 'z')) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop4;
            	    }
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IDENT"

    // $ANTLR start "WS"
    public void mWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:76:4: ( ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+ )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:76:6: ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+
            {
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:76:6: ( ' ' | '\\t' | '\\n' | '\\r' | '\\f' )+
            	int cnt5 = 0;
            	do 
            	{
            	    int alt5 = 2;
            	    int LA5_0 = input.LA(1);

            	    if ( ((LA5_0 >= '\t' && LA5_0 <= '\n') || (LA5_0 >= '\f' && LA5_0 <= '\r') || LA5_0 == ' ') )
            	    {
            	        alt5 = 1;
            	    }


            	    switch (alt5) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:
            			    {
            			    	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || (input.LA(1) >= '\f' && input.LA(1) <= '\r') || input.LA(1) == ' ' ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt5 >= 1 ) goto loop5;
            		            EarlyExitException eee5 =
            		                new EarlyExitException(5, input);
            		            throw eee5;
            	    }
            	    cnt5++;
            	} while (true);

            	loop5:
            		;	// Stops C# compiler whinging that label 'loop5' has no statements

            	_channel = HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WS"

    override public void mTokens() // throws RecognitionException 
    {
        // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:8: ( T__10 | T__11 | T__12 | T__13 | T__14 | T__15 | T__16 | CODEBLOCK | FILEPATH | IDENT | WS )
        int alt6 = 11;
        alt6 = dfa6.Predict(input);
        switch (alt6) 
        {
            case 1 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:10: T__10
                {
                	mT__10(); 

                }
                break;
            case 2 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:16: T__11
                {
                	mT__11(); 

                }
                break;
            case 3 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:22: T__12
                {
                	mT__12(); 

                }
                break;
            case 4 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:28: T__13
                {
                	mT__13(); 

                }
                break;
            case 5 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:34: T__14
                {
                	mT__14(); 

                }
                break;
            case 6 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:40: T__15
                {
                	mT__15(); 

                }
                break;
            case 7 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:46: T__16
                {
                	mT__16(); 

                }
                break;
            case 8 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:52: CODEBLOCK
                {
                	mCODEBLOCK(); 

                }
                break;
            case 9 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:62: FILEPATH
                {
                	mFILEPATH(); 

                }
                break;
            case 10 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:71: IDENT
                {
                	mIDENT(); 

                }
                break;
            case 11 :
                // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:1:77: WS
                {
                	mWS(); 

                }
                break;

        }

    }


    protected DFA2 dfa2;
    protected DFA6 dfa6;
	private void InitializeCyclicDFAs()
	{
	    this.dfa2 = new DFA2(this);
	    this.dfa6 = new DFA6(this);


	}

    const string DFA2_eotS =
        "\x03\uffff\x01\x04\x01\uffff";
    const string DFA2_eofS =
        "\x05\uffff";
    const string DFA2_minS =
        "\x02\x2e\x01\uffff\x01\x2e\x01\uffff";
    const string DFA2_maxS =
        "\x02\x7a\x01\uffff\x01\x7a\x01\uffff";
    const string DFA2_acceptS =
        "\x02\uffff\x01\x01\x01\uffff\x01\x02";
    const string DFA2_specialS =
        "\x05\uffff}>";
    static readonly string[] DFA2_transitionS = {
            "\x01\x01\x0b\x02\x07\uffff\x1a\x02\x01\uffff\x01\x02\x04\uffff"+
            "\x1a\x02",
            "\x02\x02\x0a\x03\x07\uffff\x1a\x03\x01\uffff\x01\x02\x04\uffff"+
            "\x1a\x03",
            "",
            "\x02\x02\x0a\x03\x07\uffff\x1a\x03\x01\uffff\x01\x02\x04\uffff"+
            "\x1a\x03",
            ""
    };

    static readonly short[] DFA2_eot = DFA.UnpackEncodedString(DFA2_eotS);
    static readonly short[] DFA2_eof = DFA.UnpackEncodedString(DFA2_eofS);
    static readonly char[] DFA2_min = DFA.UnpackEncodedStringToUnsignedChars(DFA2_minS);
    static readonly char[] DFA2_max = DFA.UnpackEncodedStringToUnsignedChars(DFA2_maxS);
    static readonly short[] DFA2_accept = DFA.UnpackEncodedString(DFA2_acceptS);
    static readonly short[] DFA2_special = DFA.UnpackEncodedString(DFA2_specialS);
    static readonly short[][] DFA2_transition = DFA.UnpackEncodedStringArray(DFA2_transitionS);

    protected class DFA2 : DFA
    {
        public DFA2(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 2;
            this.eot = DFA2_eot;
            this.eof = DFA2_eof;
            this.min = DFA2_min;
            this.max = DFA2_max;
            this.accept = DFA2_accept;
            this.special = DFA2_special;
            this.transition = DFA2_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 74:29: ( LETTER | DIGIT | '.' | '\\\\' | '/' )*"; }
        }

    }

    const string DFA6_eotS =
        "\x03\uffff\x01\x0c\x01\x0e\x01\uffff\x01\x0e\x01\uffff\x01\x0e"+
        "\x04\uffff\x01\x0e\x01\uffff\x0a\x0e\x01\uffff\x01\x1b\x01\uffff";
    const string DFA6_eofS =
        "\x1c\uffff";
    const string DFA6_minS =
        "\x01\x09\x02\uffff\x01\x3e\x01\x2e\x01\uffff\x01\x2e\x01\uffff"+
        "\x01\x2e\x04\uffff\x01\x2e\x01\uffff\x08\x2e\x01\x28\x01\x2e\x01"+
        "\uffff\x01\x2e\x01\uffff";
    const string DFA6_maxS =
        "\x01\x7a\x02\uffff\x01\x3e\x01\x7a\x01\uffff\x01\x7a\x01\uffff"+
        "\x01\x7a\x04\uffff\x01\x7a\x01\uffff\x0a\x7a\x01\uffff\x01\x7a\x01"+
        "\uffff";
    const string DFA6_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x02\uffff\x01\x06\x01\uffff\x01\x08"+
        "\x01\uffff\x01\x09\x01\x0b\x01\x05\x01\x03\x01\uffff\x01\x0a\x0a"+
        "\uffff\x01\x04\x01\uffff\x01\x07";
    const string DFA6_specialS =
        "\x1c\uffff}>";
    static readonly string[] DFA6_transitionS = {
            "\x02\x0a\x01\uffff\x02\x0a\x12\uffff\x01\x0a\x07\uffff\x01"+
            "\x02\x01\x03\x02\uffff\x01\x05\x03\uffff\x0a\x09\x02\uffff\x01"+
            "\x01\x01\uffff\x01\x07\x02\uffff\x1a\x08\x06\uffff\x08\x08\x01"+
            "\x06\x08\x08\x01\x04\x08\x08",
            "",
            "",
            "\x01\x0b",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x04\x0f\x01\x0d\x15\x0f",
            "",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x0d\x0f\x01\x10\x0c\x0f",
            "",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x1a\x0f",
            "",
            "",
            "",
            "",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x13\x0f\x01\x11\x06\x0f",
            "",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x1a\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x02\x0f\x01\x12\x17\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x14\x0f\x01\x13\x05\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x0b\x0f\x01\x14\x0e\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x11\x0f\x01\x15\x08\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x14\x0f\x01\x16\x05\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x0d\x0f\x01\x17\x0c\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x03\x0f\x01\x18\x16\x0f",
            "\x01\x19\x05\uffff\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff"+
            "\x01\x09\x04\uffff\x1a\x0f",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x04\x0f\x01\x1a\x15\x0f",
            "",
            "\x02\x09\x0a\x0f\x07\uffff\x1a\x0f\x01\uffff\x01\x09\x04\uffff"+
            "\x1a\x0f",
            ""
    };

    static readonly short[] DFA6_eot = DFA.UnpackEncodedString(DFA6_eotS);
    static readonly short[] DFA6_eof = DFA.UnpackEncodedString(DFA6_eofS);
    static readonly char[] DFA6_min = DFA.UnpackEncodedStringToUnsignedChars(DFA6_minS);
    static readonly char[] DFA6_max = DFA.UnpackEncodedStringToUnsignedChars(DFA6_maxS);
    static readonly short[] DFA6_accept = DFA.UnpackEncodedString(DFA6_acceptS);
    static readonly short[] DFA6_special = DFA.UnpackEncodedString(DFA6_specialS);
    static readonly short[][] DFA6_transition = DFA.UnpackEncodedStringArray(DFA6_transitionS);

    protected class DFA6 : DFA
    {
        public DFA6(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 6;
            this.eot = DFA6_eot;
            this.eof = DFA6_eof;
            this.min = DFA6_min;
            this.max = DFA6_max;
            this.accept = DFA6_accept;
            this.special = DFA6_special;
            this.transition = DFA6_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( T__10 | T__11 | T__12 | T__13 | T__14 | T__15 | T__16 | CODEBLOCK | FILEPATH | IDENT | WS );"; }
        }

    }

 
    
}
