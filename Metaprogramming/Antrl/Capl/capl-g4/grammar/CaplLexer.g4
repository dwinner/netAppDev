lexer grammar CaplLexer;

/* Lexer rules */
KeyConstants
    : ( F1Key
	|	F2Key
	|	F3Key
	|	F4Key
	|	F5Key
	|	F6Key
	|	F7Key
	|	F8Key
	|	F9Key
	|	F10Key
	|	F11Key
	|	F12Key
	|	CtrlF1Key
	|	CtrlF2Key
	|	CtrlF3Key
	|	CtrlF4Key
	|	CtrlF5Key
	|	CtrlF6Key
	|	CtrlF7Key
	|	CtrlF8Key
	|	CtrlF9Key
	|	CtrlF10Key
	|	CtrlF11Key
	|	CtrlF12Key
	|	PageUpKey
	|	PageDownKey
	|	HomeKey
	|   EndKey
	|   CursorUp
	|   CursorDown
	|   CursorRight
	|   CursorLeft
	|   CtrlCursorUp
	|   CtrlCursorDown
	|   CtrlCursorRight
	|   CtrlCursorLeft
	)
	;

Identifier
    :	SimpleId
	|	DotThisId
	|	DotConstId
	|   DoubleColonId
	|   SysVarId
	|   ArrayAccessId
	|   ByteAccessIndexerId
	;

ByteAccessIndexerId
    :   Byte LeftParen DigitSequence RightParen
    ;

ArrayAccessId
    :   (SimpleId|SysvarIdentifier) Whitespace?
        (LeftBracket Whitespace? (SimpleId|DigitSequence|SysvarIdentifier) Whitespace? RightBracket)+
        (Whitespace? Dot Whitespace? (SimpleId|SysvarIdentifier))*
    ;

DoubleColonId
    :   (IdentifierNondigit (IdentifierNondigit | Digit)*) (DoubleColon (IdentifierNondigit (IdentifierNondigit | Digit)*))*
    ;

DotConstId
    :   IdentifierNondigit (IdentifierNondigit | Digit)* Dot Constant
    ;

DotThisId
    :   ((This | IdentifierNondigit) (IdentifierNondigit | Digit)*) Whitespace? Dot Whitespace? Identifier (Dot (Identifier))*
    ;

SimpleId
    :   IdentifierNondigit (IdentifierNondigit | Digit)*
    ;

SysVarId
    :   Sysvar (DoubleColon IdentifierNondigit (IdentifierNondigit | Digit)*)+
    ;

AccessToSignalIdentifier
    :	Dollar Whitespace? Identifier (Phys|Raw|Raw64|Rx|TxRequest)?
    ;

SysvarIdentifier
    :	AtSign Whitespace? Sysvar? Whitespace? /*(DoubleColon Whitespace? Identifier)*/ (DoubleColon Whitespace? Identifier)*
    |   AtSign Whitespace? Identifier Whitespace? (DoubleColon Whitespace? Identifier)*
    ;

Constant
	:	IntegerConstant
	|	FloatingConstant
	|	CharacterConstant
	|   MessageHexConst
	;

MessageHexConst : HexadecimalPrefix HexadecimalDigitSequence ('x')?;

DigitSequence : Digit+;

StringLiteral: EncodingPrefix? '"' SCharSequence? '"';

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
Arrow : '->' ;
LessEqual : '<=';
GreaterEqual : '>=';
LeftShift : '<<';
RightShift : '>>';
Plus : '+';
PlusPlus : '++';
MinusMinus : '--';
Div : '/';
Mod : '%';
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
LeftBrace : '{';
RightBrace : '}';
Semi : ';';
Comma : ',';
Assign : '=';
Minus : '-';
Star : '*';
And : '&';
LeftParen : '(';
RightParen : ')';
LeftBracket : '[';
RightBracket : ']';
Or : '|';
Dollar : '$';
DoubleColon : '::';
AtSign : '@';
Dot : '.';
Less : '<';
Greater : '>';
Hash : '#';

