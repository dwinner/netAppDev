using FpLibrary;
using static System.Console;

WriteLine("Exercise 4.4");

// Using pipeline pattern

var result = 2.MakeTotal(3)
   .MakeCube();
WriteLine($"Result: {result}");

namespace FpLibrary
{
   public static class Extensions
   {
      //public static Func<int, int, int> Compose(
      //    this Func<int, int, int> total,
      //    Func<int, int> cube)
      //{
      //    return (x, y) => cube(total(x, y));
      //}

      public static int MakeTotal(this int a, int b) => a + b;
      public static int MakeCube(this int a) => a * a * a;
   }
}