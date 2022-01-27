using System;

namespace CaplAutoCompletion
{
    public class ReturnType : IEquatable<ReturnType>
    {
        public ReturnType(string type, string description)
        {
            Type = type;
            Description = description;
        }

        public string Type { get; }

        public string Description { get; }

        public bool Equals(ReturnType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Type, other.Type, StringComparison.OrdinalIgnoreCase);
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

            return Equals((ReturnType)obj);
        }

        public override int GetHashCode() => Type != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Type) : 0;

        public static bool operator ==(ReturnType left, ReturnType right) => Equals(left, right);

        public static bool operator !=(ReturnType left, ReturnType right) => !Equals(left, right);
    }
}