namespace UseOfOption;

public readonly struct Option<T>
   where T : class
{
   private readonly bool _isSome;
   private readonly T? _value;

   private Option(T value)
   {
      _value = value;
      _isSome = _value is not null;
   }

   public static Option<T> None => default;

   public static Option<T> Some(T value) => new(value);

   public bool IsSome(out T? value)
   {
      value = _value;
      return _isSome;
   }
}