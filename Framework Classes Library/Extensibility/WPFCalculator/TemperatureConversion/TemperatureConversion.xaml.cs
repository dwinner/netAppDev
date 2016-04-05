using System;
using System.Windows;

namespace TemperatureConversion
{
   /// <summary>
   /// Элемент управления для преобразования температур
   /// </summary>
   public partial class TemperatureConversion
   {
      public TemperatureConversion()
      {
         InitializeComponent();
         DataContext = Enum.GetNames(typeof (TempConversionType));
      }

      private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
      {
         TempConversionType fromType, toType;
         if (!Enum.TryParse((string) FromComboBox.SelectedValue, out fromType) ||
             !Enum.TryParse((string) ToComboBox.SelectedValue, out toType)) return;
         double temperature;
         string temperatureText = InpuTextBox.Text;
         if (double.TryParse(temperatureText, out temperature))
         {
            double result = temperature.ToCelsius(fromType).FromCelsius(toType);
            OutpuTextBox.Text = result.ToString("F");
         }
         else
         {
            MessageBox.Show(string.Format("Cannot convert temperature {0}", temperatureText));
         }
      }
   }
}
