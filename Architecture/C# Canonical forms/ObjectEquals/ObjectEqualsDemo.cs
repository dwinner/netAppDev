/**
 * Переопределение Object.Equals
 */

using System;

namespace ObjectEquals
{
   static class ObjectEqualsDemo
   {
      static void Main()
      {
         var numberA = new ComplexNumber(1, 2);
         var numberB = new ComplexNumber(1, 2);

         Console.WriteLine("Результат проверки на эквивалентность: {0}",
            numberA == numberB);
         // Note: Если действительно нужна ссылочная эквивалентность.
         //Console.WriteLine("Идентичность ссылок: {0}",
         //   (object) numberA == (object) numberB);
         Console.WriteLine("Идентичность ссылок: {0}",
            ReferenceEquals(numberA, numberB));

         Console.ReadKey();
      }
   }

   public class ComplexNumber
   {
      private readonly double _real;
      private readonly double _imaginary;

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public override bool Equals(object obj)
      {
         var other = obj as ComplexNumber;
         return other != null &&
                (Math.Abs(_real - other._real) < double.Epsilon &&
                 Math.Abs(_imaginary - other._imaginary) < double.Epsilon);
      }

      public override int GetHashCode()
      {
         return (int)Math.Sqrt(Math.Pow(_real, 2) * Math.Pow(_imaginary, 2));
      }

      public static bool operator ==(ComplexNumber firstNumber, ComplexNumber secondNumber)
      {
         return Equals(firstNumber, secondNumber);
      }

      public static bool operator !=(ComplexNumber firstNumber, ComplexNumber secondNumber)
      {
         return !(firstNumber == secondNumber);
      }
   }
}
