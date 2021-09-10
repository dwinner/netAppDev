using System;
using System.Numerics;
// Need this to get BigInteger!

namespace BasicDataTypes
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with Basic Data Types *****\n");
         LocalVarDeclarations();
         NewingDataTypes();
         ObjectFunctionality();
         DataTypeFunctionality();
         CharFunctionality();
         ParseFromStrings();
         UseDatesAndTimes();
         UseBigInteger();
         Console.ReadKey();
      }

      #region Test Object functionality

      private static void ObjectFunctionality()
      {
         Console.WriteLine("=> System.Object Functionality:");
         // A C# int is really a shorthand for System.Int32.
         // which inherits the following members from System.Object.
         Console.WriteLine("12.GetHashCode() = {0}", 12.GetHashCode());
         Console.WriteLine("12.Equals(23) = {0}", 12.Equals(23));
         Console.WriteLine("12.ToString() = {0}", 12);
         Console.WriteLine("12.GetType() = {0}", 12.GetType());
         Console.WriteLine();
      }

      #endregion

      #region Test data type functionality

      private static void DataTypeFunctionality()
      {
         Console.WriteLine("=> Data type Functionality:");
         Console.WriteLine("Max of int: {0}", int.MaxValue);
         Console.WriteLine("Min of int: {0}", int.MinValue);
         Console.WriteLine("Max of double: {0}", double.MaxValue);
         Console.WriteLine("Min of double: {0}", double.MinValue);
         Console.WriteLine("double.Epsilon: {0}", double.Epsilon);
         Console.WriteLine("double.PositiveInfinity: {0}",
            double.PositiveInfinity);
         Console.WriteLine("double.NegativeInfinity: {0}",
            double.NegativeInfinity);
         Console.WriteLine("bool.FalseString: {0}", bool.FalseString);
         Console.WriteLine("bool.TrueString: {0}", bool.TrueString);
         Console.WriteLine();
      }

      #endregion

      #region Test char data type

      private static void CharFunctionality()
      {
         Console.WriteLine("=> char type Functionality:");
         var myChar = 'a';
         Console.WriteLine("char.IsDigit('a'): {0}", char.IsDigit(myChar));
         Console.WriteLine("char.IsLetter('a'): {0}", char.IsLetter(myChar));
         Console.WriteLine("char.IsWhiteSpace('Hello There', 5): {0}",
            char.IsWhiteSpace("Hello There", 5));
         Console.WriteLine("char.IsWhiteSpace('Hello There', 6): {0}",
            char.IsWhiteSpace("Hello There", 6));
         Console.WriteLine("char.IsPunctuation('?'): {0}",
            char.IsPunctuation('?'));
         Console.WriteLine();
      }

      #endregion

      #region Parsing data

      private static void ParseFromStrings()
      {
         try
         {
            Console.WriteLine("=> Data type parsing:");
            var b = bool.Parse("True");
            Console.WriteLine("Value of b: {0}", b);
            var d = double.Parse("99,884");
            Console.WriteLine("Value of d: {0}", d);
            var i = int.Parse("8");
            Console.WriteLine("Value of i: {0}", i);
            var c = char.Parse("w");
            Console.WriteLine("Value of c: {0}", c);
            Console.WriteLine();
         }
         catch (FormatException)
         {
            Console.WriteLine("Format exception: {0}", 99.884);
         }
      }

      #endregion

      #region Work with dates / times

      private static void UseDatesAndTimes()
      {
         Console.WriteLine("=> Dates and Times:");
         // This constructor takes (year, month, day)
         var dt = new DateTime(2004, 10, 17);

         // What day of the month is this?
         Console.WriteLine("The day of {0} is {1}", dt.Date, dt.DayOfWeek);
         dt = dt.AddMonths(2); // Month is now December.
         Console.WriteLine("Daylight savings: {0}", dt.IsDaylightSavingTime());

         // This constructor takes (hours, minutes, seconds)
         var ts = new TimeSpan(4, 30, 0);
         Console.WriteLine(ts);

         // Subtract 15 minutes from the current TimeSpan and
         // print the result.
         Console.WriteLine(ts.Subtract(new TimeSpan(0, 15, 0)));
      }

      #endregion

      #region Use BigInteger

      private static void UseBigInteger()
      {
         Console.WriteLine("=> Use BigInteger:");
         var biggy = BigInteger.Parse("9999999999999999999999999999999999999999999999");
         Console.WriteLine("Value of biggy is {0}", biggy);
         Console.WriteLine("Is biggy an even value?: {0}", biggy.IsEven);
         Console.WriteLine("Is biggy a power of two?: {0}", biggy.IsPowerOfTwo);
         var reallyBig = BigInteger.Multiply(biggy, BigInteger.Parse("8888888888888888888888888888888888888888888"));
         var reallyBig2 = biggy * reallyBig;

         Console.WriteLine("Value of reallyBig is {0}", reallyBig);
      }

      #endregion

      #region Local variable declarations

      private static void LocalVarDeclarations()
      {
         Console.WriteLine("=> Data Declarations:");

         var myInt = 0;

         string myString;
         myString = "This is my character data";

         bool b1 = true, b2 = false, b3 = b1;

         var b4 = false;

         Console.WriteLine("Your data: {0}, {1}, {2}, {3}, {4}, {5}",
            myInt, myString, b1, b2, b3, b4);
         Console.WriteLine();
      }

      private static void NewingDataTypes()
      {
         Console.WriteLine("=> Using new to create variables:");
         var b = new bool(); // Set to false.
         var i = new int(); // Set to 0.
         var d = new double(); // Set to 0.
         var dt = new DateTime(); // Set to 1/1/0001 12:00:00 AM
         Console.WriteLine("{0}, {1}, {2}, {3}", b, i, d, dt);
         Console.WriteLine();
      }

      #endregion
   }
}