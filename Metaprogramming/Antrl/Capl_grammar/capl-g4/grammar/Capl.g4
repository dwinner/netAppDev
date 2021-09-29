/** 
 CAPL grammar implementation.
 */

grammar Capl;

/* Capl parser */

primaryExpression: Identifier
	| Constant
	| StringLiteral+
	| '(' expression ')'
	| '(' compoundStatement ')'
	| ( variableBlock
		| eventBlock
		| timerBlock
		| errorFrame
		| envBlock
		| functionDefinition
		| enumSpecifier
		| startBlock
		| messageBlock
		| stopMeasurement
		| diagRequestBlock
		| diagResponseBlock)+
	;

startBlock
    : 'on' 'start' '{' blockItemList? '}'
    ;

variableBlock
    : 'variables' '{' blockItemList? '}'
    ;

eventBlock
    : 'on' keyEventType '{' blockItemList '}'
    ;

timerBlock
    : 'on' 'timer' Identifier ('.' (Identifier | '*'))? '{' blockItemList '}'
    ;

errorFrame
    : 'on' 'errorFrame' '{' blockItemList? '}'
    ;

messageBlock
    : 'on' messageType '{' blockItemList '}'
    ;

diagRequestBlock
    : 'on' diagRequestType '{' blockItemList '}'
    ;

diagResponseBlock
    : 'on' diagResponseType '{' blockItemList '}'
    ;

stopMeasurement
    : 'on' 'stopMeasurement' '{' blockItemList '}'
    ;

envBlock
    : 'on' 'envVar' Identifier '{' blockItemList '}'
    ;

postfixExpression
    : (primaryExpression
		| '(' typeName ')' '{' initializerList ','? '}'
	) ('[' expression ']'
		| '(' argumentExpressionList? ')'
		| ('++' | '--')
	)*
	;

argumentExpressionList
    : assignmentExpression (',' assignmentExpression)*
    ;

unaryExpression
    : ('++' | '--')* (
		postfixExpression
		| unaryOperator castExpression)
    ;

unaryOperator
    : '+' | '-' | '~' | '!'
    ;

castExpression
    : '(' typeName ')' castExpression
	| unaryExpression
	| DigitSequence // for
	;

multiplicativeExpression
    : castExpression (('*' | '/' | '%') castExpression)*
    ;

additiveExpression
    : multiplicativeExpression (
		('+' | '-') multiplicativeExpression
	)*
	;

shiftExpression
    : additiveExpression (('<<' | '>>') additiveExpression)*
    ;

relationalExpression
    : shiftExpression (('<' | '>' | '<=' | '>=') shiftExpression)*
    ;

equalityExpression
    : relationalExpression (('==' | '!=') relationalExpression)*
    ;

andExpression
    : equalityExpression ( '&' equalityExpression)*
    ;

exclusiveOrExpression
    : andExpression ('^' andExpression)*
    ;

inclusiveOrExpression
    : exclusiveOrExpression ('|' exclusiveOrExpression)*
    ;

logicalAndExpression
    : inclusiveOrExpression ('&&' inclusiveOrExpression)*
    ;

logicalOrExpression
    : logicalAndExpression ('||' logicalAndExpression)*
    ;

conditionalExpression
    : logicalOrExpression (
		'?' expression ':' conditionalExpression
	)?
	;

assignmentExpression
    : conditionalExpression
	| unaryExpression assignmentOperator assignmentExpression
	| DigitSequence ; // for

assignmentOperator
    : '='
	| '*='
	| '/='
	| '%='
	| '+='
	| '-='
	| '<<='
	| '>>='
	| '&='
	| '^='
	| '|='
	;

expression
    : assignmentExpression (',' assignmentExpression)*
    ;

constantExpression
    : conditionalExpression
    ;

declaration
    : declarationSpecifiers initDeclaratorList? ';'
    ;

declarationSpecifiers
    : declarationSpecifier+
    ;

