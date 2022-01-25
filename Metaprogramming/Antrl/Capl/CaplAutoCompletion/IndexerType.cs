using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class IndexerType : IEquatable<IndexerType>
    {
        public IndexerType(string name, string description, ReturnType returnType, ISet<ParameterType> parameters,
            string writable)
        {
            Name = name;
            Description = description;
            ReturnType = returnType;
            Parameters = parameters;
            Writable = writable;
        }

        public string Name { get; }

        public string Description { get; }

        public ReturnType ReturnType { get; }

        public ISet<ParameterType> Parameters { get; }

        public string Writable { get; }

        public static IEqualityComparer<IndexerType> DefaultComparer { get; } = new DefaultEqualityComparer();

        public bool Equals(IndexerType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((IndexerType)obj);
        }

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        public static bool operator ==(IndexerType left, IndexerType right) => Equals(left, right);

        public static bool operator !=(IndexerType left, IndexerType right) => !Equals(left, right);

        private sealed class DefaultEqualityComparer : IEqualityComparer<IndexerType>
        {
            public bool Equals(IndexerType x, IndexerType y) => x != null && x.Equals(y);

            public int GetHashCode(IndexerType obj) => obj.GetHashCode();
        }
    }
}