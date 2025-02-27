﻿using System;
using System.Collections.Generic;

namespace TrieDemo;

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
      while (index < key.Length && next.GetNext(key[index]) != null) // Проследовать по ключу к последнему узлу
      {
         next = next.GetNext(key[index++]);
      }

      return index == key.Length
         ? next.GetValues(recursive)
         : Array.Empty<T>(); // Считывать значения, только если обработан весь ключ
   }

   #region Внутренний класс для хранения значений и ссылок на следующие узлы

   private sealed class TrieNode<TVal>
   {
      private readonly Dictionary<char, TrieNode<TVal>> _next = new();

      private ICollection<TVal> Values { get; } = new List<TVal>();

      internal void AddValue(string key, int depth, TVal item)
      {
         if (depth < key.Length)
         {
            // Продолжить создание узлов (или переход к узлам),
            // пока не будет достигнут конец ключа
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

      // Получить узел-потомок по символу
      internal TrieNode<TVal> GetNext(char chr) =>
         _next.TryGetValue(chr, out var node)
            ? node
            : null;

      internal ICollection<TVal> GetValues(bool recursive) // Получить все значения этого узла и, возможно, все его потомки
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

   #endregion
}