declarationSpecifiers2
    : declarationSpecifier+
    ;

declarationSpecifier
    : typeSpecifier
    | typeQualifier
    ;

typeQualifier: 'const';

initDeclaratorList
    : initDeclarator (',' initDeclarator)*
    ;

initDeclarator
    : declarator ('=' initializer)?
    ;

typeSpecifier
    : ('void'
		| 'char'
		| 'byte'
		| 'int'
		| 'long'
		| 'int64'
		| 'float'
		| 'double'
		| 'word'
		| 'dword'
		| 'qword'
		| 'timer'
		| 'msTimer'
		| enumSpecifier
		| messageType
		| diagRequestType
		| diagResponseType)
		;

specifierQualifierList
    : (typeSpecifier | typeQualifier) specifierQualifierList?
    ;

declarator
    : directDeclarator
    ;

directDeclarator
    : Identifier
	| '(' declarator ')'
	| directDeclarator '[' assignmentExpression? ']'
	| directDeclarator '(' parameterTypeList ')'
	| directDeclarator '(' identifierList? ')'
	;

nestedParenthesesBlock
    : (~('(' | ')')
		| '(' nestedParenthesesBlock ')'
	)*
	;

parameterTypeList
    : parameterList (',' '...')?
    ;

parameterList
    : parameterDeclaration (',' parameterDeclaration)*
    ;

parameterDeclaration
    : declarationSpecifiers declarator
	| declarationSpecifiers2 abstractDeclarator?
	;

identifierList
    : Identifier (',' Identifier)*
    ;

typeName
    : specifierQualifierList abstractDeclarator?
    ;

abstractDeclarator
    : directAbstractDeclarator
    ;

directAbstractDeclarator
    : '(' abstractDeclarator ')'
	| '[' assignmentExpression? ']'
	| '[' '*' ']'
	| '(' parameterTypeList? ')'
	| directAbstractDeclarator '[' assignmentExpression? ']'
	| directAbstractDeclarator '[' '*' ']'
	| directAbstractDeclarator '(' parameterTypeList? ')'
	;

initializer
    : assignmentExpression
	| '{' initializerList ','? '}'
	;

initializerList
    : designation? initializer (',' designation? initializer)*
    ;

designation
    : designatorList '='
    ;

designatorList
    : designator+
    ;

designator
    : '[' constantExpression ']'
    ;

statement
    : labeledStatement
	| compoundStatement
	| expressionStatement
	| selectionStatement
	| iterationStatement
	| jumpStatement
	;

labeledStatement
    : Identifier ':' statement
	| 'case' (constantExpression) ':' statement
	| 'default' ':' statement
	;

compoundStatement
    : '{' blockItemList? '}'
    ;

blockItemList
    : blockItem+
    ;

blockItem
    : statement | declaration
    ;

expressionStatement
    : expression? ';'
    ;

selectionStatement
    : 'if' '(' expression ')' statement ('else' statement)?
	| 'switch' '(' expression ')' statement
	;

iterationStatement
    : While '(' expression ')' statement
	| Do statement While '(' expression ')' ';'
	| For '(' forCondition ')' statement
	;

forCondition
    : (forDeclaration | expression?) ';' forExpression? ';' forExpression?
    ;

forDeclaration
    : declarationSpecifiers initDeclaratorList?
    ;

forExpression
    : assignmentExpression (',' assignmentExpression)*
    ;

jumpStatement
    : (('continue' | 'break') | 'return' expression?) ';'
    ;

compilationUnit: translationUnit? EOF;

translationUnit: externalDeclaration+;

externalDeclaration
    : functionDefinition
	| declaration
	| ';' ; // stray ;

functionDefinition
    : declarationSpecifiers? declarator declarationList? compoundStatement
    ;

declarationList: declaration+;

/* Capl lexer */

