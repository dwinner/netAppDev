namespace TrieDemo.Tests;

#nullable disable

public class Trie<T>
{
   private readonly TrieNode<T> _root = new();

   public void AddValue(string key, T item)
   {
      _root.AddValue(key, 0, item);
   }

   public ICollection<T> FindValues(string key, bool recursive = true)
   {
      var next = _root;
      var index = 0;
      while (index < key.Length && next.GetNext(key[index]) != null)
      {
         next = next.GetNext(key[index++]);
      }

      return index == key.Length
         ? next.GetValues(recursive)
         : Array.Empty<T>();
   }

   private sealed class TrieNode<TVal>
   {
      private readonly Dictionary<char, TrieNode<TVal>> _next = new();

      private List<TVal> Values { get; } = new();

      internal void AddValue(string key, int depth, TVal item)
      {
         if (depth < key.Length)
         {
            if (!_next.TryGetValue(key[depth], out var subNode))
            {
               subNode = new TrieNode<TVal>();
               _next[key[depth]] = subNode;
            }

            subNode.AddValue(key, depth + 1, item);
         }
         else
         {
            Values.Add(item);
         }
      }

      internal TrieNode<TVal> GetNext(char chr) => _next.GetValueOrDefault(chr);

      internal ICollection<TVal> GetValues(bool recursive)
      {
         var values = new List<TVal>();
         values.AddRange(Values);
         if (!recursive)
         {
            return values;
         }

         foreach (var node in _next.Values)
         {
            values.AddRange(node.GetValues(true));
         }

         return values;
      }
   }
}