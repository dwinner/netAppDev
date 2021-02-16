using System;

namespace SearchAlgs.Lib
{
   /// <summary>
   ///    Searching algorithms
   /// </summary>
   public static class Searching
   {
      /// <summary>
      ///    Linear search
      /// </summary>
      /// <typeparam name="T">Comparable generic type</typeparam>
      /// <param name="values">Array of values</param>
      /// <param name="searchKey">Search key</param>
      /// <returns>Found index or -1 if nothing has been found</returns>
      public static int LinearSearch<T>(T[] values, T searchKey)
         where T : IComparable<T>
      {
         var foundIdx = -1;

         for (var index = 0; index < values.Length; index++)
            if (values[index].CompareTo(searchKey) == 0)
            {
               foundIdx = index;
               break;
            }

         return foundIdx;
      }

      /// <summary>
      ///    Linear search
      /// </summary>
      /// <typeparam name="T">Generic type</typeparam>
      /// <param name="values">Array of values</param>
      /// <param name="searchKey">Search key</param>
      /// <param name="searchFunc">Search function</param>
      /// <returns>Found index or -1 if nothing has been found</returns>
      public static int LinearSearch<T>(T[] values, T searchKey, Func<T, T, bool> searchFunc)
      {
         var foundIdx = -1;
         for (var index = 0; index < values.Length; index++)
            if (searchFunc(values[index], searchKey))
            {
               foundIdx = index;
               break;
            }

         return foundIdx;
      }

      /// <summary>
      ///    Binary search
      /// </summary>
      /// <typeparam name="T">Comparable generic type</typeparam>
      /// <param name="values">Array of values</param>
      /// <param name="searchElement">Element to search</param>
      /// <exception cref="TooBigArrayException">When overflow has occured</exception>
      /// <returns>Found index or -1</returns>
      public static int BinarySearch<T>(T[] values, T searchElement)
         where T : IComparable<T>
      {
         try
         {
            checked
            {
               var low = 0;
               var high = values.Length - 1;
               var middle = (low + high + 1) / 2;

               do
               {
                  if (searchElement.CompareTo(values[middle]) == 0)
                     return middle;
                  if (searchElement.CompareTo(values[middle]) < 0)
                     high = middle - 1;
                  else
                     low = middle + 1;

                  middle = (low + high + 1) / 2;
               } while (low <= high);

               return -1;
            }
         }
         catch (OverflowException overflowEx)
         {
            throw new TooBigArrayException("Array is too big for searching", overflowEx);
         }
      }

      /// <summary>
      ///    Binary search
      /// </summary>
      /// <typeparam name="T">Generic type</typeparam>
      /// <param name="values">Array of values</param>
      /// <param name="searchElement">Element to search</param>
      /// <param name="compareFunc">
      ///    Compare function
      ///    <remarks>
      ///       1st param - element to search,
      ///       2nd param - current array element,
      ///       return value = 0, if element to search is equal to the current array element;
      ///       if element to search is less than the current array element, return value &lt; 0;
      ///       if element to search is greater than the current array element, return value &gt; 0;
      ///    </remarks>
      /// </param>
      /// <exception cref="TooBigArrayException">When overflow has occured</exception>
      /// <returns>Found index or -1</returns>
      public static int BinarySearch<T>(T[] values, T searchElement, Func<T, T, int> compareFunc)
      {
         try
         {
            checked
            {
               var low = 0;
               var high = values.Length - 1;
               var middle = (low + high + 1) / 2;

               do
               {
                  if (compareFunc(searchElement, values[middle]) == 0)
                     return middle;
                  if (compareFunc(searchElement, values[middle]) < 0)
                     high = middle - 1;
                  else
                     low = middle + 1;

                  middle = (low + high + 1) / 2;
               } while (low <= high);

               return -1;
            }
         }
         catch (OverflowException overflowEx)
         {
            throw new TooBigArrayException("Array is too big for searching", overflowEx);
         }
      }
   }
}