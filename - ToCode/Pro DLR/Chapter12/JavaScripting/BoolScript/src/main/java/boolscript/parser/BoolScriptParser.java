// $ANTLR 3.2 Sep 23, 2009 12:02:23 C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g 2010-11-19 23:12:45

  package boolscript.parser;
  import java.util.List;
  import java.util.ArrayList;
  import boolscript.ast.BoolExpression;
  import boolscript.ast.AndExpression;
  import boolscript.ast.OrExpression;
  import boolscript.ast.VarExpression;
  import boolscript.ast.TrueExpression;
  import boolscript.ast.FalseExpression;


import org.antlr.runtime.*;
import java.util.Stack;
import java.util.List;
import java.util.ArrayList;

public class BoolScriptParser extends Parser {
    public static final String[] tokenNames = new String[] {
        "<invalid>", "<EOR>", "<DOWN>", "<UP>", "IDENT", "LETTER", "DIGIT", "WS", "';'", "'&'", "'|'", "'('", "')'", "'True'", "'False'"
    };
    public static final int WS=7;
    public static final int T__12=12;
    public static final int T__11=11;
    public static final int LETTER=5;
    public static final int T__14=14;
    public static final int T__13=13;
    public static final int T__10=10;
    public static final int IDENT=4;
    public static final int DIGIT=6;
    public static final int EOF=-1;
    public static final int T__9=9;
    public static final int T__8=8;

    // delegates
    // delegators


        public BoolScriptParser(TokenStream input) {
            this(input, new RecognizerSharedState());
        }
        public BoolScriptParser(TokenStream input, RecognizerSharedState state) {
            super(input, state);
             
        }
        

    public String[] getTokenNames() { return BoolScriptParser.tokenNames; }
    public String getGrammarFileName() { return "C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g"; }


      public static List<BoolExpression> parse(String script) {
        CharStream codeStream = new ANTLRStringStream(script);
        BoolScriptLexer lexer = new BoolScriptLexer(codeStream);
        TokenStream tokenStream = new CommonTokenStream(lexer);
        BoolScriptParser parser = new BoolScriptParser(tokenStream);
        try {
          return parser.program();
        } catch (RecognitionException e) {
          throw new ParseException(e);
        }
      }



    // $ANTLR start "program"
    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:36:1: program returns [List<BoolExpression> result] : ( expression ';' )* EOF ;
    public final List<BoolExpression> program() throws RecognitionException {
        List<BoolExpression> result = null;

        BoolExpression expression1 = null;


        try {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:37:3: ( ( expression ';' )* EOF )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:37:5: ( expression ';' )* EOF
            {
             result = new ArrayList<BoolExpression>(); 
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:38:3: ( expression ';' )*
            loop1:
            do {
                int alt1=2;
                int LA1_0 = input.LA(1);

                if ( (LA1_0==IDENT||LA1_0==11||(LA1_0>=13 && LA1_0<=14)) ) {
                    alt1=1;
                }


                switch (alt1) {
            	case 1 :
            	    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:38:4: expression ';'
            	    {
            	    pushFollow(FOLLOW_expression_in_program58);
            	    expression1=expression();

            	    state._fsp--;

            	    match(input,8,FOLLOW_8_in_program60); 
            	     result.add(expression1); 

            	    }
            	    break;

            	default :
            	    break loop1;
                }
            } while (true);

            match(input,EOF,FOLLOW_EOF_in_program66); 

            }

        }
        catch (RecognitionException re) {
            reportError(re);
            recover(input,re);
        }
        finally {
        }
        return result;
    }
    // $ANTLR end "program"


    // $ANTLR start "expression"
    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:41:1: expression returns [BoolExpression result] : t1= term ( '&' t2= term | '|' t2= term )* ;
    public final BoolExpression expression() throws RecognitionException {
        BoolExpression result = null;

        BoolExpression t1 = null;

        BoolExpression t2 = null;


        try {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:42:3: (t1= term ( '&' t2= term | '|' t2= term )* )
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:42:5: t1= term ( '&' t2= term | '|' t2= term )*
            {
            pushFollow(FOLLOW_term_in_expression87);
            t1=term();

            state._fsp--;

             result = t1;
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:43:3: ( '&' t2= term | '|' t2= term )*
            loop2:
            do {
                int alt2=3;
                int LA2_0 = input.LA(1);

                if ( (LA2_0==9) ) {
                    alt2=1;
                }
                else if ( (LA2_0==10) ) {
                    alt2=2;
                }


                switch (alt2) {
            	case 1 :
            	    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:43:5: '&' t2= term
            	    {
            	    match(input,9,FOLLOW_9_in_expression96); 
            	    pushFollow(FOLLOW_term_in_expression100);
            	    t2=term();

            	    state._fsp--;

            	     result = new AndExpression(result, t2); 

            	    }
            	    break;
            	case 2 :
            	    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:44:5: '|' t2= term
            	    {
            	    match(input,10,FOLLOW_10_in_expression108); 
            	    pushFollow(FOLLOW_term_in_expression112);
            	    t2=term();

            	    state._fsp--;

            	     result = new OrExpression(result, t2); 

            	    }
            	    break;

            	default :
            	    break loop2;
                }
            } while (true);


            }

        }
        catch (RecognitionException re) {
            reportError(re);
            recover(input,re);
        }
        finally {
        }
        return result;
    }
    // $ANTLR end "expression"


