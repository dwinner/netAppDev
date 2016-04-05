using System.Net;
using System.Web.Security;
using System.Windows;

namespace AuthenticationServices
{
   /// <summary>
   /// Клиентское приложение для демонстрации аутентификации через собственный поставщик
   /// </summary>
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      void OnLogin(object sender, RoutedEventArgs e)
      {
         try
         {
            LabelValidatedInfo.Visibility = Visibility.Hidden;
            if (Membership.ValidateUser(TextUsername.Text, TextPassword.Password))
            {
               LabelValidatedInfo.Visibility = Visibility.Visible;
            }
            else
            {
               MessageBox.Show("Username or password not valid", "Client Authentication Services", MessageBoxButton.OK,
                  MessageBoxImage.Warning);
            }
         }
         catch (WebException ex)
         {
            MessageBox.Show(ex.Message, "Client Application Services", MessageBoxButton.OK, MessageBoxImage.Error);
         }

      }
   }
}
