using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.IO;

namespace NonCompiledXaml
{
   public class Window1 : Window
   {
      private Button _clickmeButton;

      public Window1(string xamlFile)
      {
         InitializeComponent(xamlFile);
      }

      private void InitializeComponent(string xamlFile)
      {
         // Сконфигурировать форму
         Width = Height = 285;
         Left = Top = 100;
         Title = "Dynamically Loaded XAML";

         // Получить контент XAML из внешнего файла
         DependencyObject rootElement;
         using (var stream = new FileStream(xamlFile, FileMode.Open))
         {
            rootElement = (DependencyObject)XamlReader.Load(stream);
         }

         // Вставить разметку в это окно
         Content = rootElement;

         // Найти элемент управления с соответствующим именем
         var frameworkElement = (FrameworkElement)rootElement;
         _clickmeButton = (Button)frameworkElement.FindName("ClickMeButton");

         // Присоедигить обработчик событий
         if (_clickmeButton != null)
         {
            _clickmeButton.Click += (sender, e) =>
            {
               _clickmeButton.Content = "Thank you.";
            };
         }
      }
   }
}



