/** 
 * CAPL grammar implementation.
 */

grammar Capl;

/* Capl parser */

primaryExpression
    :	Identifier
    |	AccessToSignalIdentifier
    |	SysvarIdentifier
	|	Constant
	|	StringLiteral+
	|	LeftParen expression RightParen
	|	LeftParen compoundStatement RightParen
	| 	( includeBlock
	    	| variableBlock
			| eventBlock
			| timerBlock
			| errorFrame
			| envBlock
			| functionDefinition
			| enumSpecifier
			| structSpecifier
			| startBlock
			| preStartBlock
			| messageBlock
			| multiplexedMessageBlock
			| stopMeasurement
			| diagRequestBlock
			| diagResponseBlock
			| signalBlock
			| sysvarBlock
			| externalDeclaration
		)+
	;

includeBlock
    :	Includes LeftBrace IncludeDirective* RightBrace
    ;

startBlock
    :	On Start LeftBrace blockItemList? RightBrace
    ;

preStartBlock
    :   On PreStart LeftBrace blockItemList? RightBrace
    ;

variableBlock
    :	Variables LeftBrace blockItemList? RightBrace
    ;

eventBlock
    :	On keyEventType LeftBrace blockItemList RightBrace
    ;

timerBlock
    :	On timerType LeftBrace blockItemList RightBrace
    ;

errorFrame
    : 	On ErrorFrame LeftBrace blockItemList? RightBrace
    ;

messageBlock
    :	On messageType LeftBrace blockItemList RightBrace
    ;

multiplexedMessageBlock
    :	On multiplexedMessageType LeftBrace blockItemList RightBrace
    ;

diagRequestBlock
    :	On diagRequestType LeftBrace blockItemList RightBrace
    ;

diagResponseBlock
    :	On diagResponseType LeftBrace blockItemList RightBrace
    ;

signalBlock
    :	On signalType LeftBrace blockItemList RightBrace
    ;

sysvarBlock
    :	On sysvarType LeftBrace blockItemList RightBrace
    ;

stopMeasurement
    :	On StopMeasurement LeftBrace blockItemList RightBrace
    ;

envBlock
    :	On EnvVar Identifier LeftBrace blockItemList RightBrace
    |   On EnvVar LeftParen Identifier RightParen LeftBrace blockItemList RightBrace
    ;

postfixExpression
    :	(primaryExpression
			| LeftParen typeName RightParen LeftBrace initializerList Comma? RightBrace
		)
		(LeftBracket expression RightBracket
			| LeftParen argumentExpressionList? RightParen
			| (PlusPlus | MinusMinus)
		)*
	;

argumentExpressionList
    :	assignmentExpression (Comma assignmentExpression)*
    ;

unaryExpression
    :	(PlusPlus | MinusMinus)* (postfixExpression | unaryOperator castExpression)
    ;

unaryOperator
    :	Plus | Minus | Tilde | Not
    ;

castExpression
    :	LeftParen typeName RightParen castExpression
	|	unaryExpression
	|	DigitSequence // for
	;

multiplicativeExpression
    :	castExpression ((Star | Div | Mod) castExpression)*
    ;

additiveExpression
    :	multiplicativeExpression ((Plus | Minus) multiplicativeExpression)*
	;

shiftExpression
    :	additiveExpression ((LeftShift | RightShift) additiveExpression)*
    ;

relationalExpression
    :	shiftExpression ((Less | Greater | LessEqual | GreaterEqual) shiftExpression)*
    ;

equalityExpression
    :	relationalExpression ((Equal | NotEqual) relationalExpression)*
    ;

andExpression
    :	equalityExpression ( And equalityExpression)*
    ;

exclusiveOrExpression
    :	andExpression (Caret andExpression)*
    ;

inclusiveOrExpression
    :	exclusiveOrExpression (Or exclusiveOrExpression)*
    ;

logicalAndExpression
    :	inclusiveOrExpression (AndAnd inclusiveOrExpression)*
    ;

logicalOrExpression
    :	logicalAndExpression (OrOr logicalAndExpression)*
    ;

conditionalExpression
    :	logicalOrExpression (Question expression Colon conditionalExpression)?
	;

assignmentExpression
    :	conditionalExpression
	|	unaryExpression assignmentOperator assignmentExpression
	|	DigitSequence // for
	;

