using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using CalculatorContract;
using CalculatorUtils;

namespace TemperatureConversion
{
   /// <summary>
   ///    Тип элемента управления для экспорта
   /// </summary>
   [CalculatorExtensionExport(typeof(ICalculatorExtension), Title = "Temperature Conversion",
      Description = "Convert Celsius to Fahrenheit to Celsius", ImageUri = "Temperature.png")]
   public class TemperatureCalculatorExtension : ICalculatorExtension
   {
      private TemperatureConversion _control;

      public FrameworkElement Ui
      {
         get { return _control ?? (_control = new TemperatureConversion()); }
      }

      private BitmapImage _image;

      public BitmapImage Image
      {
         get
         {
            if (_image == null)
            {
               _image = new BitmapImage();
               _image.BeginInit();
               Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Temperature.png");
               _image.StreamSource = imageStream;
               _image.EndInit();
            }

            return _image;
         }
      }
   }
}