/*
 * Copyright 2010 Chaur Wu.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
 
grammar Stitch;

options {
  language = CSharp2;
}

@header {
using System.Collections.Generic;
using Stitch.Ast;
}

@namespace { Stitch }

program returns [IList<IFunction> result] 
   : { result = new List<IFunction>(); } 
   (func { $result.Add($func.result); } )*
   ;

func returns [IFunction result]
   : '<' IDENT '(' inputArguments=parameters?  ')' funcCode  
   'return(' returnValues=parameters?  ')>'
   { 
     FunctionSignature signature = new FunctionSignature($IDENT.Text,
              $inputArguments.result, $returnValues.result);
     $result = new Function(signature, $funcCode.result);
   };
          
parameters returns [IList<String> result]
    : { $result = new List<String>(); } 
     firstParameter=IDENT { $result.Add($firstParameter.Text); }
     (',' nextParameter=IDENT { $result.Add($nextParameter.Text); })*;
 
funcCode returns [IFunctionCode result]
    : { bool isInclude = false; } 
    (include { isInclude = true; }
    | IDENT )
    CODEBLOCK
    {
	     if (isInclude)
	        $result = new IncludedCode($include.result);
	     else {  
	       String codeBlock = $CODEBLOCK.Text;
	       int i = codeBlock.IndexOf('>');
	       int j = codeBlock.LastIndexOf('<');
	       String code = codeBlock.Substring(i + 1, j - i -1);
	       $result = new BlockCode($IDENT.Text, code);
	     }
    };
    
include returns [String result]
    : 'include' FILEPATH { $result = $FILEPATH.Text; };
     
CODEBLOCK : '>' .* '<';

fragment LETTER : ('a'..'z' | 'A'..'Z') ;
fragment DIGIT : '0'..'9';

FILEPATH : (LETTER | DIGIT) (LETTER | DIGIT | '.' | '\\' | '/')* '.' (LETTER | DIGIT)+;
IDENT : LETTER (LETTER | DIGIT)*;
WS : (' ' | '\t' | '\n' | '\r' | '\f')+ {$channel = HIDDEN;};


