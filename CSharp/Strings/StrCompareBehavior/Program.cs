/**
 * Поведение при сравнении строк
 */

using System;
using System.Globalization;
using System.Threading;

namespace StrCompareBehavior
{
   internal static class Program
   {
      private static void Main()
      {
         var output = string.Empty;
         string[] symbol = { "<", "=", ">" };

         // Следующий код демонстрирует, насколько отличается результат
         // сравнения строк для различных региональных стандартов
         const string s1 = "собака";
         const string s2 = "собаки";

         #region Сортировка строк для французского языка

         var cultureInfo = new CultureInfo("fr-FR");
         var x = Math.Sign(cultureInfo.CompareInfo.Compare(s1, s2));
         output += string.Format("{0} Compare: {1} {2} {3}", cultureInfo.Name, s1, s2, symbol[x + 1]);
         output += Environment.NewLine;

         #endregion

         #region Сортировка строк для Японского языка в Японии

         cultureInfo = new CultureInfo("ja-JP");
         x = Math.Sign(cultureInfo.CompareInfo.Compare(s1, s2));
         output += string.Format("{0} Compare: {1} {2} {3}", cultureInfo.Name, s1, s2, symbol[x + 1]);
         output += Environment.NewLine;

         #endregion

         #region Сортировка строк с учетом региональных стандартов потока

         cultureInfo = Thread.CurrentThread.CurrentCulture;
         x = Math.Sign(cultureInfo.CompareInfo.Compare(s1, s2));
         output += string.Format("{0} Compare: {1} {2} {3}", cultureInfo.Name, s1, s2, symbol[x + 1]);
         output += Environment.NewLine;

         #endregion

         Console.WriteLine(output);
      }
   }
}