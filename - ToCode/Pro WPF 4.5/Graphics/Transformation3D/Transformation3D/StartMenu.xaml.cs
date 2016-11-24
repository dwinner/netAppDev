using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Transformation3D
{
   /// <summary>
   /// Interaction logic for StartMenu.xaml
   /// </summary>

   public partial class StartMenu : System.Windows.Window
   {

      public StartMenu()
      {
         InitializeComponent();
      }

      private void StartMenuButton_Click(object sender, RoutedEventArgs e)
      {
         Button btn = (Button)e.Source;
         if (btn.Content.ToString() != "Close")
         {
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            Window window = (Window)assembly.CreateInstance(
                type.Namespace + "." + btn.Content);
            window.ShowDialog();
         }
         else
            this.Close();
      }
   }
}