assignmentOperator
    :	Assign
	|	StarAssign
	|	DivAssign
	|	ModAssign
	|	PlusAssign
	|	MinusAssign
	|	LeftShiftAssign
	|	RightShiftAssign
	|	AndAssign
	|	XorAssign
	|	OrAssign
	;

expression
    :	assignmentExpression (Comma assignmentExpression)*
    ;

constantExpression
    :	conditionalExpression
    ;

declaration
    :	declarationSpecifiers initDeclaratorList? Semi
    ;

declarationSpecifiers
    :	declarationSpecifier+
    ;

declarationSpecifiers2
    :	declarationSpecifier+
    ;

typeQualifier
	:	Const
	;

functionSpecifier
    :	Testfunction
    |	(Export)? Testcase
    ;

declarationSpecifier
    :	typeSpecifier
    |	typeQualifier
    |	functionSpecifier
    ;

initDeclaratorList
    :	initDeclarator (Comma initDeclarator)*
    ;

initDeclarator
    :	declarator (Assign initializer)?
    ;

typeSpecifier
    : 	(Void
	|	Char
	|	Byte
	|	Int
	|	Long
	|	Int64
	|	Float
	|	Double
	|	Word
	|	Dword
	|	Qword
	|	Timer
	|	MsTimer
	|	structSpecifier
	|	enumSpecifier
	|	messageType
	|	multiplexedMessageType
	|	diagRequestType
	|	diagResponseType
	|	signalType
	|	sysvarType)
	;

structSpecifier
    :	structure Identifier? LeftBrace structDeclarationList RightBrace
    |	structure Identifier
    ;

structure
    :	Struct
    ;

structDeclarationList
    :	structDeclaration+
    ;

structDeclaration
    :	specifierQualifierList structDeclaratorList? Semi
    ;

specifierQualifierList
    :	(typeSpecifier | typeQualifier) specifierQualifierList?
    ;

structDeclaratorList
    :	structDeclarator (Comma structDeclarator)*
    ;

structDeclarator
    :	declarator
    |	declarator? Colon constantExpression
    ;

declarator
    :	directDeclarator
    ;

directDeclarator
    :	Identifier
	|	LeftParen declarator RightParen
	|	directDeclarator LeftBracket assignmentExpression? RightBracket
	|	directDeclarator LeftParen parameterTypeList RightParen
	|	directDeclarator LeftParen identifierList? RightParen
	;

nestedParenthesesBlock
    :	(~(LeftParen | RightParen) | LeftParen nestedParenthesesBlock RightParen)*
	;

parameterTypeList
    :	parameterList (Comma Ellipsis)?
    ;

parameterList
    :	parameterDeclaration (Comma parameterDeclaration)*
    ;

parameterDeclaration
    :	declarationSpecifiers declarator
	|	declarationSpecifiers2 abstractDeclarator?
	;

identifierList
    :	Identifier (Comma Identifier)*
    ;

typeName
    :	specifierQualifierList abstractDeclarator?
    ;

abstractDeclarator
    :	directAbstractDeclarator
    ;

directAbstractDeclarator
    :	LeftParen abstractDeclarator RightParen
	|	LeftBracket assignmentExpression? RightBracket
	|	LeftBracket Star RightBracket
	|	LeftParen parameterTypeList? RightParen
	|	directAbstractDeclarator LeftBracket assignmentExpression? RightBracket
	|	directAbstractDeclarator LeftBracket Star RightBracket
	|	directAbstractDeclarator LeftParen parameterTypeList? RightParen
	;

initializer
    :	assignmentExpression
	|	LeftBrace initializerList Comma? RightBrace
	;

initializerList
    :	designation? initializer (Comma designation? initializer)*
    ;

designation
    :	designatorList Assign
    ;

designatorList
    :	designator+
    ;

designator
    :	LeftBracket constantExpression RightBracket
    ;

statement
    :	labeledStatement
	|	compoundStatement
	|	expressionStatement
	|	selectionStatement
	|	iterationStatement
	|	jumpStatement
	;

labeledStatement
    :	Identifier Colon statement
	|	Case (constantExpression) Colon statement
	|	Default Colon statement
	;

compoundStatement
    :	LeftBrace blockItemList? RightBrace
    ;

blockItemList
    :	blockItem+
    ;

blockItem
    :	statement | declaration
    ;

expressionStatement
    :	expression? Semi
    ;

selectionStatement
    :	If LeftParen expression RightParen statement (Else statement)?
	|	Switch LeftParen expression RightParen statement
	;

