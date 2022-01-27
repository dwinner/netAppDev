using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class ParameterType : IEquatable<ParameterType>
    {
        public ParameterType(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public string Name { get; }

        public string Type { get; }

        public string Description { get; }

        public static IEqualityComparer<ParameterType> DefaultComparer { get; } = new DefaultEqualityComparer();

        public bool Equals(ParameterType other)
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

            return Equals((ParameterType)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0) * 397) ^
                       (Type != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Type) : 0);
            }
        }

        public static bool operator ==(ParameterType left, ParameterType right) => Equals(left, right);

        public static bool operator !=(ParameterType left, ParameterType right) => !Equals(left, right);

        private sealed class DefaultEqualityComparer : IEqualityComparer<ParameterType>
        {
            public bool Equals(ParameterType x, ParameterType y) => x != null && x.Equals(y);

            public int GetHashCode(ParameterType obj) => obj.GetHashCode();
        }
    }
}