using System;

namespace TrieDemo;

internal class Info
{
   private static readonly Random Rand = new();

   public Info(string key) => Key = key;

   public string Key { get; set; }

   public double SomeValue => Rand.NextDouble();
}