using System;
using System.Windows;
using System.Windows.Controls;

namespace ControlTemplates
{
   public partial class GradientButtonTest
   {
      public GradientButtonTest()
      {
         InitializeComponent();
      }

      void Clicked(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(string.Format("You clicked {0}", ((Button) sender).Name));
      }

      void OnThemeChanged(object sender, RoutedEventArgs e)
      {
         Resources.MergedDictionaries[0] = new ResourceDictionary
         {
            Source = new Uri("Resources/GradientButtonVariant.xaml", UriKind.Relative)
         };
      }

      void OnThemeUnchanged(object sender, RoutedEventArgs e)
      {
         Resources.MergedDictionaries[0] = new ResourceDictionary
         {
            Source = new Uri("Resources/GradientButton.xaml", UriKind.Relative)
         };
      }
   }
}