using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace LayoutPanels
{
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void ButtonClick(object sender, RoutedEventArgs e)
      {         
         var cmd = (Button)e.OriginalSource; // Получить текущую нажатую кнопку
         Type type = GetType();  // Создать экземпляр окна, названный текущей кнопкой
         Assembly assembly = type.Assembly;
         var win = (Window)assembly.CreateInstance(string.Format("{0}.{1}", type.Namespace, cmd.Content));
         
         if (win != null)
            win.ShowDialog(); // Показать окно
      }
   }
}