/** 
 CAPL grammar based on C programming language spec.
 */

grammar Capl;

compilationUnit: variableSection*;

variableSection:
	'variables' LEFT_BLOCK_PAREN assignmentSet RIGHT_BLOCK_PAREN;

assignmentSet: (assignmentExpr)*;

assignmentExpr: type ID ('=' expr)? ';';

type: 'int' | 'float';

expr : // :   expr '^'<assoc=right> expr  // ^ operator is right associative
	expr (MUL | DIV) expr
	| expr (PLUS | MINUS) expr
	| LEFT_PAREN expr RIGHT_PAREN
	| ID
	| INT
	| FLOAT;

FLOAT: DIGIT+ ('.' DIGIT*)?;

STRING: '"' (ESC | .)*? '"';

fragment ESC: '\\' [btnr"\\];

INT: DIGIT+;

fragment DIGIT: [0-9];

ENUM: 'enum';

FOR: 'for';

ID: ID_LETTER (ID_LETTER | DIGIT)*;

fragment ID_LETTER: 'a' ..'z' | 'A' ..'Z' | '_';

MUL: '*';

DIV: '/';

PLUS: '+';

MINUS: '-';

LINE_COMMENT: '//' .*? '\r'? '\n' -> skip;

COMMENT: '/*' .*? '*/' -> skip;

LEFT_PAREN: '(';

RIGHT_PAREN: ')';

LEFT_BLOCK_PAREN: '{';

RIGHT_BLOCK_PAREN: '}';

WS: [ \t\r\n]+ -> skip;