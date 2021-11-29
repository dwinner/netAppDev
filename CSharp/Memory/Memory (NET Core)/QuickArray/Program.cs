using System;

internal static class Program
{
   public static unsafe void Main()
   {
      string? userInput;
      int size;
      do
      {
         Console.Write($"How big an array do you want? {Environment.NewLine}>");
         userInput = Console.ReadLine();
      } while (!int.TryParse(userInput, out size));

      var pArray = stackalloc long[size];
      for (var i = 0; i < size; i++)
      {
         pArray[i] = i * i;
      }

      for (var i = 0; i < size; i++)
      {
         Console.WriteLine($"Element {i} = {*(pArray + i)}");
      }

      Console.ReadLine();
   }
}