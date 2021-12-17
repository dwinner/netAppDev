using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace CaplGrammar.Application.Poco
{
    public struct CaplIssue : IEquatable<CaplIssue>
    {
        public CaplIssue(string message, IToken token, int actualLine, int actualColumn)
        {
            Message = message;
            Token = token;
            ActualLine = actualLine;
            ActualColumn = actualColumn;
        }

        public string Message { get; }

        public IToken Token { get; }

        public int ActualLine { get; }

        public int ActualColumn { get; }

        public override string ToString() =>
            $"Line: {Token.Line}, Column: {Token.Column}, Message: {Message}";

        public bool Equals(CaplIssue other) =>
            Message == other.Message
            && ActualLine == other.ActualLine
            && ActualColumn == other.ActualColumn;

        public override bool Equals(object obj) => obj is CaplIssue other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Message != null ? Message.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ ActualLine;
                hashCode = (hashCode * 397) ^ ActualColumn;
                return hashCode;
            }
        }

        public static bool operator ==(CaplIssue left, CaplIssue right) => left.Equals(right);

        public static bool operator !=(CaplIssue left, CaplIssue right) => !left.Equals(right);

        private sealed class CaplIssueEqualityComparer : IEqualityComparer<CaplIssue>
        {
            public bool Equals(CaplIssue x, CaplIssue y) => x.Equals(y);

            public int GetHashCode(CaplIssue obj) => obj.GetHashCode();
        }

        public static IEqualityComparer<CaplIssue> DefaultComparer { get; } = new CaplIssueEqualityComparer();
    }
}