#if false

namespace CSharp10Features.GenericAttributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property,Inherited = false, AllowMultiple = true)]
public sealed class GenericAttribute<T> : Attribute
{
   private readonly T _value;

   // This is a positional argument
   public GenericAttribute(T value)
   {
      _value = value;
   }

   public T Value => _value;
}

[GenericAttribute<string>()]
public string Method() => default;

#endif
