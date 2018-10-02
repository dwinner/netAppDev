using System.Collections.Generic;

namespace TrieDemo
{
   public class Trie<T>
   {
      #region Внутренний класс для хранения значений и ссылок на следующие узлы

      private class TrieNode<TU>
      {
         private readonly Dictionary<char, TrieNode<TU>> _next = new Dictionary<char, TrieNode<TU>>();
         private ICollection<TU> Values { get; set; }

         public TrieNode()
         {
            Values = new List<TU>();
            _next = new Dictionary<char, TrieNode<TU>>();
         }

         public void AddValue(string key, int depth, TU item)
         {
            if (depth < key.Length)
            {
               // Продолжить создание узлов (или переход к узлам),
               // пока не будет достигнут конец ключа
               TrieNode<TU> subNode;
               if (!_next.TryGetValue(key[depth], out subNode))
               {
                  subNode = new TrieNode<TU>();
                  _next[key[depth]] = subNode;
               }
               subNode.AddValue(key, depth + 1, item);
            }
            else
            {
               Values.Add(item);
            }
         }

         public TrieNode<TU> GetNext(char c) // Получить узел-потомок по символу
         {
            TrieNode<TU> node;
            return _next.TryGetValue(c, out node) ? node : null;
         }

         public ICollection<TU> GetValues(bool recursive)   // Получить все значения этого узла и, возможно, все его потомки
         {
            var values = new List<TU>();
            values.AddRange(Values);
            if (recursive)
            {
               foreach (TrieNode<TU> node in _next.Values)
               {
                  values.AddRange(node.GetValues(true));
               }
            }

            return values;
         }
      }

      #endregion

      private readonly TrieNode<T> _root = new TrieNode<T>();

      public void AddValue(string key, T item)
      {
         _root.AddValue(key, 0, item);
      }

      public ICollection<T> FindValues(string key, bool recursive)
      {
         TrieNode<T> next = _root;
         int index = 0;
         while (index < key.Length && next.GetNext(key[index]) != null)       // Проследовать по ключу к последнему узлу
         {
            next = next.GetNext(key[index++]);
         }

         return index == key.Length ? next.GetValues(recursive) : new T[0];   // Считывать значения, только если обработан весь ключ
      }
   }
}