    // $ANTLR start "term"
    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:47:1: term returns [BoolExpression result] : ( IDENT | '(' expression ')' | 'True' | 'False' );
    public final BoolExpression term() throws RecognitionException {
        BoolExpression result = null;

        Token IDENT2=null;
        BoolExpression expression3 = null;


        try {
            // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:48:3: ( IDENT | '(' expression ')' | 'True' | 'False' )
            int alt3=4;
            switch ( input.LA(1) ) {
            case IDENT:
                {
                alt3=1;
                }
                break;
            case 11:
                {
                alt3=2;
                }
                break;
            case 13:
                {
                alt3=3;
                }
                break;
            case 14:
                {
                alt3=4;
                }
                break;
            default:
                NoViableAltException nvae =
                    new NoViableAltException("", 3, 0, input);

                throw nvae;
            }

            switch (alt3) {
                case 1 :
                    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:48:5: IDENT
                    {
                    IDENT2=(Token)match(input,IDENT,FOLLOW_IDENT_in_term135); 
                     result = new VarExpression((IDENT2!=null?IDENT2.getText():null)); 

                    }
                    break;
                case 2 :
                    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:49:5: '(' expression ')'
                    {
                    match(input,11,FOLLOW_11_in_term143); 
                    pushFollow(FOLLOW_expression_in_term145);
                    expression3=expression();

                    state._fsp--;

                    match(input,12,FOLLOW_12_in_term147); 
                     result = expression3; 

                    }
                    break;
                case 3 :
                    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:50:5: 'True'
                    {
                    match(input,13,FOLLOW_13_in_term155); 
                     result = TrueExpression.INSTANCE; 

                    }
                    break;
                case 4 :
                    // C:\\ProDLR\\src\\Examples\\Chapter12\\JavaScripting\\BoolScript\\src\\main\\resources\\BoolScript.g:51:5: 'False'
                    {
                    match(input,14,FOLLOW_14_in_term164); 
                    result = FalseExpression.INSTANCE; 

                    }
                    break;

            }
        }
        catch (RecognitionException re) {
            reportError(re);
            recover(input,re);
        }
        finally {
        }
        return result;
    }
    // $ANTLR end "term"

    // Delegated rules


 

    public static final BitSet FOLLOW_expression_in_program58 = new BitSet(new long[]{0x0000000000000100L});
    public static final BitSet FOLLOW_8_in_program60 = new BitSet(new long[]{0x0000000000006810L});
    public static final BitSet FOLLOW_EOF_in_program66 = new BitSet(new long[]{0x0000000000000002L});
    public static final BitSet FOLLOW_term_in_expression87 = new BitSet(new long[]{0x0000000000000602L});
    public static final BitSet FOLLOW_9_in_expression96 = new BitSet(new long[]{0x0000000000006810L});
    public static final BitSet FOLLOW_term_in_expression100 = new BitSet(new long[]{0x0000000000000602L});
    public static final BitSet FOLLOW_10_in_expression108 = new BitSet(new long[]{0x0000000000006810L});
    public static final BitSet FOLLOW_term_in_expression112 = new BitSet(new long[]{0x0000000000000602L});
    public static final BitSet FOLLOW_IDENT_in_term135 = new BitSet(new long[]{0x0000000000000002L});
    public static final BitSet FOLLOW_11_in_term143 = new BitSet(new long[]{0x0000000000006810L});
    public static final BitSet FOLLOW_expression_in_term145 = new BitSet(new long[]{0x0000000000001000L});
    public static final BitSet FOLLOW_12_in_term147 = new BitSet(new long[]{0x0000000000000002L});
    public static final BitSet FOLLOW_13_in_term155 = new BitSet(new long[]{0x0000000000000002L});
    public static final BitSet FOLLOW_14_in_term164 = new BitSet(new long[]{0x0000000000000002L});

}