/* Lexer keywords (case insensitive) */
//SysvarAlt : [sS][yY][sS][vV][aA][rR];
Sysvar : [sS][yY][sS][vV][aA][rR];
Export : [eE][xX][pP][oO][rR][tT];
Testcase : [tT][eE][sS][tT][cC][aA][sS][eE];
Testfunction : [tT][eE][sS][tT][fF][uU][nN][cC][tT][iI][oO][nN];
Includes : [iI][nN][cC][lL][uU][dD][eE][sS];
Const : [cC][oO][nN][sS][tT];
StopMeasurement : [sS][tT][oO][pP][mM][eE][aA][sS][uU][rR][eE][mM][eE][nN][tT];
SysvarUpdate : [sS][yY][sS][vV][aA][rR][_][uU][pP][dD][aA][tT][eE];
EthernetPacket : [eE][tT][hH][eE][rR][nN][eE][tT][pP][aA][cC][kK][eE][tT];
EthernetStatus : [eE][tT][hH][eE][rR][nN][eE][tT][sS][tT][aA][tT][uU][sS];
MostAmsMessage : [mM][oO][sS][tT][aA][mM][sS][mM][eE][sS][sS][aA][gG][eE];
MostMessage : [mM][oO][sS][tT][mM][eE][sS][sS][aA][gG][eE];
Start : [sS][tT][aA][rR][tT];
BusOn : [bB][uU][sS][oO][nN];
BusOff : [bB][uU][sS][oO][fF][fF];
PreStart : [pP][rR][eE][sS][tT][aA][rR][tT];
PreStop : [pP][rR][eE][sS][tT][oO][pP];
ErrorFrame : [eE][rR][rR][oO][rR][fF][rR][aA][mM][eE];
ErrorActive : [eE][rR][rR][oO][rR][aA][cC][tT][iI][vV][eE];
ErrorPassive : [eE][rR][rR][oO][rR][pP][aA][sS][sS][iI][vV][eE];
On : [oO][nN];
Variables : [vV][aA][rR][iI][aA][bB][lL][eE][sS];
Break : [bB][rR][eE][aA][kK];
Case : [cC][aA][sS][eE];
Char : [cC][hH][aA][rR];
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
Enum : [eE][nN][uU][mM];
Timer : [Tt][iI][mM][eE][rR];
Message : [mM][eE][sS][sS][aA][gG][eE];
MultiplexedMessage : [mM][uU][lL][tT][iI][pP][lL][eE][xX][eE][dD][_][mM][eE][sS][sS][aA][gG][eE];
DiagRequest : [dD][iI][aA][gG][rR][eE][qQ][uU][eE][sS][tT];
DiagResponse : [dD][iI][aA][gG][rR][eE][sS][pP][oO][nN][sS][eE];
Signal : [sS][iI][gG][nN][aA][lL];
Key : [kK][eE][yY];
Byte : [bB][yY][tT][eE];
This : [tT][hH][iI][sS];
Phys : [pP][hH][yY][sS];
Raw : [rR][aA][wW];
Raw64 : [rR][aA][wW][6][4];
Rx : [rR][xX];
TxRequest : [tT][xX][rR][qQ];
Include : [iI][nN][cC][lL][uU][dD][eE];

/* _align keyword with variants */
Align8 : [_][aA][lL][iI][gG][nN][(][8][)] ;
Align7 : [_][aA][lL][iI][gG][nN][(][7][)] ;
Align6 : [_][aA][lL][iI][gG][nN][(][6][)] ;
Align5 : [_][aA][lL][iI][gG][nN][(][5][)] ;
Align4 : [_][aA][lL][iI][gG][nN][(][4][)] ;
Align3 : [_][aA][lL][iI][gG][nN][(][3][)] ;
Align2 : [_][aA][lL][iI][gG][nN][(][2][)] ;
Align1 : [_][aA][lL][iI][gG][nN][(][1][)] ;
Align0 : [_][aA][lL][iI][gG][nN][(][0][)] ;

/* Keywords for keyboard shortcut notations */
F1Key : [fF][1];
F2Key : [fF][2];
F3Key : [fF][3];
F4Key : [fF][4];
F5Key : [fF][5];
F6Key : [fF][6];
F7Key : [fF][7];
F8Key : [fF][8];
F9Key : [fF][9];
F10Key : [fF][1][0];
F11Key : [fF][1][1];
F12Key : [fF][1][2];
CtrlF1Key : [cC][tT][rR][lL][fF][1];
CtrlF2Key : [cC][tT][rR][lL][fF][2];
CtrlF3Key : [cC][tT][rR][lL][fF][3];
CtrlF4Key : [cC][tT][rR][lL][fF][4];
CtrlF5Key : [cC][tT][rR][lL][fF][5];
CtrlF6Key : [cC][tT][rR][lL][fF][6];
CtrlF7Key : [cC][tT][rR][lL][fF][7];
CtrlF8Key : [cC][tT][rR][lL][fF][8];
CtrlF9Key : [cC][tT][rR][lL][fF][9];
CtrlF10Key : [cC][tT][rR][lL][fF][1][0];
CtrlF11Key : [cC][tT][rR][lL][fF][1][1];
CtrlF12Key : [cC][tT][rR][lL][fF][1][2];
PageUpKey : [pP][aA][gG][eE][uU][pP];
PageDownKey : [pP][aA][gG][eE][dD][oO][wW][nN];
HomeKey : [hH][oO][mM][eE];
EndKey : ('End')|([eN][nN][dD]);
CursorLeft : [cC][uU][rR][sS][oO][rR][lL][eE][fF][tT];
CursorRight : [cC][uU][rR][sS][oO][rR][rR][iI][gG][hH][tT];
CursorDown : [cC][uU][rR][sS][oO][rR][dD][oO][wW][nN];
CursorUp : [cC][uU][rR][sS][oO][rR][uU][pP];
CtrlCursorLeft : [cC][tT][rR][lL][cC][uU][rR][sS][oO][rR][lL][eE][fF][tT];
CtrlCursorDown : [cC][tT][rR][lL][cC][uU][rR][sS][oO][rR][dD][oO][wW][nN];
CtrlCursorUp : [cC][tT][rR][lL][cC][uU][rR][sS][oO][rR][uU][pP];
CtrlCursorRight : [cC][tT][rR][lL][cC][uU][rR][sS][oO][rR][rR][iI][gG][hH][tT];

/* Skip comments and whitespaces, push directives to hidden channel */
IncludeDirective
	: Hash Whitespace? Include Whitespace? (
		('"' ~[\r\n]* '"')
		| (Less ~[\r\n]* Greater)
	) Whitespace? Newline -> channel(HIDDEN);

Directive: '#' ~ [\n]* -> channel (HIDDEN);

Whitespace : [ \t]+ -> skip;

Newline : ( '\r' '\n'? | '\n') -> skip;

BlockComment : '/*' .*? '*/' -> skip;

LineComment : '//' ~[\r\n]* -> skip;