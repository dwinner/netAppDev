using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CodeOnlyWpf
{
   public class MainWindow : Window
   {
      private Button _thanksButton;

      public MainWindow()
      {
         InitializeComponent();
      }

      private void InitializeComponent()
      {
         // Сконфигурировать форму
         Width = Height = 285;
         Left = Top = 100;
         Title = "Code-Only Window";

         // Создать контейнер, содержащий кнопку
         var panel = new DockPanel();

         // Создать кнопку
         _thanksButton = new Button { Content = "Please click me", Margin = new Thickness(30) };

         // Присоединить обработчик событий
         _thanksButton.Click += (sender, args) =>
         {
            _thanksButton.Content = "Thahk you";
         };

         // Поместить кнопку в панель
         (panel as IAddChild).AddChild(_thanksButton);

         // Поместить панель в форму
         AddChild(panel);
      }
   }
}