Const: 'const';
StopMeasurement: 'stopMeasurement';
Start: 'start';
ErrorFrame: 'errorFrame';
On: 'on';
Variables: 'variables';
Break: 'break';
Case: 'case';
Char: 'char';
Byte: 'byte';
Continue: 'continue';
Default: 'default';
Do: 'do';
Double: 'double';
Else: 'else';
Float: 'float';
For: 'for';
If: 'if';
Int: 'int';
Word: 'word';
Dword: 'dword';
EnvVar: 'envVar';
Timer: 'timer';
MsTimer: 'msTimer';
Long: 'long';
Int64: 'int64';
Return: 'return';
Switch: 'switch';
Void: 'void';
While: 'while';
LeftParen: '(';
RightParen: ')';
LeftBracket: '[';
RightBracket: ']';
LeftBrace: '{';
RightBrace: '}';
Less: '<';
LessEqual: '<=';
Greater: '>';
GreaterEqual: '>=';
LeftShift: '<<';
RightShift: '>>';
Plus: '+';
PlusPlus: '++';
Minus: '-';
MinusMinus: '--';
Div: '/';
Mod: '%';
And: '&';
Or: '|';
AndAnd: '&&';
OrOr: '||';
Caret: '^';
Not: '!';
Tilde: '~';
Question: '?';
Colon: ':';
Semi: ';';
Comma: ',';
Assign: '=';
StarAssign: '*=';
DivAssign: '/=';
ModAssign: '%=';
PlusAssign: '+=';
MinusAssign: '-=';
LeftShiftAssign: '<<=';
RightShiftAssign: '>>=';
AndAssign: '&=';
XorAssign: '^=';
OrAssign: '|=';
Star: '*';
Equal: '==';
NotEqual: '!=';
Ellipsis: '...';

enumSpecifier
    :   'enum' Identifier? '{' enumeratorList ','? '}' ';'?
    |   'enum' Identifier
    ;

enumeratorList
    :   enumerator (',' enumerator)*
    ;

enumerator
    :   enumerationConstant ('=' constantExpression)?
    ;

enumerationConstant
    :   Identifier
    ;

Enum : 'enum';

messageType
    : 'message' Identifier ('.' (Identifier | '*'))?
	| 'message' '*'
	| 'message' Constant
	| 'message' Identifier '-' Identifier
	;

Message: 'message';

diagRequestType
    : 'diagRequest' Identifier (('.'|'::') (Identifier | '*'))?
	| 'diagRequest' '*'
	| 'diagRequest' Constant
	| 'diagRequest' Identifier '-' Identifier
	;

DiagRequest: 'diagRequest';

diagResponseType
    : 'diagResponse' Identifier (('.'|'::') (Identifier | '*'))?
	| 'diagResponse' '*'
	| 'diagResponse' Constant
	| 'diagResponse' Identifier '-' Identifier
	;

DiagResponse: 'diagResponse';
DoubleColon: '::';

keyEventType
    : 'key' Constant
	| 'key' (
		'F1'
		| 'F2'
		| 'F3'
		| 'F4'
		| 'F5'
		| 'F6'
		| 'F7'
		| 'F8'
		| 'F9'
		| 'F10'
		| 'F11'
		| 'F12'
	)
	| 'key' '*'
	;

Key: 'key';
F1: 'F1';
F2: 'F2';
F3: 'F3';
F4: 'F4';
F5: 'F5';
F6: 'F6';
F7: 'F7';
F8: 'F8';
F9: 'F9';
F10: 'F10';
F11: 'F11';
F12: 'F12';

Identifier
    : IdentifierNondigit (IdentifierNondigit | Digit)*
	| ('this' | IdentifierNondigit (IdentifierNondigit | Digit)*) '.' Identifier (
		'.' (Identifier)
	)*
	| IdentifierNondigit (IdentifierNondigit | Digit)* '.' Constant
	;

This: 'this';
Dot: '.';

fragment IdentifierNondigit: Nondigit | UniversalCharacterName;

