using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class CaplClass : IEquatable<CaplClass>
    {
        public CaplClass(string className, string baseClassName, ISet<FieldType> fields, ISet<IndexerType> indexers,
            ISet<FunctionType> methods)
        {
            ClassName = className;
            BaseClassName = baseClassName;
            Fields = fields;
            Indexers = indexers;
            Methods = methods;
        }

        public string ClassName { get; }

        public string BaseClassName { get; }

        public ISet<FieldType> Fields { get; }

        public ISet<IndexerType> Indexers { get; }

        public ISet<FunctionType> Methods { get; }

        public static IEqualityComparer<CaplClass> DefaultComparer { get; } = new DefaultEqualityComparer();

        public bool Equals(CaplClass other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ClassName == other.ClassName;
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

            return Equals((CaplClass)obj);
        }

        public override int GetHashCode() => ClassName != null ? ClassName.GetHashCode() : 0;

        public static bool operator ==(CaplClass left, CaplClass right) => Equals(left, right);

        public static bool operator !=(CaplClass left, CaplClass right) => !Equals(left, right);

        private sealed class DefaultEqualityComparer : IEqualityComparer<CaplClass>
        {
            public bool Equals(CaplClass x, CaplClass y) => x != null && x.Equals(y);

            public int GetHashCode(CaplClass obj) => obj.GetHashCode();
        }

        public override string ToString() => $"{nameof(ClassName)}: {ClassName}";
    }
}