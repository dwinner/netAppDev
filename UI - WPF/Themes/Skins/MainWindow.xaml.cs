using System;
using System.Windows;

namespace Skins
{
   public partial class MainWindow
   {
      private int _currentSkin;

      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnCancel(object sender, RoutedEventArgs e)
      {
         _currentSkin = (_currentSkin + 1) % 2;
         Application.Current.Resources =
            (ResourceDictionary)
            Application.LoadComponent(new Uri(_currentSkin == 0 ? "LightAndFluffySkin.xaml" : "ElectricSkin.xaml",
               UriKind.RelativeOrAbsolute));
      }
   }
}