iterationStatement
    :	While LeftParen expression RightParen statement
	|	Do statement While LeftParen expression RightParen Semi
	|	For LeftParen forCondition RightParen statement
	;

forCondition
    :	(forDeclaration | expression?) Semi forExpression? Semi forExpression?
    ;

forDeclaration
    :	declarationSpecifiers initDeclaratorList?
    ;

forExpression
    :	assignmentExpression (Comma assignmentExpression)*
    ;

jumpStatement
    :	((Continue | Break) | Return expression?) Semi
    ;

compilationUnit
	:	translationUnit? EOF
	;

translationUnit
	:	externalDeclaration+
	;

externalDeclaration
    :	functionDefinition
	|	declaration
	|	Semi
	;

functionDefinition
    :	declarationSpecifiers? declarator declarationList? compoundStatement
    ;

declarationList
	:	declaration+
	;

/* Capl lexer and misc rules */

/* Keywords */
Export : [eE][xX][pP][oO][rR][tT];
Testcase : [tT][eE][sS][tT][cC][aA][sS][eE];
Testfunction : [tT][eE][sS][tT][fF][uU][nN][cC][tT][iI][oO][nN];
Includes : [iI][nN][cC][lL][uU][dD][eE][sS];
Const : [cC][oO][nN][sS][tT];
StopMeasurement : [sS][tT][oO][pP][mM][eE][aA][sS][uU][rR][eE][mM][eE][nN][tT];
Start : [sS][tT][aA][rR][tT];
PreStart : [pP][rR][eE][sS][tT][aA][rR][tT];
ErrorFrame : [eE][rR][rR][oO][rR][fF][rR][aA][mM][eE];
On : [oO][nN];
Variables : [vV][aA][rR][iI][aA][bB][lL][eE][sS];
Break : [bB][rR][eE][aA][kK];
Case : [cC][aA][sS][eE];
Char : [cC][hH][aA][rR];
Byte : [bB][yY][tT][eE];
Continue : [cC][oO][nN][tT][iI][nN][uU][eE];
Default : [dD][eE][fF][aA][uU][lL][tT];
Do : [dD][oO];
Double : [dD][oO][uU][bB][lL][eE];
Else : [eE][lL][sS][eE];
Float : [fF][lL][oO][aA][tT];
For : [fF][oO][rR];
If : [iI][fF];
Int : [iI][nN][tT];
Word : [wW][oO][rR][dD];
Dword : [dD][wW][oO][rR][dD];
Qword : [qQ][wW][oO][rR][dD];
EnvVar : [eE][nN][vV][vV][aA][rR];
MsTimer : [mM][sS][tT][iI][mM][eE][rR];
Long : [lL][oO][nN][gG];
Int64 : [iI][nN][tT][6][4];
Return : [rR][eE][tT][uU][rR][nN];
Switch : [sS][wW][iI][tT][cC][hH];
Void : [vV][oO][iI][dD];
While : [wW][hH][iI][lL][eE];
Struct : [sS][tT][rR][uU][cC][tT];

/* Tokens */
LeftParen : '(';
RightParen : ')';
LeftBracket : '[';
RightBracket : ']';
LessEqual : '<=';
GreaterEqual : '>=';
LeftShift : '<<';
RightShift : '>>';
Plus : '+';
PlusPlus : '++';
MinusMinus : '--';
Div : '/';
Mod : '%';
And : '&';
AndAnd : '&&';
OrOr : '||';
Caret : '^';
Not : '!';
Tilde : '~';
Question : '?';
Colon : ':';
StarAssign : '*=';
DivAssign : '/=';
ModAssign : '%=';
PlusAssign : '+=';
MinusAssign : '-=';
LeftShiftAssign : '<<=';
RightShiftAssign : '>>=';
AndAssign : '&=';
XorAssign : '^=';
OrAssign : '|=';
Equal : '==';
NotEqual : '!=';
Ellipsis : '...';

enumSpecifier
    :   Enum Identifier? LeftBrace enumeratorList Comma? RightBrace Semi?
    |   Enum Identifier
    ;

LeftBrace : '{';
RightBrace : '}';
Semi : ';';

enumeratorList
    :   enumerator (Comma enumerator)*
    ;

Comma : ',';

enumerator
    :   enumerationConstant (Assign constantExpression)?
    ;

Assign : '=';

enumerationConstant
    :   Identifier
    ;

Enum : [eE][nN][uU][mM];

timerType
    :	Timer Identifier (Dot (Identifier | Star))?
    ;

