namespace AppContext.DbIdx;

public class BTreeEntry<TKey, TPointer>
   : IEquatable<BTreeEntry<TKey, TPointer>>
{
   public TKey Key { get; init; } = default!;

   public TPointer Pointer { get; init; } = default!;

   public bool Equals(BTreeEntry<TKey, TPointer>? other) =>
      Key!.Equals(other!.Key) && Pointer!.Equals(other.Pointer);

   public override bool Equals(object? obj)
      => Equals(obj as BTreeEntry<TKey, TPointer>);

   public override int GetHashCode()
      => HashCode.Combine(Key, Pointer);

   public static bool operator ==(BTreeEntry<TKey, TPointer>? left, BTreeEntry<TKey, TPointer>? right)
      => Equals(left, right);

   public static bool operator !=(BTreeEntry<TKey, TPointer>? left, BTreeEntry<TKey, TPointer>? right)
      => !Equals(left, right);
}