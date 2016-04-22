/**
 * Создание собственного средства форматирования
 */

using System;
using System.Text;
using System.Threading;

namespace CustomFormatProvider
{
   internal static class Program
   {
      private static void Main()
      {
         var builder = new StringBuilder();
         builder.AppendFormat(new BoldIntFormatProvider(), "{0} {1} {2:M}", "Jeff", 123, DateTime.Now);
         Console.WriteLine(builder);
      }
   }

   internal sealed class BoldIntFormatProvider : IFormatProvider, ICustomFormatter
   {
      public string Format(string format, object arg, IFormatProvider formatProvider)
      {
         var formattable = arg as IFormattable;
         var targetString = formattable == null ? arg.ToString() : formattable.ToString(format, formatProvider);
         return arg is int ? string.Format("<b>{0}</b>", targetString) : targetString;
      }

      public object GetFormat(Type formatType)
      {
         return formatType == typeof(ICustomFormatter)
            ? this
            : Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
      }
   }
}