using CustomLibrary;
using static System.Console;

WriteLine("Applying the concept of currying.");
int a = 10, b = 2;
// Common approach
var result = new Sample().AddTwoNumbers(a, b);
//var result1 = new Sample().AddTwoNumbers(a);// Error CS7036
WriteLine($"{a}+{b} is {result}");

// New approach-1
var total = new Sample().AddTwoNumbers.Curry()(a)(b);
WriteLine($"{a}+{b} is {total}");

// New approach-2
//Func<int, int> afterFirstNumber = new Sample().AddTwoNumbers.Curry()(a); // Explicit declaration
var afterFirstNumber = new Sample().AddTwoNumbers.Curry()(a); //Implicit declaration
var afterSecondNumber = afterFirstNumber(b);
WriteLine($"{a}+{b} is {afterSecondNumber}");

internal class Sample
{
   public Func<int, int, int> AddTwoNumbers = (first, second) => first + second;
   // public Func<double,double,double> AddTwoNonIntegers = (double first, double second) => first + second;
}

namespace CustomLibrary
{
   public static class CurryExtensions
   {
      //public static Func<int, Func<int, int>> Curry(this Func<int, int, int> f)
      //{
      //    return x => y => f(x, y);
      //}

      // Alternative syntax
      public static Func<int, Func<int, int>> Curry(this Func<int, int, int> f) =>
         x => y => f(x, y);
   }
}