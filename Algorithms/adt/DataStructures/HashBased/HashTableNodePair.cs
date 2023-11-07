namespace DataStructures.HashBased;

internal sealed class HashTableNodePair<TKey, TValue>
{
   public HashTableNodePair(TKey key, TValue value)
   {
      Key = key;
      Value = value;
   }

   public TKey Key { get; }

   public TValue Value { get; internal set; }
}