Timer : [Tt][iI][mM][eE][rR];

messageType
    :	Message Identifier (Dot (Identifier | Star))?
	|	Message Star
	|	Message Constant
	|	Message Identifier Minus Identifier
	;

Message : [mM][eE][sS][sS][aA][gG][eE];

multiplexedMessageType
    :	MultiplexedMessage Identifier (Dot (Identifier | Star))?
	|	MultiplexedMessage Star
	|	MultiplexedMessage Constant
	|	MultiplexedMessage Identifier Minus Identifier
	;

MultiplexedMessage : [mM][uU][lL][tT][iI][pP][lL][eE][xX][eE][dD][_][mM][eE][sS][sS][aA][gG][eE];

diagRequestType
    :	DiagRequest Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	DiagRequest Star
	|	DiagRequest Constant
	|	DiagRequest Identifier Minus Identifier
	;

DiagRequest : [dD][iI][aA][gG][rR][eE][qQ][uU][eE][sS][tT];

diagResponseType
    :	DiagResponse Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	DiagResponse Star
	|	DiagResponse Constant
	|	DiagResponse Identifier Minus Identifier
	;

DiagResponse : [dD][iI][aA][gG][rR][eE][sS][pP][oO][nN][sS][eE];

signalType
    :	Signal Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	Signal Star
	|	Signal Constant
	|	Signal Identifier Minus Identifier
	;

Minus : '-';
Signal : [sS][iI][gG][nN][aA][lL];

sysvarType
    :	DoubleSysvar DoubleColon Identifier (DoubleColon Identifier)*
	;

keyEventType
    :	Key Constant
	|	Key (
			F1
		|	F2
		|	F3
		|	F4
		|	F5
		|	F6
		|	F7
		|	F8
		|	F9
		|	F10
		|	F11
		|	F12
		|	CtrlF1
		|	CtrlF2
		|	CtrlF3
		|	CtrlF4
		|	CtrlF5
		|	CtrlF6
		|	CtrlF7
		|	CtrlF8
		|	CtrlF9
		|	CtrlF10
		|	CtrlF11
		|	CtrlF12
		|	PageUp
		|	PageDown
		|	Home)
	|	Key Star
	;

Star : '*';
Key : [kK][eE][yY];
F1 : [fF][1];
F2 : [fF][2];
F3 : [fF][3];
F4 : [fF][4];
F5 : [fF][5];
F6 : [fF][6];
F7 : [fF][7];
F8 : [fF][8];
F9 : [fF][9];
F10 : [fF][1][0];
F11 : [fF][1][1];
F12 : [fF][1][2];
CtrlF1 : [cC][tT][rR][lL][fF][1];
CtrlF2 : [cC][tT][rR][lL][fF][2];
CtrlF3 : [cC][tT][rR][lL][fF][3];
CtrlF4 : [cC][tT][rR][lL][fF][4];
CtrlF5 : [cC][tT][rR][lL][fF][5];
CtrlF6 : [cC][tT][rR][lL][fF][6];
CtrlF7 : [cC][tT][rR][lL][fF][7];
CtrlF8 : [cC][tT][rR][lL][fF][8];
CtrlF9 : [cC][tT][rR][lL][fF][9];
CtrlF10 : [cC][tT][rR][lL][fF][1][0];
CtrlF11 : [cC][tT][rR][lL][fF][1][1];
CtrlF12 : [cC][tT][rR][lL][fF][1][2];
PageUp : [pP][aA][gG][eE][uU][pP];
PageDown : [pP][aA][gG][eE][dD][oO][wW][nN];
Home : [hH][oO][mM][eE];

Identifier
    :	IdentifierNondigit (IdentifierNondigit | Digit)*
	|	((This | IdentifierNondigit) (IdentifierNondigit | Digit)*) Dot Identifier (Dot (Identifier))*
	|	IdentifierNondigit (IdentifierNondigit | Digit)* Dot Constant
	;

This : [tT][hH][iI][sS];

AccessToSignalIdentifier
    :	Dollar Identifier (Phys|Raw|Raw64|Rx|TxRequest)?
    ;

Or : '|';
Dollar : '$';
Phys : [pP][hH][yY][sS];
Raw : [rR][aA][wW];
Raw64 : [rR][aA][wW][6][4];
Rx : [rR][xX];
TxRequest : [tT][xX][rR][qQ];

SysvarIdentifier
    :	AtSign Sysvar? (DoubleColon Identifier) (DoubleColon Identifier)*
    |   AtSign Identifier (DoubleColon Identifier)*
    ;

