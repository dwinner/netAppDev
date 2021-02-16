using System.Windows;
using System.Windows.Media;

namespace CustomControlsClient
{
	public partial class ColorPickerUserControlTest
	{
		public ColorPickerUserControlTest()
		{
			InitializeComponent();
		}

		private void OnGetClick(object sender, RoutedEventArgs e) => MessageBox.Show(ColorPicker.Color.ToString());

		private void OnSetClick(object sender, RoutedEventArgs e) => ColorPicker.Color = Colors.Beige;

		private void OnColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			if (ColorLabel != null)
				ColorLabel.Text = $"The new color is {e.NewValue}";
		}
	}
}