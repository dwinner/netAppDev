using System.Collections;
using System.Collections.Generic;

namespace LanguageBasedAdapter
{
   public class CustomCollection : IEnumerable<int>
   {
      private readonly List<int> _list = new List<int>();

      public IEnumerator<int> GetEnumerator() => _list.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      public void Insert(int value) => _list.Add(value);
   }
}