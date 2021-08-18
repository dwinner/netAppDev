/**
 * Способы создания массивов
 */

namespace _01_ArrayCreations
{
   class Program
   {
      static void Main(string[] args)
      {
         int[] firstArray = new int[10];
         for (int i = 0; i < firstArray.Length; i++)
         {
            firstArray[i] = i * 2;
         }
         int[] secondArray = new int[] { 2, 4, 6, 8 };
         int[] thirdArray = { 1, 3, 5, 7 };
      }
   }
}
