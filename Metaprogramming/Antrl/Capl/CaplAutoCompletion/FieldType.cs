using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class FieldType : IEquatable<FieldType>
    {
        public FieldType(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public string Name { get; }

        public string Type { get; }

        public string Description { get; }

        public static IEqualityComparer<FieldType> DefaultComparer { get; } = new DefaultEqualityComparer();

        public bool Equals(FieldType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Type, other.Type, StringComparison.OrdinalIgnoreCase);
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

            return Equals((FieldType)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0) * 397) ^
                       (Type != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Type) : 0);
            }
        }

        public static bool operator ==(FieldType left, FieldType right) => Equals(left, right);

        public static bool operator !=(FieldType left, FieldType right) => !Equals(left, right);

        private sealed class DefaultEqualityComparer : IEqualityComparer<FieldType>
        {
            public bool Equals(FieldType x, FieldType y) => x != null && x.Equals(y);

            public int GetHashCode(FieldType obj) => obj.GetHashCode();
        }
    }
}