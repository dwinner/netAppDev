using System;
using System.Collections.Generic;

namespace SortStringsTest
{
   /// <summary>
   ///     Фабрика компараторов
   /// </summary>
   public static class ComparerFactory
   {
      /// <summary>
      ///     Обобщенный компаратор натуральной сортировки строк
      /// </summary>
      public static IComparer<string> NaturalSort
      {
         get { return new NaturalSortComparer(); }
      }

      /// <summary>
      ///     Компаратор для натурального сравнения строк
      /// </summary>
      private class NaturalSortComparer : IComparer<string>
      {
         private const int DefaultSplitBufferSize = 0x100;
         private readonly char[] _splitBuffer;
         private readonly int _splitBufferSize;

         private NaturalSortComparer(int splitBufferSize)
         {
            _splitBufferSize = splitBufferSize;
            _splitBuffer = new char[_splitBufferSize];
         }

         public NaturalSortComparer()
            : this(DefaultSplitBufferSize)
         {
         }

         public int Compare(string x, string y)
         {
            var a = SplitByNumbers(x);
            var b = SplitByNumbers(y);

            var numToCompare = a.Count < b.Count ? a.Count : b.Count;
            for (var i = 0; i < numToCompare; i++)
            {
               if (a[i].Equals(b[i]))
               {
                  continue;
               }

               int aInt, bInt;
               var aIsNumber = int.TryParse(a[i], out aInt);
               var bIsNumber = int.TryParse(b[i], out bInt);
               var bothNumbers = aIsNumber && bIsNumber;
               var bothNotNumbers = !aIsNumber && !bIsNumber;

               return bothNumbers
                   ? aInt.CompareTo(bInt)
                   : (bothNotNumbers ? string.Compare(a[i], b[i], StringComparison.Ordinal) : (aIsNumber ? -1 : 1));
            }

            return a.Count.CompareTo(b.Count);
         }

         private IList<string> SplitByNumbers(string value)
         {
            if (value == null)
            {
               throw new ArgumentNullException("value");
            }

            if (value.Length > _splitBufferSize)
            {
               throw new ArgumentException(string.Format("argument 'value' must be less or equal than {0}",
                   _splitBufferSize));
            }

            IList<string> list = new List<string>();
            int current = 0, dest = 0;
            while (current < value.Length)
            {
               while (current < value.Length && !char.IsDigit(value[current]))
               {
                  _splitBuffer[dest++] = value[current++];
               }

               if (dest > 0)
               {
                  list.Add(new string(_splitBuffer, 0, dest));
                  dest = 0;
               }

               while (current < value.Length && char.IsDigit(value[current]))
               {
                  _splitBuffer[dest++] = value[current++];
               }

               if (dest > 0)
               {
                  list.Add(new string(_splitBuffer, 0, dest));
               }
            }

            return list;
         }
      }
   }
}