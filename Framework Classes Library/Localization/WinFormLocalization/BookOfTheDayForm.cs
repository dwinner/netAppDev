using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace WinFormLocalization
{
   public partial class BookOfTheDayForm : Form
   {
      public BookOfTheDayForm(string culture)
      {
         if (culture != null && !string.IsNullOrEmpty(culture))
         {
            try
            {
               var currentCultureInfo = new CultureInfo(culture);

               // Установить культуру для форматирования
               Thread.CurrentThread.CurrentCulture = currentCultureInfo;

               // Установить культуру для ресурсов
               Thread.CurrentThread.CurrentUICulture = currentCultureInfo;
            }
            catch (CultureNotFoundException)
            {               
            }
         }

         WelcomeMessage();
         InitializeComponent();
         SetDateAndNumber();
      }

      private void WelcomeMessage()
      {         
         DateTime now = DateTime.Now;
         string message = now.Hour <= 12
            ? Properties.Resources.GoodMorning
            : now.Hour <= 19 ? Properties.Resources.GoodAfternoon : Properties.Resources.GoogEvening;
         MessageBox.Show(string.Format("{0}\n{1}", message, Properties.Resources.SampleMessage));
      }

      private void SetDateAndNumber()
      {
         DateTime today = DateTime.Today;
         DateTextBox.Text = today.ToString("D");
         const int itemsSold = 327444;
         BooksSoldTextBox.Text = itemsSold.ToString("###,###,###");
      }
   }
}
