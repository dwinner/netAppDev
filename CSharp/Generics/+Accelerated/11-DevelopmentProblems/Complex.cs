using System;
using System.Collections.Generic;

namespace _11_DevelopmentProblems
{
   public struct Complex<T> : IComparable<Complex<T>>
      where T : struct
   {
      public delegate T BinaryOp(T firstOperand, T secondOperand);   // Делегат для выполнения бинарной операции

      private T _real;
      private T _imaginary;
      private readonly BinaryOp _mult;
      private readonly BinaryOp _add;
      private readonly Converter<double, T> _convToT;
      private readonly Converter<T, double> _convToDouble;

      public T Real
      {
         get { return _real; }
         set { _real = value; }
      }

      public T Img
      {
         get { return _imaginary; }
         set { _imaginary = value; }
      }

      public T Magnitude
      {
         get
         {
            double magnitude =
               Math.Sqrt(
                  _convToDouble(
                     _add(
                        _mult(_real, _real),
                        _mult(_imaginary, _imaginary)
                     )
                  )
               );
            return _convToT(magnitude);
         }
      }

      public Complex(T real,
         T imaginary,
         BinaryOp mult,
         BinaryOp add,
         Converter<T, double> convToDouble,
         Converter<double, T> convToT)
      {
         _real = real;
         _imaginary = imaginary;
         _mult = mult;
         _add = add;
         _convToDouble = convToDouble;
         _convToT = convToT;
      }

      public int CompareTo(Complex<T> other)
      {
         return Comparer<T>.Default.Compare(Magnitude, other.Magnitude);
      }
   }
}
