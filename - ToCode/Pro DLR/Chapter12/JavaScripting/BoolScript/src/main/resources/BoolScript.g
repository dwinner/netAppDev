grammar BoolScript;

options {
  language = Java;
}

@header {
  package boolscript.parser;
  import java.util.List;
  import java.util.ArrayList;
  import boolscript.ast.BoolExpression;
  import boolscript.ast.AndExpression;
  import boolscript.ast.OrExpression;
  import boolscript.ast.VarExpression;
  import boolscript.ast.TrueExpression;
  import boolscript.ast.FalseExpression;
}

@lexer::header {
  package boolscript.parser;
}
@members {
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
}
 
program returns [List<BoolExpression> result] 
  : { $result = new ArrayList<BoolExpression>(); } 
  (expression ';' { $result.add($expression.result); })* EOF ; 

 
expression returns [BoolExpression result] 
  : t1=term { $result = $t1.result;} 
  ( '&' t2=term { $result = new AndExpression($result, $t2.result); }
  | '|' t2=term { $result = new OrExpression($result, $t2.result); }
   )*;

term returns [BoolExpression result] 
  : IDENT { $result = new VarExpression($IDENT.text); }
  | '(' expression ')' { $result = $expression.result; }
  | 'True' { $result = TrueExpression.INSTANCE; } 
  | 'False' {$result = FalseExpression.INSTANCE; }; 
    
fragment LETTER : ('a'..'z' | 'A'..'Z') ;
fragment DIGIT : '0'..'9';
IDENT : LETTER (LETTER | DIGIT)*; 
WS : (' ' | '\t' | '\n' | '\r' | '\f')+ {$channel = HIDDEN;};
