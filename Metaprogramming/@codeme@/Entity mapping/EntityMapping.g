grammar EntityMapping;

options{
  output=AST;
  language=CSharp3;
  ASTLabelType=CommonTree;
}

tokens{  
  ENTMAP;
  MAPDCL;
  MAPBODY;
  ASSIGN;
  GETMEMBER;
  CALL;
  COND;
  TFEXPR;
}

@lexer::namespace { EntMapping }
@parser::namespace { EntMapping }

@header {
   using System;
   using System.Collections;
}

public prog : mapDeclaration stat* -> ^(ENTMAP mapDeclaration ^(MAPBODY stat*) )  ;

mapDeclaration	: ID '<-' ID ';' -> ^(MAPDCL ID ID) ;

stat : ID '=' expr ';' -> ^(ASSIGN ID expr);

expr	:	boolAndExpr (OR^ boolAndExpr)*;

boolAndExpr :	equalityExpr (AND^ equalityExpr)*;

equalityExpr :	relationalExpr ((EQUALS^|NOTEQUALS^)relationalExpr)*;

relationalExpr : additiveExpr ((LT^|LTEQ^|GT^|GTEQ^)additiveExpr)*	;

additiveExpr : multiExpr ( (PLUS^|MINUS^) multiExpr )*;

multiExpr : unaryExpr ( (MULT^|DIV^|MOD^) unaryExpr)*;

unaryExpr : (NOT^|INC^|DEC^|) atom;

atom : 	value
	| ID
	| functionCall
	| ifExpr 
	| memberAccess 
	| '(' expr ')' ;

value	:	INT | FLOAT | STRING |CHAR;

memberAccess :	ID '.' ID -> ^(GETMEMBER ID ID);

ifExpr 	:	'IF''(' ce=expr ',' e1=expr ',' e2=expr ')' -> ^('IF' ^(COND $ce) ^(TFEXPR $e1 $e2));

functionCall : ID '(' (params)? ')' -> ^(CALL ID params*) ;

params 	: expr (',' expr )*;


ID  :	('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')*
    ;

INT :	'0'..'9'+
    ;

FLOAT
    :   ('0'..'9')+ '.' ('0'..'9')* EXPONENT?
    |   '.' ('0'..'9')+ EXPONENT?
    |   ('0'..'9')+ EXPONENT
    ;

COMMENT
    :   '//' ~('\n'|'\r')* '\r'? '\n' { Skip();}
    |   '/*' ( options {greedy=false;} : . )* '*/' { Skip();}
    ;

WS  :   ( ' '
        | '\t'
        | '\r'
        | '\n'
        ) { Skip();}
    ;
    

STRING
    :  '"' ( ESC_SEQ | ~('\\'|'"') )* '"'
    ;

CHAR:  '\'' ( ESC_SEQ | ~('\''|'\\') ) '\''
    ;

fragment
EXPONENT : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;

fragment
HEX_DIGIT : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
ESC_SEQ
    :   '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    |   UNICODE_ESC
    |   OCTAL_ESC
    ;

fragment
OCTAL_ESC
    :   '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')
    ;

fragment
UNICODE_ESC
    :   '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
    ;
    


OR    :     '||' | 'or';
AND   :     '&&' | 'and';
EQUALS
      :     '==' | '=' | 'eq';
NOTEQUALS
      :    '!=' | '<>' | 'ne';
LT    :    '<';
LTEQ  :    '<=';
GT    :    '>';
GTEQ  :    '>=';
PLUS  :    '+';
MINUS :    '-';
MULT  :    '*';
DIV   :    '/';
MOD   :    '%';
NOT   :    '!' | 'not';
INC   :	   '++';
DEC   :    '--';

