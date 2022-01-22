using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Poco;
using CaplGrammar.Core;

namespace CaplGrammar.Application.Impl.GrammarValidation
{
    public sealed class CaplSyntaxValidationImpl : CaplValidationBase
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
            var tokenStream = new CommonTokenStream(caplLexer);
            var caplParser = new CaplParser(tokenStream)
            {
                ErrorHandler = new CaplErrorStrategy()  // TODO: inject in ctor
            };
            caplParser.RemoveErrorListeners();
            var errorHandler = new DefaultErrorHandlerImpl();   // TODO: inject in ctor
            caplParser.AddErrorListener(errorHandler);
            var caplRoot = caplParser.primaryExpression();

            return caplRoot.IsEmpty ? errorHandler.Issues : Enumerable.Empty<CaplIssue>();
        }
    }
}