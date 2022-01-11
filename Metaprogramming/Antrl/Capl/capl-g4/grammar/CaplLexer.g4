lexer grammar CaplLexer;

/* Lexer rules */

StringLiteral: EncodingPrefix? '"' SCharSequence? '"';
DigitSequence : Digit+;
Constant
	:	IntegerConstant
	|	FloatingConstant
	|	CharacterConstant
	;
DoubleSysvar : Sysvar Whitespace Sysvar;
SysvarIdentifier
    :	AtSign Sysvar? (DoubleColon Identifier) (DoubleColon Identifier)*
    |   AtSign Identifier (DoubleColon Identifier)*
    ;
AccessToSignalIdentifier
    :	Dollar Identifier (Phys|Raw|Raw64|Rx|TxRq)?
    ;
Identifier
    :	IdentifierNondigit (IdentifierNondigit | Digit)*
    |	((This | IdentifierNondigit) (IdentifierNondigit | Digit)*) Dot Identifier (Dot (Identifier))*
    |	IdentifierNondigit (IdentifierNondigit | Digit)* Dot Constant
    ;
IncludeDirective
    : Hash Whitespace? Include Whitespace? (
        ('"' ~[\r\n]* '"')
        | (Less ~[\r\n]* Greater)
    ) Whitespace? Newline -> channel(HIDDEN)
    ;

/* Fragments */
fragment IdentifierNondigit: Nondigit | UniversalCharacterName;

fragment Nondigit: [a-zA-Z_];

fragment Digit: [0-9];

fragment UniversalCharacterName:
	'\\u' HexQuad
	| '\\U' HexQuad HexQuad;

fragment HexQuad:
	HexadecimalDigit HexadecimalDigit HexadecimalDigit HexadecimalDigit;

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

fragment HexadecimalFractionalConstant
	:	HexadecimalDigitSequence? Dot HexadecimalDigitSequence
	|	HexadecimalDigitSequence Dot
	;

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

fragment EncodingPrefix: 'u8' | 'u' | 'U' | 'L';

fragment SCharSequence: SChar+;

fragment SChar
	:	~["\\\r\n]
	|	EscapeSequence
	|	'\\\n' // Added line
	|	'\\\r\n' // Added line
	;

/* Lexer tokens */
Less : '<';
Greater : '>';
Hash : '#';
Dot : '.';
DoubleColon : '::';
AtSign : '@';
Or : '|';
Dollar : '$';
Star : '*';
Minus : '-';
Assign : '=';
Comma : ',';
LeftBrace : '{';
RightBrace : '}';
Semi : ';';
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

/* Lexer keywords (case insensitive) */
Include : [iI][nN][cC][lL][uU][dD][eE];
Sysvar : [sS][yY][sS][vV][aA][rR];
Phys : [pP][hH][yY][sS];
Raw : [rR][aA][wW];
Raw64 : [rR][aA][wW][6][4];
Rx : [rR][xX];
TxRq : [tT][xX][rR][qQ];
This : [tT][hH][iI][sS];
Key : [kK][eE][yY];
Signal : [sS][iI][gG][nN][aA][lL];
DiagRequest : [dD][iI][aA][gG][rR][eE][qQ][uU][eE][sS][tT];
DiagResponse : [dD][iI][aA][gG][rR][eE][sS][pP][oO][nN][sS][eE];
MultiplexedMessage : [mM][uU][lL][tT][iI][pP][lL][eE][xX][eE][dD][_][mM][eE][sS][sS][aA][gG][eE];
Message : [mM][eE][sS][sS][aA][gG][eE];
Timer : [Tt][iI][mM][eE][rR];
Enum : [eE][nN][uU][mM];
Export : [eE][xX][pP][oO][rR][tT];
Testcase : [tT][eE][sS][tT][cC][aA][sS][eE];
Testfunction : [tT][eE][sS][tT][fF][uU][nN][cC][tT][iI][oO][nN];
Includes : [iI][nN][cC][lL][uU][dD][eE][sS];
Const : [cC][oO][nN][sS][tT];
StopMeasurement : [sS][tT][oO][pP][mM][eE][aA][sS][uU][rR][eE][mM][eE][nN][tT];
Start : [sS][tT][aA][rR][tT];
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

/* Keyboard shortcut notations */
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

/* Skip comments and whitespaces */
Whitespace : [ \t]+ -> skip;
Newline : ( '\r' '\n'? | '\n') -> skip;
BlockComment : '/*' .*? '*/' -> skip;
LineComment : '//' ~[\r\n]* -> skip;