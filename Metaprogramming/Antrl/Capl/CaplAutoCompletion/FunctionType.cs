using System;
using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class FunctionType : IEquatable<FunctionType>
    {
        public FunctionType(string name, string description, ReturnType returnType, ISet<ParameterType> parameters)
        {
            Name = name;
            Description = description;
            ReturnType = returnType;
            Parameters = parameters;
        }

        public string Name { get; }

        public string Description { get; }

        public ReturnType ReturnType { get; }

        public ISet<ParameterType> Parameters { get; }

        public static IEqualityComparer<FunctionType> DefaultComparer { get; } = new DefaultEqualityComparer();

        public bool Equals(FunctionType other)
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

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(ReturnType)}: {ReturnType}";

        public void Deconstruct(out string name, out string description, out ReturnType returnType,
            out ISet<ParameterType> parameters)
        {
            name = Name;
            description = Description;
            returnType = ReturnType;
            parameters = Parameters;
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

            return Equals((FunctionType)obj);
        }

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        public static bool operator ==(FunctionType left, FunctionType right) => Equals(left, right);

        public static bool operator !=(FunctionType left, FunctionType right) => !Equals(left, right);

        private sealed class DefaultEqualityComparer : IEqualityComparer<FunctionType>
        {
            public bool Equals(FunctionType x, FunctionType y) => x != null && x.Equals(y);

            public int GetHashCode(FunctionType obj) => obj.GetHashCode();
        }
    }
}