fragment Nondigit: [a-zA-Z_];

fragment Digit: [0-9];

fragment UniversalCharacterName:
	'\\u' HexQuad
	| '\\U' HexQuad HexQuad;

fragment HexQuad:
	HexadecimalDigit HexadecimalDigit HexadecimalDigit HexadecimalDigit;

Constant:
	IntegerConstant
	| FloatingConstant
	| CharacterConstant;

fragment IntegerConstant:
	DecimalConstant IntegerSuffix?
	| OctalConstant IntegerSuffix?
	| HexadecimalConstant IntegerSuffix?
	| BinaryConstant;

fragment BinaryConstant: '0' [bB] [0-1]+;

fragment DecimalConstant: NonzeroDigit Digit*;

fragment OctalConstant: '0' OctalDigit*;

fragment HexadecimalConstant:
	HexadecimalPrefix HexadecimalDigit+;

fragment HexadecimalPrefix: '0' [xX];

fragment NonzeroDigit: [1-9];

fragment OctalDigit: [0-7];

fragment HexadecimalDigit: [0-9a-fA-F];

fragment IntegerSuffix: LongSuffix | LongLongSuffix;

fragment LongSuffix: [lL];

fragment LongLongSuffix: 'll' | 'LL';

fragment FloatingConstant:
	DecimalFloatingConstant
	| HexadecimalFloatingConstant;

fragment DecimalFloatingConstant:
	FractionalConstant ExponentPart? FloatingSuffix?
	| DigitSequence ExponentPart FloatingSuffix?;

fragment HexadecimalFloatingConstant:
	HexadecimalPrefix (
		HexadecimalFractionalConstant
		| HexadecimalDigitSequence
	) BinaryExponentPart FloatingSuffix?;

fragment FractionalConstant:
	DigitSequence? '.' DigitSequence
	| DigitSequence '.';

fragment ExponentPart: [eE] Sign? DigitSequence;

fragment Sign: [+-];

DigitSequence: Digit+;

fragment HexadecimalFractionalConstant:
	HexadecimalDigitSequence? '.' HexadecimalDigitSequence
	| HexadecimalDigitSequence '.';

fragment BinaryExponentPart: [pP] Sign? DigitSequence;

fragment HexadecimalDigitSequence: HexadecimalDigit+;

fragment FloatingSuffix: [flFL];

fragment CharacterConstant:
	'\'' CCharSequence '\''
	| 'L\'' CCharSequence '\''
	| 'u\'' CCharSequence '\''
	| 'U\'' CCharSequence '\'';

fragment CCharSequence: CChar+;

fragment CChar: ~['\\\r\n] | EscapeSequence;

fragment EscapeSequence:
	SimpleEscapeSequence
	| OctalEscapeSequence
	| HexadecimalEscapeSequence
	| UniversalCharacterName;

fragment SimpleEscapeSequence: '\\' ['"?abfnrtv\\];

fragment OctalEscapeSequence:
	'\\' OctalDigit OctalDigit? OctalDigit?;

fragment HexadecimalEscapeSequence: '\\x' HexadecimalDigit+;

StringLiteral: EncodingPrefix? '"' SCharSequence? '"';

fragment EncodingPrefix: 'u8' | 'u' | 'U' | 'L';

fragment SCharSequence: SChar+;

fragment SChar:
	~["\\\r\n]
	| EscapeSequence
	| '\\\n' // Added line
	| '\\\r\n'; // Added line

IncludeDirective:
	'#' Whitespace? 'include' Whitespace? (
		('"' ~[\r\n]* '"')
		| ('<' ~[\r\n]* '>')
	) Whitespace? Newline -> skip;

Whitespace: [ \t]+ -> skip;

Newline: ( '\r' '\n'? | '\n') -> skip;

BlockComment: '/*' .*? '*/' -> skip;

LineComment: '//' ~[\r\n]* -> skip;