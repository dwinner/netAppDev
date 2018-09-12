using System.Globalization;
using System.Text;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace HowToCSharp.Ch05.BaseConvert
{
   public class ConvertUtilities
   {
      private static string ConvertToBase(long decNum, int destBase)
      {
         var builder = new StringBuilder();
         var accum = decNum;
         while (accum > 0)
         {
            var digit = accum % destBase;
            string digitStr;
            if (digit <= 9)
               digitStr = digit.ToString(CultureInfo.InvariantCulture);
            else
               switch (digit)
               {
                  case 10:
                     digitStr = "A";
                     break;
                  case 11:
                     digitStr = "B";
                     break;
                  case 12:
                     digitStr = "C";
                     break;
                  case 13:
                     digitStr = "D";
                     break;
                  case 14:
                     digitStr = "E";
                     break;
                  case 15:
                     digitStr = "F";
                     break;
                  // не понятно, что делать с базой больше 16
                  default:
                     digitStr = "?";
                     break;
               }
            builder.Append(digitStr);
            accum /= destBase;
         }
         return builder.ToString();
      }
   }
}