using System;
using System.Numerics;
using System.Threading;

namespace ComplexNumberDemo
{
   internal class ComplexFormatter : IFormatProvider, ICustomFormatter
   {
      // note: допускает два формата : i и j
      public string Format(string format, object arg, IFormatProvider formatProvider)
      {
         if (arg is Complex complex)
         {
            if (format.Equals("i", StringComparison.OrdinalIgnoreCase))
            {
               return $"{complex.Real:N2} + {complex.Imaginary:N2}i";
            }

            if (format.Equals("j", StringComparison.OrdinalIgnoreCase))
            {
               return $"{complex.Real:N2} + {complex.Imaginary:N2}j";
            }

            return complex.ToString(format, formatProvider);
         }

         if (arg is IFormattable formattable)
         {
            return formattable.ToString(format, formatProvider);
         }

         return arg != null
            ? arg.ToString()
            : string.Empty;
      }

      public object GetFormat(Type formatType) =>
         formatType == typeof(ICustomFormatter)
            ? this
            : Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
   }
}