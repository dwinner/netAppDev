using System;
using System.Collections.Generic;

public interface IEnumerableEx<out T> : IEnumerable<T>
{
   public IEnumerable<T> Where(Func<T, bool> pred)
   {
      foreach (var item in this)
      {
         if (pred(item))
         {
            yield return item;
         }
      }
   }
}