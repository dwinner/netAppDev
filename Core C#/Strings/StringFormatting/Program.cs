/**
 * Форматные строки
 */

using System.Globalization;
using System.Windows.Forms;

namespace _03_StringFormatting
{
   class Program
   {
      static void Main()
      {
         CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
         var germanyCultureInfo = new CultureInfo("de-DE");
         var russianCultureInfo = new CultureInfo("ru-RU");
         const double money = 123.45;
         string localMoney = money.ToString("C", currentCultureInfo);
         MessageBox.Show(localMoney, "Локальные деньги");
         localMoney = money.ToString("C", germanyCultureInfo);
         MessageBox.Show(localMoney, "Деньги Германии");
         localMoney = money.ToString("C", russianCultureInfo);
         MessageBox.Show(localMoney, "Деньги России");
      }
   }
}
