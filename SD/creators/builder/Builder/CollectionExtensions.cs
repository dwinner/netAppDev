﻿using System.Collections.Generic;

namespace Builder
{
   /// <summary>
   ///    Небольшое расширение для коллекций.
   /// </summary>
   public static class CollectionExtensions
   {
      public static bool Empty<T>(this ICollection<T> aCollection) => aCollection.Count == 0;
   }
}