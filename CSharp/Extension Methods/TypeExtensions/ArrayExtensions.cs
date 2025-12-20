namespace TypeExtensions;

public static class ArrayExtensions
{
   extension<T>(T[])
   {
      public static T[] operator +(T[] numbers, T[] other)
      {
         var newNumbers = new T[numbers.Length + other.Length];

         var j = 0;
         for (var i = 0; i < numbers.Length; i++, j++)
         {
            newNumbers[j] = numbers[i];
         }

         for (var i = 0; i < other.Length; i++, j++)
         {
            newNumbers[j] = other[i];
         }

         return newNumbers;
      }
   }
}