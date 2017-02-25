using System.Windows;
using FirstsStepsRUI.ViewModels;
using ReactiveUI;

namespace FirstsStepsRUI.Views
{
   public partial class LoginView : IViewFor<LoginViewModel>
   {
      public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
         typeof(LoginViewModel), typeof(LoginView), new PropertyMetadata(null));

      public LoginView()
      {
         InitializeComponent();
         UserName.Focus();
         this.Bind(ViewModel, vm => vm.UserName, v => v.UserName.Text);
         this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
      }

      object IViewFor.ViewModel
      {
         get { return ViewModel; }
         set { ViewModel = (LoginViewModel) value; }
      }

      public LoginViewModel ViewModel
      {
         get { return (LoginViewModel) GetValue(ViewModelProperty); }
         set
         {
            SetValue(ViewModelProperty, value);
            // This is dirty, but not dirtier than using a TextBox for passwords (-8
            value.Password = Password;
         }
      }
   }
}