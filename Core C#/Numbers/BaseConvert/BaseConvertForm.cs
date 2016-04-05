using HowToCSharp.Ch05.BaseConvert.Properties;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace HowToCSharp.Ch05.BaseConvert
{
   public partial class BaseConvertForm : Form
   {
      private bool _calculating;

      public BaseConvertForm()
      {
         InitializeComponent();
      }

      public bool Calculating
      {
         get { return _calculating; }
         set { _calculating = value; }
      }

      private void OnSourceChanged(object sender, EventArgs e)
      {
         Recalculate();
      }

      private void OnBaseChanged(object sender, EventArgs e)
      {
         Recalculate();
      }

      private void Recalculate()
      {
         if (Calculating)
            return;
         _calculating = true;
         string source = textBoxSource.Text;
         int destBase = (int)numericUpDownBase.Value;
         Int64 sourceNum;

         if (!string.IsNullOrEmpty(source) && Int64.TryParse(source, out sourceNum))
         {
            switch (destBase)
            {
               case 2:
               case 8:
               case 10:
               case 16:
                  textBoxDest.Text = Convert.ToString(sourceNum, destBase);
                  break;
               default:
                  textBoxDest.Text = ConvertToBase(sourceNum, destBase);
                  break;
            }
         }
         else
         {
            textBoxDest.Text = Resources.BaseConvertForm_Recalculate_Undefined;
         }
         _calculating = false;
      }

      /// <summary>
      /// Конвертирование числа в строку с другим основанием системы счисления
      /// </summary>
      /// <param name="decNum">Исходное число</param>
      /// <param name="destBase">Назначенная система счисления</param>
      /// <returns>Конвертированную строку</returns>
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
                  // note: не понятно, что делать с базой больше 16
                  default: digitStr = "?"; break;
               }
            }
            sb.Append(digitStr);
            accum /= destBase;
         }
         return sb.ToString();
      }

      /// <summary>
      /// Конвертирование строки с исходной системой счисления в число
      /// </summary>
      /// <param name="num">Число в строке</param>
      /// <param name="fromBase">Исходная система счисления</param>
      /// <returns>Преобразованное десятичное число</returns>
      private static Int64 ConvertFromBase(string num, int fromBase)
      {
         Int64 accum = 0;
         Int64 multiplier = 1;
         for (int i = num.Length - 1; i >= 0; i--)
         {
            int digitVal;
            if (num[i] >= '0' && num[i] <= '9')
            {
               digitVal = num[i] - '0';
            }
            else
            {
               switch (num[i])
               {
                  case 'A': digitVal = 10; break;
                  case 'B': digitVal = 11; break;
                  case 'C': digitVal = 12; break;
                  case 'D': digitVal = 13; break;
                  case 'E': digitVal = 14; break;
                  case 'F': digitVal = 15; break;
                  default: throw new FormatException("Unknown digit");
               }
            }
            accum += (digitVal * multiplier);
            multiplier *= fromBase;
         }
         return accum;
      }

      private void OnDestChanged(object sender, EventArgs e)
      {
         if (_calculating)
            return;
         _calculating = true;
         string num = textBoxDest.Text;
         if (!string.IsNullOrEmpty(num))
         {
            textBoxSource.Text = ConvertFromBase(num, (int)numericUpDownBase.Value).ToString(CultureInfo.InvariantCulture);
         }
         _calculating = false;
      }
   }
}
