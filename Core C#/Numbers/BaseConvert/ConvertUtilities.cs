using System;
using System.Globalization;
using System.Text;

namespace HowToCSharp.Ch05.BaseConvert
{
   public class ConvertUtilities
   {
      public ConvertUtilities()
      {
      }

      private static string ConvertToBase(Int64 decNum, int destBase)
      {
         StringBuilder sb = new StringBuilder();
         Int64 accum = decNum;
         while (accum > 0)
         {
            Int64 digit = (accum % destBase);
            string digitStr;
            if (digit <= 9)
            {
               digitStr = digit.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
               switch (digit)
               {
                  case 10: digitStr = "A"; break;
                  case 11: digitStr = "B"; break;
                  case 12: digitStr = "C"; break;
                  case 13: digitStr = "D"; break;
                  case 14: digitStr = "E"; break;
                  case 15: digitStr = "F"; break;
                  // не понятно, что делать с базой больше 16
                  default: digitStr = "?"; break;
               }
            }
            sb.Append(digitStr);
            accum /= destBase;
         }
         return sb.ToString();
      }
   }
}