using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Poco;
using CaplGrammar.Core;

namespace CaplGrammar.Application.Impl.SymbolUsageValidation
{
    public sealed class CaplSymbolUsageValidationImpl : CaplValidationBase
    {
        public override IEnumerable<CaplIssue> GetIssues(string source, AntlrInput antlrInput)
        {
            AntlrInputStream antlrStream;
            switch (antlrInput)
            {
                case AntlrInput.None:
                    throw new ArgumentOutOfRangeException(nameof(antlrInput), antlrInput, null);
                case AntlrInput.FromFile:
                    antlrStream = new AntlrFileStream(source, CurrentEncoding);
                    break;
                case AntlrInput.FromContent:
                    antlrStream = new AntlrInputStream(source);
                    break;
                default:
                    goto case AntlrInput.None;
            }

            var caplLexer = new CaplLexer(antlrStream);
            var tokens = new CommonTokenStream(caplLexer);
            var caplParser = new CaplParser(tokens) { BuildParseTree = true };
            var caplAst = caplParser.primaryExpression();

            var walker = new ParseTreeWalker();
            var definitionPhase = new DefinitionPhaseVisitor();
            walker.Walk(definitionPhase, caplAst);

            var globalScope = definitionPhase.GlobalScope;
            var scopes = definitionPhase.Scopes;
            var referencePhase = new ReferencePhaseVisitor(globalScope, scopes);
            walker.Walk(referencePhase, caplAst);

            return referencePhase.Issues;
        }
    }
}