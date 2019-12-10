using System;
using System.Threading;

namespace HowToCSharp.Ch02.VertexDemo
{
   internal class TypeFormatter : IFormatProvider, ICustomFormatter
   {
      public string Format(string format, object arg, IFormatProvider formatProvider)
      {
         var formattable = arg as IFormattable;
         var value = formattable == null ? arg.ToString() : formattable.ToString(format, formatProvider);
         return string.Format("Type: {0}, Value: {1}", arg.GetType(), value);
      }

      public object GetFormat(Type formatType)
      {
         if (formatType == typeof(ICustomFormatter))
         {
            return this;
         }

         return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
      }
   }
}