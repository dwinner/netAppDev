// $ANTLR 3.1.2 C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g 2010-08-04 14:06:48

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162
namespace  Stitch 
{

using System.Collections.Generic;
using Stitch.Ast;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class StitchParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"IDENT", 
		"CODEBLOCK", 
		"FILEPATH", 
		"LETTER", 
		"DIGIT", 
		"WS", 
		"'<'", 
		"'('", 
		"')'", 
		"'return('", 
		"')>'", 
		"','", 
		"'include'"
    };

    public const int WS = 9;
    public const int T__16 = 16;
    public const int T__15 = 15;
    public const int T__12 = 12;
    public const int T__11 = 11;
    public const int LETTER = 7;
    public const int T__14 = 14;
    public const int T__13 = 13;
    public const int T__10 = 10;
    public const int FILEPATH = 6;
    public const int IDENT = 4;
    public const int DIGIT = 8;
    public const int EOF = -1;
    public const int CODEBLOCK = 5;

    // delegates
    // delegators



        public StitchParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public StitchParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();

             
        }
        

    override public string[] TokenNames {
		get { return StitchParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g"; }
    }



    // $ANTLR start "program"
    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:30:1: program returns [IList<IFunction> result] : ( func )* ;
    public IList<IFunction> program() // throws RecognitionException [1]
    {   
        IList<IFunction> result = default(IList<IFunction>);

        IFunction func1 = default(IFunction);


        try 
    	{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:31:4: ( ( func )* )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:31:6: ( func )*
            {
            	 result = new List<IFunction>(); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:32:4: ( func )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == 10) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:32:5: func
            			    {
            			    	PushFollow(FOLLOW_func_in_program55);
            			    	func1 = func();
            			    	state.followingStackPointer--;

            			    	 result.Add(func1); 

            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "program"


    // $ANTLR start "func"
    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:35:1: func returns [IFunction result] : '<' IDENT '(' (inputArguments= parameters )? ')' funcCode 'return(' (returnValues= parameters )? ')>' ;
    public IFunction func() // throws RecognitionException [1]
    {   
        IFunction result = default(IFunction);

        IToken IDENT2 = null;
        IList<String> inputArguments = default(IList<String>);

        IList<String> returnValues = default(IList<String>);

        IFunctionCode funcCode3 = default(IFunctionCode);


        try 
    	{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:36:4: ( '<' IDENT '(' (inputArguments= parameters )? ')' funcCode 'return(' (returnValues= parameters )? ')>' )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:36:6: '<' IDENT '(' (inputArguments= parameters )? ')' funcCode 'return(' (returnValues= parameters )? ')>'
            {
            	Match(input,10,FOLLOW_10_in_func79); 
            	IDENT2=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_func81); 
            	Match(input,11,FOLLOW_11_in_func83); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:36:34: (inputArguments= parameters )?
            	int alt2 = 2;
            	int LA2_0 = input.LA(1);

            	if ( (LA2_0 == IDENT) )
            	{
            	    alt2 = 1;
            	}
            	switch (alt2) 
            	{
            	    case 1 :
            	        // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:36:34: inputArguments= parameters
            	        {
            	        	PushFollow(FOLLOW_parameters_in_func87);
            	        	inputArguments = parameters();
            	        	state.followingStackPointer--;


            	        }
            	        break;

            	}

            	Match(input,12,FOLLOW_12_in_func91); 
            	PushFollow(FOLLOW_funcCode_in_func93);
            	funcCode3 = funcCode();
            	state.followingStackPointer--;

            	Match(input,13,FOLLOW_13_in_func100); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:37:26: (returnValues= parameters )?
            	int alt3 = 2;
            	int LA3_0 = input.LA(1);

            	if ( (LA3_0 == IDENT) )
            	{
            	    alt3 = 1;
            	}
            	switch (alt3) 
            	{
            	    case 1 :
            	        // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:37:26: returnValues= parameters
            	        {
            	        	PushFollow(FOLLOW_parameters_in_func104);
            	        	returnValues = parameters();
            	        	state.followingStackPointer--;


            	        }
            	        break;

            	}

            	Match(input,14,FOLLOW_14_in_func108); 
            	 
            	     FunctionSignature signature = new FunctionSignature(IDENT2.Text,
            	              inputArguments, returnValues);
            	     result =  new Function(signature, funcCode3);
            	   

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "func"


    // $ANTLR start "parameters"
    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:44:1: parameters returns [IList<String> result] : firstParameter= IDENT ( ',' nextParameter= IDENT )* ;
    public IList<String> parameters() // throws RecognitionException [1]
    {   
        IList<String> result = default(IList<String>);

        IToken firstParameter = null;
        IToken nextParameter = null;

        try 
    	{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:45:5: (firstParameter= IDENT ( ',' nextParameter= IDENT )* )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:45:7: firstParameter= IDENT ( ',' nextParameter= IDENT )*
            {
            	 result =  new List<String>(); 
            	firstParameter=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_parameters149); 
            	 result.Add(firstParameter.Text); 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:47:6: ( ',' nextParameter= IDENT )*
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( (LA4_0 == 15) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:47:7: ',' nextParameter= IDENT
            			    {
            			    	Match(input,15,FOLLOW_15_in_parameters159); 
            			    	nextParameter=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_parameters163); 
            			    	 result.Add(nextParameter.Text); 

            			    }
            			    break;

            			default:
            			    goto loop4;
            	    }
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "parameters"


    // $ANTLR start "funcCode"
    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:49:1: funcCode returns [IFunctionCode result] : ( include | IDENT ) CODEBLOCK ;
    public IFunctionCode funcCode() // throws RecognitionException [1]
    {   
        IFunctionCode result = default(IFunctionCode);

        IToken CODEBLOCK5 = null;
        IToken IDENT6 = null;
        String include4 = default(String);


        try 
    	{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:50:5: ( ( include | IDENT ) CODEBLOCK )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:50:7: ( include | IDENT ) CODEBLOCK
            {
            	 bool isInclude = false; 
            	// C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:51:5: ( include | IDENT )
            	int alt5 = 2;
            	int LA5_0 = input.LA(1);

            	if ( (LA5_0 == 16) )
            	{
            	    alt5 = 1;
            	}
            	else if ( (LA5_0 == IDENT) )
            	{
            	    alt5 = 2;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d5s0 =
            	        new NoViableAltException("", 5, 0, input);

            	    throw nvae_d5s0;
            	}
            	switch (alt5) 
            	{
            	    case 1 :
            	        // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:51:6: include
            	        {
            	        	PushFollow(FOLLOW_include_in_funcCode192);
            	        	include4 = include();
            	        	state.followingStackPointer--;

            	        	 isInclude = true; 

            	        }
            	        break;
            	    case 2 :
            	        // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:52:7: IDENT
            	        {
            	        	IDENT6=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_funcCode202); 

            	        }
            	        break;

            	}

            	CODEBLOCK5=(IToken)Match(input,CODEBLOCK,FOLLOW_CODEBLOCK_in_funcCode210); 

            		     if (isInclude)
            		        result =  new IncludedCode(include4);
            		     else {  
            		       String codeBlock = CODEBLOCK5.Text;
            		       int i = codeBlock.IndexOf('>');
            		       int j = codeBlock.LastIndexOf('<');
            		       String code = codeBlock.Substring(i + 1, j - i -1);
            		       result =  new BlockCode(IDENT6.Text, code);
            		     }
            	    

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "funcCode"


    // $ANTLR start "include"
    // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:66:1: include returns [String result] : 'include' FILEPATH ;
    public String include() // throws RecognitionException [1]
    {   
        String result = default(String);

        IToken FILEPATH7 = null;

        try 
    	{
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:67:5: ( 'include' FILEPATH )
            // C:\\ProDLR\\src\\Examples\\Chapter9\\Eclipse\\Stitch\\src\\Stitch.g:67:7: 'include' FILEPATH
            {
            	Match(input,16,FOLLOW_16_in_include236); 
            	FILEPATH7=(IToken)Match(input,FILEPATH,FOLLOW_FILEPATH_in_include238); 
            	 result =  FILEPATH7.Text; 

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "include"

    // Delegated rules


	private void InitializeCyclicDFAs()
	{
	}

 

    public static readonly BitSet FOLLOW_func_in_program55 = new BitSet(new ulong[]{0x0000000000000402UL});
    public static readonly BitSet FOLLOW_10_in_func79 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_IDENT_in_func81 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_11_in_func83 = new BitSet(new ulong[]{0x0000000000001010UL});
    public static readonly BitSet FOLLOW_parameters_in_func87 = new BitSet(new ulong[]{0x0000000000001000UL});
    public static readonly BitSet FOLLOW_12_in_func91 = new BitSet(new ulong[]{0x0000000000010010UL});
    public static readonly BitSet FOLLOW_funcCode_in_func93 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_13_in_func100 = new BitSet(new ulong[]{0x0000000000004010UL});
    public static readonly BitSet FOLLOW_parameters_in_func104 = new BitSet(new ulong[]{0x0000000000004000UL});
    public static readonly BitSet FOLLOW_14_in_func108 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_parameters149 = new BitSet(new ulong[]{0x0000000000008002UL});
    public static readonly BitSet FOLLOW_15_in_parameters159 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_IDENT_in_parameters163 = new BitSet(new ulong[]{0x0000000000008002UL});
    public static readonly BitSet FOLLOW_include_in_funcCode192 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_IDENT_in_funcCode202 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_CODEBLOCK_in_funcCode210 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_16_in_include236 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_FILEPATH_in_include238 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}