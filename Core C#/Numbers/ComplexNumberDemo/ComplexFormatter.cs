using System;
using System.Numerics;

namespace ComplexNumberDemo
{
   class ComplexFormatter : IFormatProvider, ICustomFormatter
   {
      // note: допускает два формата : i и j
      public string Format(string format, object arg, IFormatProvider formatProvider)
      {
         if (arg is Complex)
         {
            Complex c = (Complex)arg;
            if (format.Equals("i", StringComparison.OrdinalIgnoreCase))
            {
               return string.Format("{0} + {1}i",
                  c.Real.ToString("N2"), c.Imaginary.ToString("N2"));
            }
            if (format.Equals("j", StringComparison.OrdinalIgnoreCase))
            {
               return string.Format("{0} + {1}j",
                  c.Real.ToString("N2"), c.Imaginary.ToString("N2"));
            }
            return c.ToString(format, formatProvider);
         }
         IFormattable formattable = arg as IFormattable;
         if (formattable != null)
         {
            return formattable.ToString(format, formatProvider);
         }
         return arg != null ? arg.ToString() : string.Empty;
      }

      public object GetFormat(Type formatType)
      {
         return formatType == typeof(ICustomFormatter)
            ? this
            : System.Threading.Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
      }
   }
}
