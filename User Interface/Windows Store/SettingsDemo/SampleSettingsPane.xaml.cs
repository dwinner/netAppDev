using Windows.UI.Xaml.Controls;

namespace SettingsDemo
{
   public sealed partial class SampleSettingsPane : UserControl
   {
      private readonly SampleSettings _settings = new SampleSettings { Text1 = "enter a value", Text2 = "enter a value" };

      public SampleSettingsPane()
      {
         InitializeComponent();
         DataContext = _settings;
      }
   }
}