DoubleColon : '::';
AtSign : '@';
DoubleSysvar : Sysvar Whitespace Sysvar;
Sysvar : [sS][yY][sS][vV][aA][rR];

fragment IdentifierNondigit: Nondigit | UniversalCharacterName;

fragment Nondigit: [a-zA-Z_];

fragment Digit: [0-9];

fragment UniversalCharacterName:
	'\\u' HexQuad
	| '\\U' HexQuad HexQuad;

fragment HexQuad:
	HexadecimalDigit HexadecimalDigit HexadecimalDigit HexadecimalDigit;

Constant
	:	IntegerConstant
	|	FloatingConstant
	|	CharacterConstant
	;

fragment IntegerConstant
	:	DecimalConstant IntegerSuffix?
	|	OctalConstant IntegerSuffix?
	|	HexadecimalConstant IntegerSuffix?
	|	BinaryConstant
	;

fragment BinaryConstant: '0' [bB] [0-1]+;

fragment DecimalConstant: NonzeroDigit Digit*;

fragment OctalConstant: '0' OctalDigit*;

fragment HexadecimalConstant: HexadecimalPrefix HexadecimalDigit+;

fragment HexadecimalPrefix: '0' [xX];

fragment NonzeroDigit: [1-9];

fragment OctalDigit: [0-7];

fragment HexadecimalDigit: [0-9a-fA-F];

fragment IntegerSuffix: LongSuffix | LongLongSuffix;

fragment LongSuffix: [lL];

fragment LongLongSuffix: 'll' | 'LL';

fragment FloatingConstant
	:	DecimalFloatingConstant
	|	HexadecimalFloatingConstant
	;

fragment DecimalFloatingConstant
	:	FractionalConstant ExponentPart? FloatingSuffix?
	|	DigitSequence ExponentPart FloatingSuffix?;

fragment HexadecimalFloatingConstant
	:	HexadecimalPrefix (
			HexadecimalFractionalConstant
			| HexadecimalDigitSequence
		) BinaryExponentPart FloatingSuffix?
	;

fragment FractionalConstant
	:	DigitSequence? Dot DigitSequence
	|	DigitSequence Dot
	;

fragment ExponentPart: [eE] Sign? DigitSequence;

fragment Sign: [+-];

DigitSequence : Digit+;

fragment HexadecimalFractionalConstant
	:	HexadecimalDigitSequence? Dot HexadecimalDigitSequence
	|	HexadecimalDigitSequence Dot
	;

Dot : '.';

fragment BinaryExponentPart: [pP] Sign? DigitSequence;

fragment HexadecimalDigitSequence: HexadecimalDigit+;

fragment FloatingSuffix: [flFL];

fragment CharacterConstant
	:	'\'' CCharSequence '\''
	|	'L\'' CCharSequence '\''
	|	'u\'' CCharSequence '\''
	|	'U\'' CCharSequence '\''
	;

fragment CCharSequence: CChar+;

fragment CChar: ~['\\\r\n] | EscapeSequence;

fragment EscapeSequence
	:	SimpleEscapeSequence
	|	OctalEscapeSequence
	|	HexadecimalEscapeSequence
	|	UniversalCharacterName
	;

fragment SimpleEscapeSequence: '\\' ['"?abfnrtv\\];

fragment OctalEscapeSequence: '\\' OctalDigit OctalDigit? OctalDigit?;

fragment HexadecimalEscapeSequence: '\\x' HexadecimalDigit+;

StringLiteral: EncodingPrefix? '"' SCharSequence? '"';

fragment EncodingPrefix: 'u8' | 'u' | 'U' | 'L';

fragment SCharSequence: SChar+;

fragment SChar
	:	~["\\\r\n]
	|	EscapeSequence
	|	'\\\n' // Added line
	|	'\\\r\n' // Added line
	;

IncludeDirective
	: Hash Whitespace? Include Whitespace? (
		('"' ~[\r\n]* '"')
		| (Less ~[\r\n]* Greater)
	) Whitespace? Newline -> channel(HIDDEN);

Less : '<';
Greater : '>';
Hash : '#';
Include : [iI][nN][cC][lL][uU][dD][eE];

Whitespace : [ \t]+ -> skip;

Newline : ( '\r' '\n'? | '\n') -> skip;

BlockComment : '/*' .*? '*/' -> skip;

LineComment : '//' ~[\r\n]* -> skip;